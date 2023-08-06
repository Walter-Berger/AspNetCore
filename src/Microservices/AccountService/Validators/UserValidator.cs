namespace AccountService.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationErrorDetails.InvalidEmail);

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.BirthDateTimestampUnix)
            .NotEmpty()
            .Must(BeAValidBirthDate)
            .WithMessage(ValidationErrorDetails.InvalidBirthDate);
    }

    private bool BeAValidBirthDate(long unixTimestamp)
    {
        var tooOld = DateTimeOffset.Now.AddYears(-100).ToUnixTimeSeconds();
        var tooYoung = DateTimeOffset.Now.ToUnixTimeSeconds();
        return unixTimestamp > tooOld && unixTimestamp < tooYoung;
    }
}
