using AccountService.Data;
using AccountService.Models;
using Contracts.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

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
            id: message.Id,
            email: message.Email,
            firstName: message.FirstName,
            lastName: message.LastName
            );

        await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();
    }
}
