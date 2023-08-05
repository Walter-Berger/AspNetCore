namespace BookService.Features.GetAllBooks;

public record GetAllBooksQry() : IRequest<List<GetAllBooksQryResult>>;
