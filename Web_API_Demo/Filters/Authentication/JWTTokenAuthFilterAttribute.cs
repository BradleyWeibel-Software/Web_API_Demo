using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            var secretKey = configuration?["SecurityKey"] ?? "kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk"; // TODO: why can't this come from appsettings?

            // 4. Verify the Token
            if (await Authenticator.VerifyTokenAsync(token, secretKey))
            {
                // Token is valid, continue with the request
                return;
            }
            else
            {
                // Token is invalid, return 401 Unauthorized
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
