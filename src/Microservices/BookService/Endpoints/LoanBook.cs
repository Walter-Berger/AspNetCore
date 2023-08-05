namespace BookService.Endpoints;

public static class LoanBook
{
    public static IEndpointRouteBuilder MapLoanBook(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(EndpointRoutes.Book.Loan, async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var cmd = new LoanBookCmd(id);
            await mediator.Send(cmd, ct);

            return Results.Ok();
        })
        .RequireAuthorization();

        return endpoints;
    }
}