using Common.Extensions;
using Contracts.User.Requests;
using System.Security.Claims;

namespace AccountService.Features.UpdateUser;

public class UpdateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Updates a user
        app.MapPut("/api/users", async (
            UpdateUserService updateUserService,
            UpdateUserRequest request, 
            HttpContext httpContext, 
            CancellationToken ct) =>
        {
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var cmd = new UpdateUserCmd(
                Id: userId,
                Email: request.Email,
                FirstName: request.FirstName,
                LastName: request.LastName);

            await updateUserService.Update(cmd, ct);
            return Results.Ok();
        })
        .RequireAuthorization();
    }
}
