namespace AccountService.Features.CreateUser;

public class CreateUserCmdValidator : AbstractValidator<CreateUserCmd>
{
    public CreateUserCmdValidator()
    {
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
