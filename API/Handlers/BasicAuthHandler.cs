using UB = Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace API.Handlers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UB.IAuthenticationService _authService;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UB.IAuthenticationService authService) : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Response.StatusCode = 401;
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }

            var authHeader = Request.Headers["Authorization"].ToString();

            if (!authHeader.StartsWith("Basic", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.StatusCode = 401;
                return Task.FromResult(AuthenticateResult.Fail("Malformed Authorization Header"));
            }


            var token = authHeader.Substring("Basic ".Length).Trim();

            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialstring.Split(':');

            if (!_authService.ValidatePassword(credentials[0], credentials[1]))
            {
                Response.StatusCode = 401;
                return Task.FromResult(AuthenticateResult.Fail("Invalid Credentials!"));
            }

            var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "User") };
            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
