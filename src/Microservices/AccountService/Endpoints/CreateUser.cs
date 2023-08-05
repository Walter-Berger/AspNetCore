namespace AccountService.Endpoints;

public static class CreateUser
{
    public static IEndpointRouteBuilder MapCreateUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(EndpointRoutes.User.Create,
            async (HttpContext context, CreateUserRequest request, ISender mediator, CancellationToken ct) =>
            {
                var claims = context.User.Claims;
                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;

                var cmd = new CreateUserCmd(
                    Email: email,
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    BirthDate: request.BirthDate);

                await mediator.Send(cmd, ct);
                return Results.Ok();
            })
            .RequireAuthorization();

        return endpoints;
    }
}
