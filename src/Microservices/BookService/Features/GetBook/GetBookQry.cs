namespace BookService.Features.GetBook;

public record GetBookQry(Guid Id) : IRequest<GetBookQryResult>;
