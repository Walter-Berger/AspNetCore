namespace Contracts.User.Requests;

public record UpdateUserRequest(
    string Email,
    string FirstName,
    string LastName);

