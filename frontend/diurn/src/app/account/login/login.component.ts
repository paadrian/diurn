import { Component, OnDestroy, OnInit, inject } from "@angular/core";
import { UserProfile } from "../../models/userProfile";
import { Subject, takeUntil } from "rxjs";
import { MSAL_GUARD_CONFIG, MsalBroadcastService, MsalGuardConfiguration, MsalService } from "@azure/msal-angular";
import { RedirectRequest, EventMessage, EventType, InteractionStatus } from "@azure/msal-browser";
import { environment } from "../../../environments/environment";
import { filter } from "rxjs";
import { UserService } from "../../services/user.service";
import { AsyncPipe } from "@angular/common";

@Component({
    selector: "app-login",
    imports: [AsyncPipe ],
    templateUrl: "./login.component.html",
    styleUrl: "./login.component.scss"
})
export class LoginComponent implements OnInit, OnDestroy {
    user: UserProfile = new UserProfile();
    userService = inject(UserService);

    private readonly _destroying$ = new Subject<void>();
    private readonly authService = inject(MsalService);
    private readonly msalBroadcastingService = inject(MsalBroadcastService);
    private readonly msalGuardConfig = inject<MsalGuardConfiguration>(MSAL_GUARD_CONFIG);

    ngOnInit(): void {
        this.authService.instance.initialize().then(() =>
        {
            this.authService.instance.handleRedirectPromise().then(response =>
            {
                if (response)
                {
                    this.authService.instance.setActiveAccount(response.account);
                    // Decode the access token to read roles and cache them
                    if (response.accessToken) {
                        const tokenClaims = this.decodeToken(response.accessToken);
                        if (tokenClaims?.roles) {
                            this.userService.setRoles(tokenClaims.roles);
                        }
                    }
                }
            })
        });

        this.msalBroadcastingService.msalSubject$
        .pipe(
            filter((msg: EventMessage) => msg.eventType === EventType.ACCOUNT_ADDED || msg.eventType === EventType.ACCOUNT_REMOVED))
        .subscribe((result: EventMessage) => {
            if (this.authService.instance.getAllAccounts().length === 0) {
                globalThis.location.pathname = "/";
            }
            else {
                this.setLoginDisplay();
            }                 
        });

        this.msalBroadcastingService.inProgress$
        .pipe(
            filter((status: InteractionStatus) => status === InteractionStatus.None),
            takeUntil(this._destroying$)
        )
        .subscribe(() => {
            this.setLoginDisplay();
        });
    }

    setLoginDisplay() : void {
        const accounts = this.authService.instance.getAllAccounts();
        const isAuthenticated = accounts.length > 0;

        if (isAuthenticated && accounts[0])
        {
            const user = new UserProfile();
            user.username = accounts[0].username || "";
            user.firstName = accounts[0].idTokenClaims?.["given_name"]?.toString() || "";
            user.lastName = accounts[0].idTokenClaims?.["family_name"]?.toString() || "";
            user.email = accounts[0].idTokenClaims?.["email"]?.toString() || "";
            this.userService.setUser(user);
            this.authService.instance.setActiveAccount(accounts[0]);
        }
        else {
            this.userService.resetUser();
        }
    }

    private decodeToken(token: string): any {
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replaceAll('-', '+').replaceAll('_', '/');
            const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => {
                return '%' + ('00' + c.codePointAt(0)?.toString(16)).slice(-2);
            }).join(''));
            return JSON.parse(jsonPayload);
        } catch (error) {
            console.error('Failed to decode token:', error);
            return null;
        }
    }

    loginEntraId() : void {
        if (this.msalGuardConfig.authRequest){
            this.authService.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
        }
        else {
            this.authService.loginRedirect();
        }
    }

    logout() : void {
        this.authService.logoutRedirect({
            postLogoutRedirectUri: environment.logoutUrl || "http://localhost:4200"
        });
        
        this.setLoginDisplay();
    }

    ngOnDestroy() : void {
        this._destroying$.next();
        this._destroying$.complete();
    }
}