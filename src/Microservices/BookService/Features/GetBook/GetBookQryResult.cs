namespace BookService.Features.GetBook;

public record GetBookQryResult(
    Guid Id,
    string Title,
    string Author,
    double Price,
    bool IsLoaned);
