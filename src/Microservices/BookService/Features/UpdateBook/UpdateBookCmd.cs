namespace BookService.Features.UpdateBook;

public record UpdateBookCmd(
    Guid Id,
    string Author,
    string Title,
    double Price) : IRequest<Unit>;

