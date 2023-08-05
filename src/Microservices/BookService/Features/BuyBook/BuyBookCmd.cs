namespace BookService.Features.BuyBook;

public record BuyBookCmd(Guid Id) : IRequest<Unit>;
