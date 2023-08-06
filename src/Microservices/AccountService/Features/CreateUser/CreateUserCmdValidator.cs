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

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(BeAValidBirthDate)
            .WithMessage(ValidationErrorDetails.InvalidBirthDate);
    }

    private static bool BeAValidBirthDate(DateOnly birthDate)
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly tooOld = currentDate.AddYears(-100);
        return birthDate <= currentDate && birthDate >= tooOld;
    }
}
