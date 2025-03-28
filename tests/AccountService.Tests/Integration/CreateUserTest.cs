using AccountService.Features.CreateUser;

namespace AccountService.Tests.Integration;

public class CreateUserTest : BaseIntegrationTest
{
    public CreateUserTest(EndpointFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenUserIsValid()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@gmx.at", "John", "Xina");

        // Act
        await Sender.Send(command);

        // Assert
        var user = DbContext.Users.FirstOrDefault(p => p.Email == command.Email);
        Assert.NotNull(user);
    }
}