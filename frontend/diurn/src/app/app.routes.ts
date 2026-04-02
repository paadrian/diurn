import { Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    { path: "", loadComponent: () => import("./activity-list/activity-list.component").then(m => m.ActivityListComponent) },
    { path: "home", loadComponent: () => import('./activity-list/activity-list.component').then(m => m.ActivityListComponent) },
    { path: "login", loadComponent: () => import('./account/login/login.component').then(m => m.LoginComponent) },
    { path: "user", loadComponent: () => import('./account/user/user.component').then(m => m.UserComponent), canActivate: [MsalGuard] },
];
