using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Consumers;

public class DeleteUserConsumer : IConsumer<DeleteUserEvent>
{
    private readonly UserManager<IdentityUser> _userManager;

    public DeleteUserConsumer(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<DeleteUserEvent> context)
    {
        var message = context.Message;

        var user = await _userManager.FindByEmailAsync(message.Email);

        if (user == null)
        {
            return;
        }

        await _userManager.DeleteAsync(user);
    }
}
