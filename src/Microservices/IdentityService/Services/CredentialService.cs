namespace IdentityService.Services;

public class CredentialService : ICredentialService
{
    public (string, string) ExtractUsernameAndPassword(string authorizationHeader)
    {
        // check if header is null or empty
        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            throw new BadHttpRequestException(ErrorDetails.EmptyAuthHeader);
        }

        // check if header has the right format
        if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            throw new BadHttpRequestException(ErrorDetails.InvalidAuthHeaderFormat);
        }

        // since we do not want the substring "Basic ", we start reading the auth header at index 6
        string encodedCredentials = authorizationHeader[6..].Trim();
        string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
        string[] credentialsArray = decodedCredentials.Split(':', 2);

        // check if the credentials have the right format
        if (credentialsArray.Length != 2)
        {
            throw new BadHttpRequestException(ErrorDetails.InvalidCredentialsFormat);
        }

        // get username und email from the credentials
        string username = credentialsArray[0];
        string password = credentialsArray[1];

        // return them
        return (username, password);
    }
}
