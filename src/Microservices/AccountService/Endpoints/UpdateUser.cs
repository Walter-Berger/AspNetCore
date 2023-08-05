namespace AccountService.Endpoints;

public static class UpdateUser
{
    public static IEndpointRouteBuilder MapUpdateUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(EndpointRoutes.User.Update,
            async (Guid id, UpdateUserRequest request, ISender mediator, CancellationToken ct) =>
            {
                var cmd = new UpdateUserCmd(
                    Id: id,
                    Email: request.Email,
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
