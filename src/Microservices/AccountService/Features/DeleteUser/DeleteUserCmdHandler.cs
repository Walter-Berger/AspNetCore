namespace AccountService.Features.DeleteUser;

public class DeleteUserCmdHandler : IRequestHandler<DeleteUserCmd, Unit>
{
    private readonly DatabaseContext _databaseContext;

    public DeleteUserCmdHandler(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Unit> Handle(DeleteUserCmd request, CancellationToken cancellationToken)
    {
        //check if user exists
        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(ErrorDetails.UserNotFound);

        // remove user from database
        _databaseContext.Users.Remove(user);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
