namespace BookService.Features.UpdateBook;

public class UpdateBookCmdHandler : IRequestHandler<UpdateBookCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;
    private readonly ITimeFactory _timeFactory;

    public UpdateBookCmdHandler(DatabaseContext databaseContext, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _timeFactory = timeFactory;
    }

    public async Task<Unit> Handle(UpdateBookCmd request, CancellationToken cancellationToken)
    {
        // check if the requested book exists in database
        var book = await _databaseContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsBought == false, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.BookNotFound);

        // check if the book is currently loaned
        if (book.IsLoaned is true)
        {
            throw new NotFoundException(ErrorDetails.CannotUpdateLoanedBook);
        }

        // create a model of the updated version
        var updatedBook = new Book(
            id: request.Id,
            title: request.Title,
            author: request.Author,
            price: request.Price);

        // update the old book
        book.Update(updatedBook, _timeFactory.UnixTimeNow());
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
