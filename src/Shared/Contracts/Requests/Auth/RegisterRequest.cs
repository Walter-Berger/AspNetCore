namespace Contracts.Auth.Requests;

public record RegisterRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password, 
    string ConfirmPassword);
