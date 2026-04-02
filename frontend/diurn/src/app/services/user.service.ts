import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserProfile } from "../models/userProfile";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private readonly currentUserSubject: BehaviorSubject<UserProfile>;
    public currentUser$: Observable<UserProfile>;

    setUser(user: UserProfile) {
        this.currentUserSubject.value.id = user.id;
        this.currentUserSubject.value.username = user.username;
        this.currentUserSubject.value.email = user.email;
        this.currentUserSubject.value.firstName = user.firstName;
        this.currentUserSubject.value.lastName = user.lastName;
        this.currentUserSubject.next(user);
    }

    resetUser() {
        this.currentUserSubject.next(new UserProfile());
    }

    setRoles(roles: string[]): void {
        if (this.currentUserSubject.value) {
            this.currentUserSubject.value.Roles = roles;
            this.currentUserSubject.next(this.currentUserSubject.value);
        }
    }

    constructor() {
        this.currentUserSubject = new BehaviorSubject<UserProfile>(new UserProfile());
        this.currentUser$ = this.currentUserSubject.asObservable();
    }
}