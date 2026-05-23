using AccountService.Data;
using Common.ErrorDetails;
using Common.Exceptions;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Features.GetUser;

public interface IGetUserService
{
    Task<GetUserResponse> GetUser(GetUserQuery query, CancellationToken cancellationToken);
}
public class GetUserService : IGetUserService
{
    private readonly DatabaseContext _databaseContext;
    private readonly ITimeFactory _timeFactory;

    public GetUserService(DatabaseContext databaseContext, ITimeFactory timeFactory)
    {
        _databaseContext = databaseContext;
        _timeFactory = timeFactory;
    }

    public async Task<GetUserResponse> GetUser(GetUserQuery request, CancellationToken cancellationToken)
    {
        // check if user exists in database
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.UserNotFound);

        var result = new GetUserResponse(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName
            );

        return result;
    }
}
