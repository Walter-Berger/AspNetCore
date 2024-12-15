using AccountService.Features.GetUser;
using Common.Extensions;
using Contracts.User.Responses;
using MediatR;

namespace AccountService.Endpoints;

public class GetUserById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Returns a single user with the given id
        app.MapGet("/api/users/{id}", async (Guid id, ISender mediator, CancellationToken ct) =>
        {
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
