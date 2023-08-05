namespace AccountService.Features.GetAllUsers;

public record GetAllUsersQryResult(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string BirthDate);