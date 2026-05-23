using AccountService.Features.UpdateUser;
using Common.Extensions;
using Contracts.User.Requests;
using MediatR;
using System.Security.Claims;

namespace AccountService.Endpoints;

public class UpdateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Updates a user with the given id
        app.MapPut("/api/users",
            async (UpdateUserRequest request, HttpContext httpContext, ISender mediator, CancellationToken ct) =>
            {
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                    return Results.Unauthorized();

                var cmd = new UpdateUserCmd(
                        Id: userId,
                        Email: request.Email,
                        FirstName: request.FirstName,
                        LastName: request.LastName);

                await mediator.Send(cmd, ct);
                return Results.Ok();
            })
            .RequireAuthorization();
    }
}
