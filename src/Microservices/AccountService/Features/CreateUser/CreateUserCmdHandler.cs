namespace AccountService.Features.CreateUser;

public class CreateUserCmdHandler : IRequestHandler<CreateUserCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;
    private readonly UserValidator _userValidator;
    private readonly ITimeFactory _timeFactory;

    public CreateUserCmdHandler(DatabaseContext databaseContext, UserValidator userValidator, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _userValidator = userValidator;
        _timeFactory = timeFactory;
    }

    public async Task<Unit> Handle(CreateUserCmd request, CancellationToken cancellationToken)
    {
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
            lastName: request.LastName,
            birthDateTimestampUnix: _timeFactory.DateOnlyToUnixTime(request.BirthDate)
        );

        // check if input is valid
        await _userValidator.ValidateAndThrowAsync(user, cancellationToken);

        // save user in database
        await _databaseContext.AddAsync(user, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
