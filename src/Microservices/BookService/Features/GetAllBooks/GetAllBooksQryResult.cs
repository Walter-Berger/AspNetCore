namespace BookService.Features.GetAllBooks;

public record GetAllBooksQryResult(
    Guid Id,
    string Title,
    string Author,
    double Price,
    bool IsLoaned);
