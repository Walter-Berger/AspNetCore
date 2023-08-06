using AccountService.Exceptions.ErrorDetails;
using Contracts.Events;

namespace AccountService.Features.DeleteUser;

public class DeleteUserCmdHandler : IRequestHandler<DeleteUserCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;
    private readonly IPublishEndpoint _publisher;

    public DeleteUserCmdHandler(DatabaseContext databaseContext, IPublishEndpoint publisher)
    {
        _databaseContext = databaseContext;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(DeleteUserCmd request, CancellationToken cancellationToken)
    {
        //check if user exists
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.UserNotFound);

        // create an event which will be publisherd to rabbitmq broker
        var userDeletedEvent = new UserDeletedEvent(Email: user.Email);

        await _publisher.Publish(userDeletedEvent, cancellationToken);

        // remove user from database
        _databaseContext.Users.Remove(user);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
