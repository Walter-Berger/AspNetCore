using Common.Extensions;
using Contracts.Auth.Requests;
using IdentityService.Features.Register;
using MediatR;

namespace IdentityService.Endpoints;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth", async (RegisterRequest request, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new RegisterCmd(
                Email: request.Email,
                FirstName: request.FirstName,
                LastName: request.LastName,
                Password: request.Password,
                ConfirmPassword: request.ConfirmPassword);

            await mediator.Send(cmd, ct);
            return Results.Ok();
        });
    }
}
