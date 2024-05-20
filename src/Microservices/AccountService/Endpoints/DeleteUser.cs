namespace AccountService.Endpoints;

public static class DeleteUser
{
    public static IEndpointRouteBuilder MapDeleteUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/api/users/{id}", async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new DeleteUserCmd(id);
            await mediator.Send(cmd, ct);
            return Results.Ok();
        })
        .RequireAuthorization();

        return endpoints;
    }
}
