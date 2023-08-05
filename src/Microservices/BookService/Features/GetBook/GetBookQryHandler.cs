namespace BookService.Features.GetBook;

public class GetBookQryHandler : IRequestHandler<GetBookQry, GetBookQryResult>
{
    private readonly DatabaseContext _databaseContext;

    public GetBookQryHandler(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<GetBookQryResult> Handle(GetBookQry request, CancellationToken cancellationToken)
    {
        var book = await _databaseContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsBought == false, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.BookNotFound);

        var result = new GetBookQryResult(
            Id: book.Id,
            Title: book.Title,
            Author: book.Author,
            Price: book.Price,
            IsLoaned: book.IsLoaned);

        return result;
    }
}
