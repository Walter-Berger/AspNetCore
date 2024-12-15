using Common.Extensions;
using Contracts.Auth.Responses;
using IdentityService.Features.Login;
using IdentityService.Interfaces;
using MediatR;

namespace IdentityService.Endpoints;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth", async (HttpContext context, ICredentialService credentialService, ISender mediator, CancellationToken ct) =>
        {
            var authHeader = context.Request.Headers.Authorization.ToString();
            var (userName, password) = credentialService.ExtractUsernameAndPassword(authHeader);

            var qry = new LoginQry(userName, password);
            var result = await mediator.Send(qry, ct);
            var response = new LoginResponse(result.accessToken);
            return Results.Ok(response);
        });
    }
}