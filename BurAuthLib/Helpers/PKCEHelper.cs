using System.Security.Cryptography;
using System.Text;

namespace BurAuthLib.Helpers;

public class PKCEHelper
{
    public (string codeVerifier, string codeChallenge) GeneratePKCE()
    {
        var codeVerifier = GenerateCodeVerifier();
        var codeChallenge = GenerateCodeChallenge(codeVerifier);
        return (codeVerifier, codeChallenge);
    }
    private string GenerateCodeVerifier()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var bytes = new byte[32]; rng.GetBytes(bytes);
            return Base64UrlEncode(bytes);
        }
    }
    private string GenerateCodeChallenge(string codeVerifier)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.ASCII.GetBytes(codeVerifier);
            var hash = sha256.ComputeHash(bytes);
            return Base64UrlEncode(hash);
        }
    }
    private  string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }
}