namespace BookService.Features.CreateBook;

public class CreateBookCmdHandler : IRequestHandler<CreateBookCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;

    public CreateBookCmdHandler(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Unit> Handle(CreateBookCmd request, CancellationToken cancellationToken)
    {
        // create new book 
        var book = new Book(
            id: Guid.NewGuid(),
            title: request.Title,
            author: request.Author,
            price: request.Price
        );

        // TODO: check if the input is valid

        // save book in database
        await _databaseContext.AddAsync(book, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
