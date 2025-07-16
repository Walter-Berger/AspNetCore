using Common.ErrorDetails;
using Common.Exceptions;
using IdentityService.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Features.Login;


public interface ILoginService
{
    Task<LoginResponse> Login(LoginQuery query, CancellationToken cancellationToken);
}

public class LoginService : ILoginService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtService _jwtService;

    public LoginService(UserManager<IdentityUser> userManager,
                        SignInManager<IdentityUser> signInManager,
                        IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Login(LoginQuery query, CancellationToken cancellationToken)
    {
        // check if email exists
        var identityUser = await _userManager.FindByEmailAsync(query.UserName)
            ?? throw new NotFoundException(ErrorDetails.LoginFailed);

        // check if password matches with user
        _ = await _signInManager.CheckPasswordSignInAsync(identityUser, query.Password, false)
            ?? throw new NotFoundException(ErrorDetails.LoginFailed);

        // generate access token
        var accessToken = _jwtService.GenerateAccessToken(identityUser);
        return new LoginResponse(accessToken);
    }
}
