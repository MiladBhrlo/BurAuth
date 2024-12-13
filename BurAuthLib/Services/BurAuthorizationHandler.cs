using Microsoft.AspNetCore.Authorization;

namespace BurAuthLib.Services;
public class BurAuthorizationHandler : AuthorizationHandler<CustomPolicyRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomPolicyRequirement requirement)
    {
        // منطق سفارشی برای احراز شرایط سیاست امنیتی
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
