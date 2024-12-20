﻿using Common.ErrorDetails;
using Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Features.Register;

public class RegisterCmdHandler : IRequestHandler<RegisterCmd, Unit>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IPublishEndpoint _publisher;

    public RegisterCmdHandler(UserManager<IdentityUser> userManager, IPublishEndpoint publisher)
    {
        _userManager = userManager;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(RegisterCmd request, CancellationToken cancellationToken)
    {
        // check if both passwords match
        if (!request.Password.Equals(request.ConfirmPassword))
        {
            throw new Exception(ErrorDetails.PasswordMustMatch);
        }

        // create new user
        var identityUser = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        // add the user to database
        var identityResult = await _userManager.CreateAsync(identityUser, request.Password);
        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors.Select(x => x.Description).Distinct();
            throw new Exception(errors.First());
        }

        // create an event which will be published to rabbitmq broker
        var userRegisteredEvent = new UserRegisteredEvent(
            Id: Guid.Parse(identityUser.Id),
            Email: request.Email,
            FirstName: request.FirstName,
            LastName: request.LastName);

        await _publisher.Publish(userRegisteredEvent, cancellationToken);

        return Unit.Value;
    }
}