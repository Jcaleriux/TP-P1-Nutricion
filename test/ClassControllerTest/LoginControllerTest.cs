using ClassController;
using ClassController.Abstractions;
using ClassModels;
using Moq;

namespace ClassControllerTest;

/// <summary>
/// Test class for <see cref="LoginController"/>. Tests the login and registration functionality of the controller, 
/// ensuring it correctly interacts with the IUserController dependency and handles various scenarios 
/// as successful authentication, failed authentication, successful registration, and duplicate registration.
/// </summary>
[TestClass]
public class LoginControllerTest
{
    /// <summary>
    /// When user controller authenticates returns user.
    /// </summary>
    [TestMethod]
    public void Login_WhenUserControllerAuthenticates_ReturnsUser()
    {
        // Arrange
        var expectedUser = TestDataFactory.CreateStandardUser();
        var userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
        userControllerMock
            .Setup(controller => controller.Login("user@test.com", "Password123"))
            .Returns(expectedUser);
        var controller = new LoginController(userControllerMock.Object);

        // Act
        var result = controller.Login("user@test.com", "Password123");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreSame(expectedUser, result);
        userControllerMock.Verify(
            mock => mock.Login("user@test.com", "Password123"),
            Times.Once);
    }

    /// <summary>
    /// Logins the when credentials are invalid returns null.
    /// </summary>
    [TestMethod]
    public void Login_WhenCredentialsAreInvalid_ReturnsNull()
    {
        // Arrange
        var userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
        userControllerMock
            .Setup(controller => controller.Login("user@test.com", "bad-password"))
            .Returns((User?)null);
        var controller = new LoginController(userControllerMock.Object);

        // Act
        var result = controller.Login("user@test.com", "bad-password");

        // Assert
        Assert.IsNull(result);
        userControllerMock.Verify(
            mock => mock.Login("user@test.com", "bad-password"),
            Times.Once);
    }

    /// <summary>
    /// Registers the when user controller accepts user returns true.
    /// </summary>
    [TestMethod]
    public void Register_WhenUserControllerAcceptsUser_ReturnsTrue()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser();
        var userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
        userControllerMock
            .Setup(controller => controller.Register(user))
            .Returns(true);
        var controller = new LoginController(userControllerMock.Object);

        // Act
        var result = controller.Register(user);

        // Assert
        Assert.IsTrue(result);
        userControllerMock.Verify(
            mock => mock.Register(user),
            Times.Once);
    }

    /// <summary>
    /// Registers the when user controller rejects duplicate returns false.
    /// </summary>
    [TestMethod]
    public void Register_WhenUserControllerRejectsDuplicate_ReturnsFalse()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser();
        var userControllerMock = new Mock<IUserController>(MockBehavior.Strict);
        userControllerMock
            .Setup(controller => controller.Register(user))
            .Returns(false);
        var controller = new LoginController(userControllerMock.Object);

        // Act
        var result = controller.Register(user);

        // Assert
        Assert.IsFalse(result);
        userControllerMock.Verify(
            mock => mock.Register(user),
            Times.Once);
    }
}
