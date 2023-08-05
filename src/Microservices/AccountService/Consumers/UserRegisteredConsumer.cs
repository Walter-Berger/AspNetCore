using ECommerce.Contracts.Events;

namespace AccountService.Consumers;

public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly DatabaseContext _databaseContext;

    public UserRegisteredConsumer(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var message = context.Message;

        var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Email == message.Email);

        if (user != null)
        {
            return;
        }

        user = new User(
            id: Guid.NewGuid(),
            email: message.Email,
            firstName: message.FirstName,
            lastName: message.LastName,
            birthDateTimestampUnix: default);

        await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();
    }
}
