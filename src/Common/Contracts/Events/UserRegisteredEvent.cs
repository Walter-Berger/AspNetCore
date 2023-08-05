namespace Contracts.Events;

public record UserRegisteredEvent(
    string Email,
    string FirstName,
    string LastName);
