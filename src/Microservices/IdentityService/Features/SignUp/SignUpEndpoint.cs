using Common.Extensions;
using MediatR;

namespace IdentityService.Features.SignUp;

public class SignUpEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth", async (SignUp.Request request, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new SignUp.Request(
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
