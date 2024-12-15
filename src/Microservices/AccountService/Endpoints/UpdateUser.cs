using AccountService.Features.UpdateUser;
using Common.Extensions;
using Contracts.User.Requests;
using MediatR;

namespace AccountService.Endpoints;

public class UpdateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Updates a user with the given id
        app.MapPut("/api/users/{id}",
            async (Guid id, UpdateUserRequest request, ISender mediator, CancellationToken ct) =>
            {
                var cmd = new UpdateUserCmd(
                        Id: id, // TODO: id should be taken from the currently logged in user
                        Email: request.Email,
                        FirstName: request.FirstName,
                        LastName: request.LastName);

                await mediator.Send(cmd, ct);
                return Results.Ok();
            })
            .RequireAuthorization();
    }
}
