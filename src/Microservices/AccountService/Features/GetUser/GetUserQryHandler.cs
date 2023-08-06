namespace AccountService.Features.GetUser;

public class GetUserQryHandler : IRequestHandler<GetUserQry, GetUserQryResult>
{
    private readonly DatabaseContext _databaseContext;
    private readonly ITimeFactory _timeFactory;

    public GetUserQryHandler(DatabaseContext databaseContext, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _timeFactory = timeFactory;
    }

    public async Task<GetUserQryResult> Handle(GetUserQry request, CancellationToken cancellationToken)
    {
        // check if user exists in database
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.UserNotFound);

        var result = new GetUserQryResult(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            BirthDate: _timeFactory.UnixTimeToDateString(user.BirthDateTimestampUnix)
        );

        return result;
    }
}
