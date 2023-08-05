namespace BookService.Endpoints;

public static class UpdateBook
{
    public static IEndpointRouteBuilder MapUpdateBook(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPut(EndpointRoutes.Book.Update,  async (Guid id, UpdateBookRquest request, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new UpdateBookCmd(
                Id: id,
                Author: request.Author,
                Title: request.Title,
                Price: request.Price);

            await mediator.Send(cmd, ct);
            return Results.Ok();
        })
        .RequireAuthorization();

        return endpoint;
    }
}
