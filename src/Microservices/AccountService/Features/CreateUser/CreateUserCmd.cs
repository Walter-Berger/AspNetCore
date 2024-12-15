using MediatR;

namespace AccountService.Features.CreateUser;

public record CreateUserCmd(
    string Email,
    string FirstName,
    string LastName) : IRequest<Unit>;
