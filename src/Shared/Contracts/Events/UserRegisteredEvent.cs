namespace Contracts.Events;

public record UserRegisteredEvent(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
