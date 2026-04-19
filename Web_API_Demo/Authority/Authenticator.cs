using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Web_API_Demo.Authority
{
    public static class Authenticator
    {
        public static bool Authenticate(string clientId, string secret)
        {
            var app = AppRepository.GetApplicationByClientId(clientId);
            if (app == null)
                return false;

            return (app.ClientId == clientId && app.Secret == secret);
        }

        public static string CreateToken(string clientId, DateTime expiresAt, string secretKey)
        {
            // Signing key

            // Algorythm
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature);

            // Payload (claims)
            var app = AppRepository.GetApplicationByClientId(clientId);
            var claimsDictionary = new Dictionary<string, object>
            {
                { "AppName", app?.ApplicationName ?? string.Empty },
                { "Read", (app?.Scopes ?? string.Empty).Contains("read") ? "true" : "false" },
                { "Write", (app?.Scopes ?? string.Empty).Contains("write") ? "true" : "false" }
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = signingCredentials,
                Claims = claimsDictionary,
                Expires = expiresAt,
                NotBefore = DateTime.UtcNow
            };

            var tokenHandler = new JsonWebTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        public static async Task<bool> VerifyTokenAsync(string token, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(secretKey))
                return false;

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JsonWebTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);
                return result.IsValid;
            }
            catch (SecurityTokenMalformedException)
            {
                // Token is malformed
                return false;
            }
            catch (SecurityTokenExpiredException)
            {
                // Token is expired
                return false;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                // Token has invalid signature
                return false;
            }
            catch (Exception)
            {
                // Other exceptions
                throw;
            }
        }
    }
}
