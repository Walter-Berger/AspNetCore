namespace AccountService.Endpoints;

public static class UpdateUser
{
    public static IEndpointRouteBuilder MapUpdateUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/api/users/{id}",
            async (Guid id, UpdateUserRequest request, ISender mediator, CancellationToken ct) =>
            {
                var cmd = new UpdateUserCmd(
                    Id: id,
                    Email: request.Email,
                    FirstName: request.FirstName,
                    LastName: request.LastName);

                await mediator.Send(cmd, ct);
                return Results.Ok();
            })
            .RequireAuthorization();

        return endpoints;
    }
}
