namespace AccountService.Endpoints;

public class DeleteUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id}", async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new DeleteUserCmd(id);
            await mediator.Send(cmd, ct);
            return Results.Ok();
        })
        .RequireAuthorization();
    }
}
