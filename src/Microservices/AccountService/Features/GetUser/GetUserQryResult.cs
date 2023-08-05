namespace AccountService.Features.GetUser;

public record GetUserQryResult(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string BirthDate);