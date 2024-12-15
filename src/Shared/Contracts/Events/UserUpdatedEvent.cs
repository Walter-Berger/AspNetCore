namespace Contracts.Events;

public record UserUpdatedEvent(
    string Email,
    string FirstName,
    string LastName);


