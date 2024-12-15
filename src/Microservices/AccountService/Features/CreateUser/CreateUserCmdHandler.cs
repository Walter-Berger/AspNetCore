using AccountService.Data;
using AccountService.Models;
using Common.ErrorDetails;
using Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Features.CreateUser;

public class CreateUserCmdHandler : IRequestHandler<CreateUserCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;
    private readonly CreateUserCmdValidator _userValidator;

    public CreateUserCmdHandler(DatabaseContext databaseContext, CreateUserCmdValidator userValidator)
    {
        _databaseContext = databaseContext;
        _userValidator = userValidator;
    }

    public async Task<Unit> Handle(CreateUserCmd request, CancellationToken cancellationToken)
    {
        // check if input is valid
        await _userValidator.ValidateAndThrowAsync(request, cancellationToken);

        // check if email already exists in database
        bool emailAlreadyTaken = await _databaseContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (emailAlreadyTaken)
        {
            throw new DuplicationException(ErrorDetails.EmailAlreadyExists);
        }

        // create new user
        var user = new User(
            id: Guid.NewGuid(),
            email: request.Email,
            firstName: request.FirstName,
            lastName: request.LastName
            );

        // save user in database
        await _databaseContext.AddAsync(user, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
