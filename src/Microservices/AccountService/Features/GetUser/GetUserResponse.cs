namespace AccountService.Features.GetUser;

public record GetUserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);