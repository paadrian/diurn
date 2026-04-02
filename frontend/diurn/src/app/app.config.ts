import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserCacheLocation, InteractionType, IPublicClientApplication, LogLevel, PublicClientApplication } from '@azure/msal-browser';
import { MsalGuardConfiguration, MsalInterceptorConfiguration, MsalService, MsalGuard, MsalInterceptor, MsalBroadcastService, MSAL_INSTANCE, MSAL_INTERCEPTOR_CONFIG, MSAL_GUARD_CONFIG } from '@azure/msal-angular';

import { environment } from '../environments/environment';

const isIE = !globalThis.navigator.userAgent.includes('MSIE')
          || !globalThis.navigator.userAgent.includes('Trident/');

function MsalInstanceFactory(): IPublicClientApplication
{
  return new PublicClientApplication(
  {
    auth:
    {
      clientId: environment.azure.clientId || "",
      authority: `${environment.azure.instance}/${environment.azure.tenantId}`,
      redirectUri: environment.redirectUrl,
      postLogoutRedirectUri: environment.logoutUrl
    },
    cache:
    {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: isIE, // set to true for IE 11
    },
    system:
    {
      loggerOptions: 
      {
        loggerCallback: (level: LogLevel, message: string) =>
        {
          switch (level)
          {
            case LogLevel.Error:
              console.error(message);
              return;
            case LogLevel.Info:
              console.info(message);
              return;
            case LogLevel.Verbose:
              console.debug(message);
              return;
            case LogLevel.Warning:
              console.warn(message);
              return;
          }
        },
        logLevel: LogLevel.Verbose,
        piiLoggingEnabled: true
      }
    }
  });
}

function MsalGuardConfigFactory(): MsalGuardConfiguration 
{
  return {
    interactionType: InteractionType.Redirect,
    authRequest: 
    {
      scopes: [
        "openid",
        "profile",
        "offline_access",
        ...environment.azure.audience || []
      ]
    }
  };
}

function MsalInterceptorConfigFactory(): MsalInterceptorConfiguration 
{
  const protectedResourceMap = new Map<string, Array<string>>([
    [environment.graph.baseUrl, ["user.read"]],
    [environment.apiBaseUrl, environment.azure.audience || []],
    [environment.apiBaseUrl, environment.azure.audience || []]
  ]);
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi(), withFetch()),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MsalInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MsalGuardConfigFactory
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MsalInterceptorConfigFactory
    },    
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ]
};
