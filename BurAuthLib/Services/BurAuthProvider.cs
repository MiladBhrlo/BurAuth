using BurAuthLib.Helpers;
using BurAuthLib.Models;
using System.Text.Json;

namespace BurAuthLib.Services;
public class BurAuthProvider
{

    private readonly HttpClient _httpClient;
    private readonly PKCEHelper _pkceHelper;
    private bool _isAuthenticated;

    public BurAuthProvider(HttpClient httpClient, PKCEHelper pkceHelper)
    {
        _httpClient = httpClient;
        _pkceHelper = pkceHelper;
        _isAuthenticated = false;
    }

    public async Task<string> GetAuthorizationCodeAsync(string clientId, string redirectUri)
    {
        var (codeVerifier, codeChallenge) = _pkceHelper.GeneratePKCE();
        var authService = new BurAuthService(_httpClient);
        var authCode = await authService.GetAuthorizationCodeAsync(clientId, redirectUri, codeChallenge);
        _isAuthenticated = true;  // فرض کنیم که احراز هویت موفق بوده است
        return authCode;
    }

    public Task<bool> IsUserAuthenticatedAsync()
    {
        return Task.FromResult(_isAuthenticated);
    }

    public async Task<User> GetUserAsync()
    {
        // فرض کنیم که یک API برای دریافت اطلاعات کاربر موجود است
        var response = await _httpClient.GetAsync("[sso-address]/userinfo");
        response.EnsureSuccessStatusCode();
        var userString = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(userString);
        return user;
    }
}
