namespace AccountService.Features.DeleteUser;

public record DeleteUserCmd(Guid Id) : IRequest<Unit>;
