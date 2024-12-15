using Microsoft.AspNetCore.Identity;

namespace IdentityService.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(IdentityUser user);
}
