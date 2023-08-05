namespace IdentityService.Constants.ErrorDetails;

public static class ErrorDetails
{
    // 400 errors
    public const string EmptyAuthHeader = "Authorization header must not be empty.";
    public const string InvalidAuthHeaderFormat = "Invalid Authorization header format. Must start with Basic: ";
    public const string InvalidCredentialsFormat = "Invalid credentials format. Must only contain username and password credentials.";
    public const string PasswordMustMatch = "Password and confirm password do not match.";
    public const string LoginFailed = "Incorrect username or password.";

    // 404 errors
    public const string UserNotFound = "No user found with the given id in database.";
}