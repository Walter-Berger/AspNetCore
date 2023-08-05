namespace IdentityService.Endpoints;

public static class Register
{
    public static IEndpointRouteBuilder MapRegister(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(EndpointRoutes.Auth.Register, async (RegisterRequest request, ISender mediator, CancellationToken ct) =>
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

        return endpoints;
    }
}
