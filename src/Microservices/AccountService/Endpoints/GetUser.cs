using AccountService.Features.GetUser;
using Common.Extensions;
using Contracts.User.Responses;
using MediatR;
using System.Security.Claims;

namespace AccountService.Endpoints;

public class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Returns the currently logged in user
        app.MapGet("/api/users/me", async (ISender mediator, CancellationToken ct, HttpContext httpContext) =>
        {
            var userClaims = httpContext.User.Claims;
            var id = Guid.Parse(userClaims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);

            var qry = new GetUserQry(id);
            var result = await mediator.Send(qry, ct);
            var response = new GetUserResponse(
                Id: result.Id,
                Email: result.Email,
                FirstName: result.FirstName,
                LastName: result.LastName);

            return Results.Ok(response);
        })
        .RequireAuthorization();
    }
}
