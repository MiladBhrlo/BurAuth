using BurAuthLib.Models;
using System.Text;
using System.Text.Json;

namespace BurAuthLib.Services;

public class BurAuthService
{
    private readonly HttpClient _httpClient;

    public BurAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAuthorizationCodeAsync(string clientId, string redirectUri, string codeChallenge)
    {
        var url = $"[sso-address]/oauth2/authorize?client_id={clientId}&redirect_uri={redirectUri}&response_type=code&code_challenge={codeChallenge}&code_challenge_method=S256";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var code = await response.Content.ReadAsStringAsync();
        return code;
    }

    public async Task<TokenResponse> GetAccessTokenAsync(string clientId, string clientSecret, string code, string redirectUri, string codeVerifier)
    {
        var requestContent = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("code_verifier", codeVerifier)
            });

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "[sso-address]/oauth2/token")
        {
            Headers =
                {
                    { "Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))}" }
                },
            Content = requestContent
        };

        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TokenResponse>(json);
    }
}
