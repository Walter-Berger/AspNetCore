using AccountService.Features.CreateUser;
using Common.ErrorDetails;
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
        Assert.Contains("John", user.FirstName);
        Assert.Contains("Xina", user.LastName);
    }

    [Fact]
    public async Task Create_ShouldThrowValidationError_WhenEmailIsInvalid()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@.", "John", "Xina");

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => Sender.Send(command));

        // Assert
        Assert.IsType<ValidationException>(exception);
        Assert.Contains(ValidationErrorDetails.InvalidEmail, exception.Message);
    }

    [Fact]
    public async Task Create_ShouldThrowValidationError_WhenFirstNameIsEmpty()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@gmx.at", "", "Xina");

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => Sender.Send(command));

        // Assert
        Assert.IsType<ValidationException>(exception);
        Assert.Contains(ValidationErrorDetails.InvalidFirstName, exception.Message);
    }

    [Fact]
    public async Task Create_ShouldThrowValidationError_WhenLastNameIsEmpty()
    {
        // Arrange
        var command = new CreateUserCmd("john.xina@gmx.at", "John", "");

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => Sender.Send(command));

        // Assert
        Assert.IsType<ValidationException>(exception);
        Assert.Contains(ValidationErrorDetails.InvalidLastName, exception.Message);
    }
}