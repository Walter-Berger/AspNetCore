namespace IdentityService.Services;

public class JwtService : IJwtService
{
    private readonly byte[] _jwtKey;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;

    public JwtService(byte[] jwtKey, string jwtIssuer, string jwtAudience)
    {
        _jwtKey = jwtKey;
        _jwtIssuer = jwtIssuer;
        _jwtAudience = jwtAudience;
    }

    public string GenerateAccessToken(IdentityUser user)
    {
        // create additional claims
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };

        // create signing credentials
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(_jwtKey),
            SecurityAlgorithms.HmacSha512Signature);

        // create security token
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            notBefore: DateTime.Now,
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            signingCredentials: signingCredentials);

        // return the token
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(securityToken);
    }
}
