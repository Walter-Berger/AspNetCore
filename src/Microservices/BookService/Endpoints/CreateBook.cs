namespace BookService.Endpoints;

public static class CreateBook
{
    public static IEndpointRouteBuilder MapCreateBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(EndpointRoutes.Book.Create, async (CreateBookRequest request, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new CreateBookCmd(
                Title: request.Title,
                Author: request.Author,
                Price: request.Price);

            await mediator.Send(cmd, ct);
            return Results.Ok();
        })
        .RequireAuthorization();

        return endpoints;
    }
}
