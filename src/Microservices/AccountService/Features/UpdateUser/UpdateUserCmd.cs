namespace AccountService.Features.UpdateUser;

public record UpdateUserCmd(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    DateOnly BirthDate) : IRequest<Unit>;
