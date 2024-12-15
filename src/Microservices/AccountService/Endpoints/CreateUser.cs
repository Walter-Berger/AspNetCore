namespace AccountService.Endpoints;

public class CreateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users",
            async (HttpContext context, CreateUserRequest request, ISender mediator, CancellationToken ct) =>
            {
                var claims = context.User.Claims;
                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;

                var cmd = new CreateUserCmd(
                    Email: email,
                    FirstName: request.FirstName,
                    LastName: request.LastName);

                await mediator.Send(cmd, ct);
                return Results.Ok();
            })
            .RequireAuthorization();
    }
}
