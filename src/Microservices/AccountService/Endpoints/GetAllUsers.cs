using AccountService.Features.GetAllUsers;
using Common.Extensions;
using Contracts.User.Responses;
using MediatR;

namespace AccountService.Endpoints;

public class GetAllUsers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Returns all users without parameters
        app.MapGet("/api/users", async (ISender mediator, CancellationToken ct) =>
        {
            var qry = new GetAllUsersQry();
            var results = await mediator.Send(qry, ct);

            var responses = new List<GetUserResponse>();
            foreach (var result in results)
            {
                responses.Add(new GetUserResponse(
                    Id: result.Id,
                    Email: result.Email,
                    FirstName: result.FirstName,
                    LastName: result.LastName));
            }

            return Results.Ok(responses);
        })
        .RequireAuthorization();
    }
}
