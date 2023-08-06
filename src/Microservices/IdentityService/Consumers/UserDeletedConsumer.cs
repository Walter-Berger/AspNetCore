namespace IdentityService.Consumers;

public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserDeletedConsumer(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
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
