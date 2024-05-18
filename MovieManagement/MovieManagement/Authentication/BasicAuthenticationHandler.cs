using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;
using MovieManagement.DataAccess.Entities;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace MovieManagement.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IQueryExecutor _queryExecutor;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IQueryExecutor queryExecutor)
        : base(options, logger, encoder)
    {
        _queryExecutor = queryExecutor;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            return AuthenticateResult.NoResult();
        }

        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        User user = null!;

        try
        {
            var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]!);
            var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            var query = new GetUserQuery { Login = username };
            user = await _queryExecutor.Execute(query);

            if (user is null || user.Password != password)
            {
                return AuthenticateResult.Fail("Wrong Username or Password");
            }
        }
        catch
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login!),
            new Claim(ClaimTypes.Role, user.AccessLevel.ToString()!),
            new Claim(ClaimTypes.UserData, user.IsActive.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}
