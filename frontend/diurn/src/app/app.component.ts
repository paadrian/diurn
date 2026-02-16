import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import { MSAL_GUARD_CONFIG, MsalBroadcastService, MsalGuardConfiguration, MsalService } from '@azure/msal-angular';
import { EventMessage, EventType, InteractionStatus, RedirectRequest } from '@azure/msal-browser';
import { filter, Subject, takeUntil } from 'rxjs';

import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'diurn';
  currentPage : number = 1;

  isAuthenticated : boolean = false;
  userName : string = "";
  private readonly _destroying$ = new Subject<void>();
  private readonly authService = inject(MsalService);
  private readonly msalBroadcastService = inject(MsalBroadcastService);
  private readonly msalGuardConfig = inject<MsalGuardConfiguration>(MSAL_GUARD_CONFIG);

  ngOnInit(): void {
    this.authService.instance.initialize().then(() =>
    {
      this.authService.instance.handleRedirectPromise().then(response =>
      {
        if (response)
        {
          this.authService.instance.setActiveAccount(response.account);
        }

        this.setLoginDisplay();
      });
    });

    this.msalBroadcastService.msalSubject$
    .pipe(
      filter((msg: EventMessage) => msg.eventType === EventType.ACCOUNT_ADDED
       || msg.eventType === EventType.ACCOUNT_REMOVED)
       )
       .subscribe((result: EventMessage) => {
        if (this.authService.instance.getAllAccounts().length === 0) {
          globalThis.location.pathname = "/";
        }
        else {
          this.setLoginDisplay();
        }
       })

    this.msalBroadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None),
        takeUntil(this._destroying$)
      )
      .subscribe(() =>
      {
        this.setLoginDisplay();
      });
  }

  setLoginDisplay() : void
  {
    const accounts = this.authService.instance.getAllAccounts();
    this.isAuthenticated = accounts.length > 0;

    if (this.isAuthenticated && accounts[0])
    {
      this.userName = accounts[0].name || accounts[0].username || "";
      this.authService.instance.setActiveAccount(accounts[0]);
    }
  }

  login() : void
  {
    if (this.msalGuardConfig.authRequest)
    {
      this.authService.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
    }
    else
    {
      this.authService.loginRedirect();
    }
  }

  logout() : void
  {
    this.authService.logoutRedirect({
      postLogoutRedirectUri: environment.logoutUrl || "http://localhost:4200"
    });
  }

  ngOnDestroy(): void
  {
    this._destroying$.next();
    this._destroying$.complete();
  }

  changePage(page : number) : void
  {
    this.currentPage = page
  }
}
