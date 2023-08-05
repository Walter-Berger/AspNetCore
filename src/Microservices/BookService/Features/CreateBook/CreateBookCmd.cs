namespace BookService.Features.CreateBook;

public record CreateBookCmd(
    string Title,
    string Author,
    double Price) : IRequest<Unit>;
