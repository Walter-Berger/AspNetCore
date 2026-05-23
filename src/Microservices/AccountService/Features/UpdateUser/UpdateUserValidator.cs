using Common.ErrorDetails;
using FluentValidation;

namespace AccountService.Features.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCmd>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationErrorDetails.InvalidEmail);

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();
    }
}
