namespace Diurn.Config;

public record AzureAdOptions(
    string Instance,
    string TenantId,
    string ClientId,
    string Audience,
    string[] Scopes,
    bool CallsWebApi
);