namespace BookService.Features.GetAllBooks;

public class GetAllBooksQryHandler : IRequestHandler<GetAllBooksQry, List<GetAllBooksQryResult>>
{
    private readonly DatabaseContext _databaseContext;

    public GetAllBooksQryHandler(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<GetAllBooksQryResult>> Handle(GetAllBooksQry request, CancellationToken cancellationToken)
    {
        // check if there are any books in database
        var books = await _databaseContext.Books.Where(x => x.IsBought == false).ToListAsync(cancellationToken);

        var results = new List<GetAllBooksQryResult>();
        foreach (var book in books)
        {
            results.Add(new GetAllBooksQryResult(
                Id: book.Id,
                Title: book.Title,
                Author: book.Author,
                Price: book.Price,
                IsLoaned: book.IsLoaned));
        }

        return results;
    }
}
