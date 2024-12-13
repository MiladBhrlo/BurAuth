using Microsoft.AspNetCore.Authorization;

namespace BurAuthLib.Services;
public class BurAuthorizationPolicyProviderr : IAuthorizationPolicyProvider
{
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName == "CustomPolicy")
        {
            return Task.FromResult(new AuthorizationPolicyBuilder().AddRequirements(new CustomPolicyRequirement()).Build());
        }

        return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
    }
}

public class CustomPolicyRequirement : IAuthorizationRequirement { }