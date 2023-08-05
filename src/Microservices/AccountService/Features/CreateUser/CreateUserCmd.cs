namespace AccountService.Features.CreateUser;

public record CreateUserCmd(
    string Email,
    string FirstName,
    string LastName,
    DateOnly BirthDate) : IRequest<Unit>;
