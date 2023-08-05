namespace BookService.Features.DeleteBook;

public class DeleteBookCmdHandler : IRequestHandler<DeleteBookCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;

    public DeleteBookCmdHandler(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Unit> Handle(DeleteBookCmd request, CancellationToken cancellationToken)
    {
        // check if book exists
        var book = await _databaseContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsBought == false, cancellationToken) 
            ?? throw new NotFoundException(ErrorDetails.BookNotFound);

        // check if book is currently loaned
        if (book.IsLoaned is true)
        {
            throw new DatabaseException(ErrorDetails.CannotDeleteLoanedBook);
        }

        // remove book from database
        _databaseContext.Remove(book);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
