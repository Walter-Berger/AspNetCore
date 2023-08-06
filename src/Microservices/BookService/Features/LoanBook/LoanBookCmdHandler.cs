namespace BookService.Features.LoanBook;

public class LoanBookCmdHandler : IRequestHandler<LoanBookCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;

    public LoanBookCmdHandler(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Unit> Handle(LoanBookCmd request, CancellationToken cancellationToken)
    {
        var book = await _databaseContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsBought == false, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.BookNotFound);

        if (book.IsLoaned is true)
        {
            throw new NotFoundException(ErrorDetails.BookLoaned);
        }

        book.Loan();
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
