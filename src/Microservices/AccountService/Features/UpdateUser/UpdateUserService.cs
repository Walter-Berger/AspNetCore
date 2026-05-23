using AccountService.Data;
using AccountService.Models;
using Common.ErrorDetails;
using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Features.UpdateUser;

public interface IUpdateUserService
{
    Task Update(UpdateUserCmd cmd, CancellationToken cancellationToken);
}

public class UpdateUserService : IUpdateUserService
{
    public readonly DatabaseContext _databaseContext;
    public readonly UpdateUserValidator _userValidator;
    public readonly ITimeFactory _timeFactory;

    public UpdateUserService(
        DatabaseContext databaseContext,
        UpdateUserValidator userValidator,
        ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _userValidator = userValidator;
        _timeFactory = timeFactory;
    }

    public async Task Update(UpdateUserCmd request, CancellationToken cancellationToken)
    {
        // check if updates are valid
        await _userValidator.ValidateAndThrowAsync(request, cancellationToken);

        // check if user exists
        var user = await _databaseContext.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
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
    }
}