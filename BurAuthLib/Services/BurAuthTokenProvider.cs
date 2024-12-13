using BurAuthLib.Models;

namespace BurAuthLib.Services;
public class BurAuthTokenProvider
{
    private readonly HttpClient _httpClient;

    public BurAuthTokenProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TokenResponse> GetAccessTokenAsync(string clientId, string clientSecret, string code, string redirectUri, string codeVerifier)
    {
        var authService = new BurAuthService(_httpClient);
        return await authService.GetAccessTokenAsync(clientId, clientSecret, code, redirectUri, codeVerifier);
    }
}
