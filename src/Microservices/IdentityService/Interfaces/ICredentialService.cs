namespace IdentityService.Interfaces;

public interface ICredentialService
{
    (string, string) ExtractUsernameAndPassword(string authorizationHeader);
}
