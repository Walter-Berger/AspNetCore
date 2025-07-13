namespace Common.ErrorDetails;

public static class ErrorDetails
{
    // 404 errors
    public const string UserNotFound = "No user found with the given id.";
    public const string UsersNotFound = "There were no users found in database.";
    public const string EmptyAuthHeader = "Authorization header must not be empty.";
    public const string InvalidAuthHeaderFormat = "Invalid Authorization header format. Must start with Basic: ";
    public const string InvalidCredentialsFormat = "Invalid credentials format. Must only contain username and password credentials.";
    public const string PasswordMustMatch = "Password's do not match.";
    public const string LoginFailed = "Incorrect username or password.";

    // 409 errors
    public const string EmailAlreadyExists = "Email address already exists.";

    // 500 errors
    public const string CouldNotSaveChanges = "Something went wrong while saving changes in database.";
}
