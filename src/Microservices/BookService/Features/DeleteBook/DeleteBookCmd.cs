namespace BookService.Features.DeleteBook;

public record DeleteBookCmd(Guid Id) : IRequest<Unit>;
