using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BurAuthLib.Services;
public class BurAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly BurAuthProvider _authProvider;

    public BurAuthenticationStateProvider(BurAuthProvider authProvider)
    {
        _authProvider = authProvider;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var isAuthenticated = await _authProvider.IsUserAuthenticatedAsync();
        ClaimsIdentity identity;

        if (isAuthenticated)
        {
            // فرض کنیم که اگر کاربر احراز هویت شده باشد، اطلاعات کاربر را می‌خوانیم
            var user = await _authProvider.GetUserAsync();
            identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                    // و افزودن دیگر ادعاهای مورد نیاز
                }, "Custom authentication");
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void MarkUserAsAuthenticated(string userName)
    {
        var identity = new ClaimsIdentity(new[]
        {
                new Claim(ClaimTypes.Name, userName)
            }, "apiauth");

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}