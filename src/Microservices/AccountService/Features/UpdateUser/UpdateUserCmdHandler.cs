namespace AccountService.Features.UpdateUser;

public class UpdateUserCmdHandler : IRequestHandler<UpdateUserCmd, Unit>
{
    public readonly DatabaseContext _databaseContext;
    public readonly UserValidator _userValidator;
    public readonly ITimeFactory _timeFactory;

    public UpdateUserCmdHandler(DatabaseContext databaseContext, UserValidator userValidator, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _userValidator = userValidator;
        _timeFactory = timeFactory;
    }

    public async Task<Unit> Handle(UpdateUserCmd request, CancellationToken cancellationToken)
    {
        // check if user exists
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.UserNotFound);

        // create updated user
        var updatedUser = new User(
            id: request.Id,
            email: request.Email,
            firstName: request.FirstName,
            lastName: request.LastName,
            birthDateTimestampUnix: _timeFactory.DateOnlyToUnixTime(request.BirthDate)
        );

        // check if updates are valid
        await _userValidator.ValidateAndThrowAsync(updatedUser, cancellationToken);

        // update and save changes
        user.Update(updatedUser, _timeFactory.UnixTimeNow());
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
