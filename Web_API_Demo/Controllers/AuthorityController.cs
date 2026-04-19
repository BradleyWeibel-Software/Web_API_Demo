using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Authority;

namespace Web_API_Demo.Controllers
{
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorityController(IConfiguration configuration) 
        {
            this._configuration = configuration;
        }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AppCredential credential)
        {
            if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                var securityKey = _configuration.GetValue<string>("SecurityKey");
                securityKey = securityKey ?? "kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk"; // TODO: why can't this come from appsettings in Web_API_Demo instead of WebApp?

                return Ok(new
                {
                    access_token = Authenticator.CreateToken(credential.ClientId, expiresAt, securityKey),
                    expires_at = expiresAt
                });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized - Respect my authoritaaaay!");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Invalid client credentials provided."
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }
    }
}
