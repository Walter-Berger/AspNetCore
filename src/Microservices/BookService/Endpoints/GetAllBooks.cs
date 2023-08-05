namespace BookService.Endpoints;

public static class GetAllBooks
{
    public static IEndpointRouteBuilder MapGetAllBooks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(EndpointRoutes.Book.GetAll, async (ISender mediator, CancellationToken ct) =>
        {
            var qry = new GetAllBooksQry();
            var results = await mediator.Send(qry, ct);

            var responses = new List<GetBookResponse>();
            foreach (var result in results)
            {
                responses.Add(new GetBookResponse(
                    Id: result.Id,
                    Title: result.Title,
                    Author: result.Author,
                    Price: result.Price,
                    IsLoaned: result.IsLoaned));
            }

            return Results.Ok(responses);
        })
        .RequireAuthorization();

        return endpoints;
    }
}
