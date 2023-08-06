namespace BookService.Endpoints;

public static class GetBook
{
    public static IEndpointRouteBuilder MapGetBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(EndpointRoutes.Book.Get, async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var qry = new GetBookQry(id);
            var result = await mediator.Send(qry, ct);
            var response = new GetBookResponse(
                Id: result.Id,
                Title: result.Title,
                Author: result.Author,
                Price: result.Price,
                IsLoaned: result.IsLoaned);

            return response;
        })
        .RequireAuthorization();

        return endpoints;
    }
}
