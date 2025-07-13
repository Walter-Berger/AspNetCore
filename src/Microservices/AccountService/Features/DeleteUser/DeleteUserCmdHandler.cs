using AccountService.Data;
using Common.ErrorDetails;
using Common.Exceptions;
using Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var userDeletedEvent = new DeleteUserEvent(Email: user.Email);

        await _publisher.Publish(userDeletedEvent, cancellationToken);

        // remove user from database
        _databaseContext.Users.Remove(user);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
