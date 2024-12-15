namespace Contracts.User.Responses;

public record GetUserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);