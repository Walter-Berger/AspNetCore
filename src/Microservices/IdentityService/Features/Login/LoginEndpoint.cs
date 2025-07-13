using Common.Extensions;
using IdentityService.Interfaces;
using MediatR;

namespace IdentityService.Features.Login;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth", async (HttpContext context, ICredentialService credentialService, ISender mediator, CancellationToken ct) =>
        {
            var authHeader = context.Request.Headers.Authorization.ToString();
            var (userName, password) = credentialService.ExtractUsernameAndPassword(authHeader);

            var qry = new Login.Request(userName, password);
            var result = await mediator.Send(qry, ct);
            var response = new Login.Response(result.AccessToken);
            return Results.Ok(response);
        });
    }
}