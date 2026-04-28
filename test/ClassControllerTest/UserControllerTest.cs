using ClassController;
using ClassModels;
using Moq;

namespace ClassControllerTest;

/// <summary>
/// Test class for <see cref="UserController"/>, covering authentication, registration, password resets,
/// activation changes, and user retrieval.
/// </summary>
[TestClass]
public class UserControllerTest
{
    /// <summary>
    /// Logins with matching active credentials returns user.
    /// </summary>
    [TestMethod]
    public void Login_WithMatchingActiveCredentials_ReturnsUser()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(email: "user@test.com");
        var controller = this.CreateController([user]);

        // Act
        var result = controller.Login("user@test.com", "Password123");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(user.UserId, result.UserId);
    }

    /// <summary>
    /// Logins with invalid credentials returns null.
    /// </summary>
    [TestMethod]
    public void Login_WithInvalidCredentials_ReturnsNull()
    {
        // Arrange
        var controller = this.CreateController([TestDataFactory.CreateStandardUser()]);

        // Act
        var result = controller.Login("user@test.com", "wrong");

        // Assert
        Assert.IsNull(result);
    }

    /// <summary>
    /// Logins with inactive user returns null.
    /// </summary>
    [TestMethod]
    public void Login_WithInactiveUser_ReturnsNull()
    {
        // Arrange
        var controller = this.CreateController([TestDataFactory.CreateStandardUser(isActive: false)]);

        // Act
        var result = controller.Login("user@test.com", "Password123");

        // Assert
        Assert.IsNull(result);
    }

    /// <summary>
    /// Registers with unique email assigns next identifier and saves.
    /// </summary>
    [TestMethod]
    public void Register_WithUniqueEmail_AssignsNextIdAndSaves()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 7, email: "existing@test.com"),
        ]);
        var controller = this.CreateController(repositoryContext: repositoryContext);
        var newUser = TestDataFactory.CreateStandardUser(userId: 0, email: "new@test.com");

        // Act
        var result = controller.Register(newUser);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(8, newUser.UserId);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<User>>()), Times.Once);
    }

    /// <summary>
    /// Registers with duplicate email returns false.
    /// </summary>
    [TestMethod]
    public void Register_WithDuplicateEmail_ReturnsFalse()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<User>(
        [
            TestDataFactory.CreateStandardUser(email: "user@test.com"),
        ]);
        var controller = this.CreateController(repositoryContext: repositoryContext);

        // Act
        var result = controller.Register(TestDataFactory.CreateStandardUser(userId: 0, email: "user@test.com"));

        // Assert
        Assert.IsFalse(result);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<User>>()), Times.Never);
    }

    /// <summary>
    /// Resets password with admin and existing user updates password.
    /// </summary>
    [TestMethod]
    public void ResetPassword_WithAdminAndExistingUser_UpdatesPassword()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 10),
        ]);
        var controller = this.CreateController(repositoryContext: repositoryContext);

        // Act
        var result = controller.ResetPassword(10, "NewPass456", TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual("NewPass456", repositoryContext.LastSavedSnapshot!.Single().Password);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<User>>()), Times.Once);
    }

    /// <summary>
    /// Resets password with non-admin throws <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    [TestMethod]
    public void ResetPassword_WithNonAdmin_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var controller = this.CreateController([TestDataFactory.CreateStandardUser(userId: 10)]);

        // Act
        void action() => controller.ResetPassword(10, "NewPass456", TestDataFactory.CreateStandardUser(userId: 99));

        // Assert
        Assert.ThrowsException<UnauthorizedAccessException>(action);
    }

    /// <summary>
    /// Resets password with unknown user returns false.
    /// </summary>
    [TestMethod]
    public void ResetPassword_WithUnknownUser_ReturnsFalse()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<User>();
        var controller = this.CreateController(repositoryContext: repositoryContext);

        // Act
        var result = controller.ResetPassword(10, "NewPass456", TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsFalse(result);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<User>>()), Times.Never);
    }

    /// <summary>
    /// Deactivates user with admin stores inactive status.
    /// </summary>
    [TestMethod]
    public void DeactivateUser_WithAdmin_StoresInactiveStatus()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 10, isActive: true),
        ]);
        var controller = this.CreateController(repositoryContext: repositoryContext);

        // Act
        var result = controller.DeactivateUser(10, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsTrue(result);
        Assert.IsFalse(repositoryContext.LastSavedSnapshot!.Single().IsActive);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<User>>()), Times.Once);
    }

    /// <summary>
    /// Activates user with admin stores active status.
    /// </summary>
    [TestMethod]
    public void ActivateUser_WithAdmin_StoresActiveStatus()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 10, isActive: false),
        ]);
        var controller = this.CreateController(repositoryContext: repositoryContext);

        // Act
        var result = controller.ActivateUser(10, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsTrue(result);
        Assert.IsTrue(repositoryContext.LastSavedSnapshot!.Single().IsActive);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<User>>()), Times.Once);
    }

    /// <summary>
    /// Deactivates user with non-admin throws <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    [TestMethod]
    public void DeactivateUser_WithNonAdmin_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var controller = this.CreateController([TestDataFactory.CreateStandardUser(userId: 10)]);

        // Act
        void action() => controller.DeactivateUser(10, TestDataFactory.CreateStandardUser(userId: 99));

        // Assert
        Assert.ThrowsException<UnauthorizedAccessException>(action);
    }

    /// <summary>
    /// Activates user with unknown user returns false.
    /// </summary>
    [TestMethod]
    public void ActivateUser_WithUnknownUser_ReturnsFalse()
    {
        // Arrange
        var controller = this.CreateController([]);

        // Act
        var result = controller.ActivateUser(10, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Gets all users returns loaded users.
    /// </summary>
    [TestMethod]
    public void GetAllUsers_ReturnsLoadedUsers()
    {
        // Arrange
        var users = new List<User>
        {
            TestDataFactory.CreateStandardUser(userId: 1),
            TestDataFactory.CreateStandardUser(userId: 2, email: "other@test.com"),
        };
        var controller = this.CreateController(users);

        // Act
        var result = controller.GetAllUsers();

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreSame(users[0], result[0]);
    }

    private UserController CreateController(
        IEnumerable<User>? users = null,
        RepositoryMockContext<User>? repositoryContext = null)
    {
        return new UserController((repositoryContext ?? new RepositoryMockContext<User>(users)).Mock.Object);
    }
}
