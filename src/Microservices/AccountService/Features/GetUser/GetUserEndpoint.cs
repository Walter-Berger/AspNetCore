using Common.Extensions;
using Contracts.User.Responses;
using System.Security.Claims;

namespace AccountService.Features.GetUser;

public class GetUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Returns the currently logged in user
        app.MapGet("/api/users/me", async (
            GetUserService getUserService,
            HttpContext httpContext,
            CancellationToken ct) =>
        {
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var qry = new GetUserQuery(userId);
            var response = await getUserService.GetUser(qry, ct);

            return Results.Ok(response);
        })
        .RequireAuthorization();
    }
}
