using BurAuthLib.Helpers;
using BurAuthLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace BurAuth.Extensions;

public static class BurAuthExtension
{
    public static IServiceCollection AddBurAuth(this IServiceCollection services)
    {
        services.AddScoped<PKCEHelper>();
        services.AddScoped<BurAuthProvider>();
        services.AddScoped<BurAuthTokenProvider>();
        services.AddScoped<AuthenticationStateProvider,BurAuthenticationStateProvider>();
        services.AddScoped<IAuthorizationPolicyProvider, BurAuthorizationPolicyProviderr>();
        services.AddScoped<IAuthorizationHandler, BurAuthorizationHandler>();
        services.AddScoped<IAuthorizationService, DefaultAuthorizationService>();
        services.AddAuthorizationCore();
        return services;
    }
}
