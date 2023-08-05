namespace AccountService.Features.GetUser;

public record GetUserQry(Guid Id) : IRequest<GetUserQryResult>;
