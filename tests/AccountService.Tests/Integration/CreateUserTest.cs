using AccountService.Features.CreateUser;
using FluentValidation;

namespace AccountService.Tests.Integration;

public class CreateUserTest : BaseIntegrationTest
{
    public CreateUserTest(EndpointFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldAddUser_WhenDataIsValid()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@gmx.at", "John", "Xina");

        // Act
        await Sender.Send(command);

        // Assert
        var user = DbContext.Users.FirstOrDefault(p => p.Email == command.Email);
        Assert.NotNull(user);
    }

    [Fact]
    public async Task Create_ShouldThrowValidationError_WhenEmailIsInvalid()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina", "John", "Xina");

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task Create_ShouldThrowValidationError_WhenFirstNameIsEmpty()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@gmx.at", "", "Xina");

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task Create_ShouldThrowValidationError_WhenLastNameIsEmpty()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@gmx.at", "John", "");

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }
}