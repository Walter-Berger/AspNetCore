using Common.ErrorDetails;
using FluentValidation;

namespace AccountService.Features.CreateUser;

public class CreateUserCmdValidator : AbstractValidator<CreateUserCmd>
{
    public CreateUserCmdValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationErrorDetails.InvalidEmail)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage(ValidationErrorDetails.InvalidEmail);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(ValidationErrorDetails.InvalidFirstName);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(ValidationErrorDetails.InvalidLastName);
    }
}
