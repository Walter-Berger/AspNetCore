namespace BookService.Endpoints;

public static class GetBook
{
    public static IEndpointRouteBuilder MapGetBook(this IEndpointRouteBuilder endpoints)
    {
        // Returns book with given id
        endpoints.MapGet("/api/books/{id}", async (Guid id, ISender mediator, CancellationToken ct) =>
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


        // Returns all books
        endpoints.MapGet("/api/books", async (ISender mediator, CancellationToken ct) =>
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
