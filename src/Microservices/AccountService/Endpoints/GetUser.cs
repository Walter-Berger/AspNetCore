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
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var qry = new GetUserQry(userId);
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
