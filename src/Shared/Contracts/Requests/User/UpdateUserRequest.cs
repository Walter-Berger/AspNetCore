namespace Contracts.Requests;

public record UpdateUserRequest(
    string Email,
    string FirstName,
    string LastName,
    DateOnly BirthDate);

