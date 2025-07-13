using Common.ErrorDetails;
using Common.Exceptions;
using IdentityService.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Features.Login;

public static class Login
{
    public record Request(string UserName, string Password) : IRequest<Response>;
    public record Response(string AccessToken);

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtService;

        public Handler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            // check if email exists
            var identityUser = await _userManager.FindByEmailAsync(email: request.UserName)
                ?? throw new NotFoundException(ErrorDetails.LoginFailed);

            // check if password matches with user
            _ = await _signInManager.CheckPasswordSignInAsync(user: identityUser, password: request.Password, false)
                ?? throw new NotFoundException(ErrorDetails.LoginFailed);

            // generate access token
            var accessToken = _jwtService.GenerateAccessToken(identityUser);
            return new Response(accessToken);
        }
    }
}
