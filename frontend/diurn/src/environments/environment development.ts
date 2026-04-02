export const environment = {
    apiBaseUrl: "http://localhost:5000/api",
    redirectUrl: "/",
    logoutUrl: "/",
    azure: {
        clientId: "6e839544-a762-44c6-ad77-3af4ba2d199b",
        tenantId: "a39938ac-4160-4a2c-a941-6cb792b714da",
        instance: "https://login.microsoftonline.com/",
        audience: [
            // "api://8331eab0-5687-411d-8fff-16056e15083f/User.Read",
            // "api://8331eab0-5687-411d-8fff-16056e15083f/User.Write"
            "api://8331eab0-5687-411d-8fff-16056e15083f/.default"
        ]
    },
    graph: {
        baseUrl: "https://graph.microsoft.com/v1.0/me"
    }
};