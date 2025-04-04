﻿using AccountService.Data;
using AccountService.Models;
using Common.ErrorDetails;
using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Features.UpdateUser;

public class UpdateUserCmdHandler : IRequestHandler<UpdateUserCmd, Unit>
{
    public readonly DatabaseContext _databaseContext;
    public readonly UpdateUserCmdValidator _userValidator;
    public readonly ITimeFactory _timeFactory;

    public UpdateUserCmdHandler(DatabaseContext databaseContext, UpdateUserCmdValidator userValidator, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _userValidator = userValidator;
        _timeFactory = timeFactory;
    }

    public async Task<Unit> Handle(UpdateUserCmd request, CancellationToken cancellationToken)
    {
        // check if updates are valid
        await _userValidator.ValidateAndThrowAsync(request, cancellationToken);

        // check if user exists
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.UserNotFound);

        // create updated user
        var updatedUser = new User(
            id: request.Id,
            email: request.Email,
            firstName: request.FirstName,
            lastName: request.LastName
            );

        // update and save changes
        user.Update(updatedUser, _timeFactory.UnixTimeNow());
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
