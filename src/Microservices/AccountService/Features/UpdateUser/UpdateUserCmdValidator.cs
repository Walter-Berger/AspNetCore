using Common.ErrorDetails;
using FluentValidation;

namespace AccountService.Features.UpdateUser;

public class UpdateUserCmdValidator : AbstractValidator<UpdateUserCmd>
{
    public UpdateUserCmdValidator()
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
