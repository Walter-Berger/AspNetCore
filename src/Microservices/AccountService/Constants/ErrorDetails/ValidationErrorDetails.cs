namespace AccountService.Constants.ErrorDetails;

public static class ValidationErrorDetails
{
    public const string InvalidEmail = "Must be a valid email address.";
    public const string InvalidBirthDate = "Birthdate must not be in the future or more than 100 days in the past.";
}
