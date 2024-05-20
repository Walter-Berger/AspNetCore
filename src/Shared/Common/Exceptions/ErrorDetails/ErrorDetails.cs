namespace Common.Exceptions.ErrorDetails;

public static class ErrorDetails
{
    // 404 errors
    public const string UserNotFound = "No user found with the given id in database.";
    public const string UsersNotFound = "There were no users found in database.";
    public const string BookNotFound = "Book was not found in database.";
    public const string BookSold = "Book has already been sold.";
    public const string BookLoaned = "Book is currently loaned by another user.";
    public const string EmptyAuthHeader = "Authorization header must not be empty.";
    public const string InvalidAuthHeaderFormat = "Invalid Authorization header format. Must start with Basic: ";
    public const string InvalidCredentialsFormat = "Invalid credentials format. Must only contain username and password credentials.";
    public const string PasswordMustMatch = "Password and confirm password do not match.";
    public const string LoginFailed = "Incorrect username or password.";

    // 409 errors
    public const string EmailAlreadyExists = "Email address already exists in database.";

    // 500 errors
    public const string CouldNotSaveChanges = "Something went wrong while saving changes in database.";
    public const string CannotDeleteLoanedBook = "Cannot delete a book which is currently loaned.";
    public const string CannotUpdateLoanedBook = "Cannot update a book which is currently loaned.";
}
