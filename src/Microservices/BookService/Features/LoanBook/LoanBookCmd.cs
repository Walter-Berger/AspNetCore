namespace BookService.Features.LoanBook;

public record LoanBookCmd(Guid Id) : IRequest<Unit>;
