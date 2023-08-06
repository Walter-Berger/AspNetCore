namespace BookService.Endpoints;

public static class DeleteBook
{
    public static IEndpointRouteBuilder MapDeleteBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(EndpointRoutes.Book.Delete, async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new DeleteBookCmd(id);
            await mediator.Send(cmd, ct);
            return Results.Ok("Book deleted.");
        })
        .RequireAuthorization();

        return endpoints;
    }
}
