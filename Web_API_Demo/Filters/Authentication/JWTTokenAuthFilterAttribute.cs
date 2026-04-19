using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web_API_Demo.Attributes;
using Web_API_Demo.Authority;

namespace Web_API_Demo.Filters.Authentication
{
    public class JWTTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 1. Get Authentication header from the request
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            string token = authorizationHeader.ToString();

            // 2. Get rid of the Bearer prefix
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // 3. Get Configuration and the secret key
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var secretKey = configuration.GetValue<string>("SecurityKey"); // TODO: why can't this come from appsettings in Web_API_Demo instead of WebApp?
            secretKey = secretKey ?? "kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk";

            // 4. Verify the Token and extract claims
            var claims = await Authenticator.VerifyTokenAsync(token, secretKey);
            if (claims != null)
            {
                // Get the claims requirement
                var requiredClaims = context.ActionDescriptor.EndpointMetadata.OfType<RequiredClaimAttribute>().ToList();
                if (requiredClaims != null &&
                    requiredClaims.All(rc => claims.Any(c => c.Type.Equals(rc.ClaimType, StringComparison.OrdinalIgnoreCase) && c.Value.Equals(rc.ClaimValue, StringComparison.OrdinalIgnoreCase))))
                {
                    // All required claims are present, allow access
                    return;
                }
                else
                {
                    // Required claims are not present
                    context.Result = new StatusCodeResult(403);
                }
            }
            else
            {
                // Token is invalid, return 401 Unauthorized
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
