using Common.Extensions;
using IdentityService.Interfaces;

namespace IdentityService.Features.Login;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth", async (
            HttpContext context,
            ICredentialService credentialService,
            ILoginService loginService,
            CancellationToken ct) =>
        {
            var authHeader = context.Request.Headers.Authorization.ToString();
            var (userName, password) = credentialService.ExtractUsernameAndPassword(authHeader);

            var query = new LoginQuery(userName, password);
            var result = await loginService.Login(query, ct);

            return Results.Ok(result);
        });
    }
}