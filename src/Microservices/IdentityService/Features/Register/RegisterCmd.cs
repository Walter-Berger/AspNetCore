namespace IdentityService.Features.Register;

public record RegisterCmd(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string ConfirmPassword) : IRequest<Unit>;
