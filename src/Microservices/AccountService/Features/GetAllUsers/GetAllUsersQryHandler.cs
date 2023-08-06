namespace AccountService.Features.GetAllUsers;

public class GetAllUsersQryHandler : IRequestHandler<GetAllUsersQry, List<GetAllUsersQryResult>>
{
    private readonly DatabaseContext _databaseContext;
    private readonly ITimeFactory _timeFactory;

    public GetAllUsersQryHandler(DatabaseContext databaseContext, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _timeFactory = timeFactory;
    }

    public async Task<List<GetAllUsersQryResult>> Handle(GetAllUsersQry request, CancellationToken cancellationToken)
    {
        // check if there are users in database
        var users = await _databaseContext.Users.ToListAsync(cancellationToken);
        if (!users.Any())
        {
            throw new NotFoundException(ErrorDetails.UsersNotFound);
        }

        var results = new List<GetAllUsersQryResult>();
        foreach (var user in users)
        {
            results.Add(new GetAllUsersQryResult(
                Id: user.Id,
                Email: user.Email,
                FirstName: user.FirstName,
                LastName: user.LastName,
                BirthDate: _timeFactory.UnixTimeToDateString(user.BirthDateTimestampUnix)));
        }

        return results;
    }
}
