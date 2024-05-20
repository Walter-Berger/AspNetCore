namespace BookService.Endpoints;

public static class BuyBook
{
    public static IEndpointRouteBuilder MapBuyBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/api/books/{id}/buy", async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new BuyBookCmd(id);
            await mediator.Send(cmd, ct);

            return Results.Ok();
        })
        .RequireAuthorization();

        return endpoints;
    }
}
