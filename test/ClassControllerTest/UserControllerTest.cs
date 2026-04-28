using ClassController;
using ClassModels;

namespace ClassControllerTest;

[TestClass]
public class UserControllerTest
{
    [TestMethod]
    public void Login_WithMatchingActiveCredentials_ReturnsUser()
    {
        var user = TestDataFactory.CreateStandardUser(email: "user@test.com");
        var controller = this.CreateController([user]);

        var result = controller.Login("user@test.com", "Password123");

        Assert.IsNotNull(result);
        Assert.AreEqual(user.UserId, result.UserId);
    }

    [TestMethod]
    public void Login_WithInvalidCredentials_ReturnsNull()
    {
        var controller = this.CreateController([TestDataFactory.CreateStandardUser()]);

        var result = controller.Login("user@test.com", "wrong");

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Login_WithInactiveUser_ReturnsNull()
    {
        var controller = this.CreateController([TestDataFactory.CreateStandardUser(isActive: false)]);

        var result = controller.Login("user@test.com", "Password123");

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Register_WithUniqueEmail_AssignsNextIdAndSaves()
    {
        var repository = new InMemoryRepository<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 7, email: "existing@test.com"),
        ]);
        var controller = this.CreateController(repository: repository);
        var newUser = TestDataFactory.CreateStandardUser(userId: 0, email: "new@test.com");

        var result = controller.Register(newUser);

        Assert.IsTrue(result);
        Assert.AreEqual(8, newUser.UserId);
        Assert.AreEqual(1, repository.SaveCallCount);
    }

    [TestMethod]
    public void Register_WithDuplicateEmail_ReturnsFalse()
    {
        var repository = new InMemoryRepository<User>(
        [
            TestDataFactory.CreateStandardUser(email: "user@test.com"),
        ]);
        var controller = this.CreateController(repository: repository);

        var result = controller.Register(TestDataFactory.CreateStandardUser(userId: 0, email: "user@test.com"));

        Assert.IsFalse(result);
        Assert.AreEqual(0, repository.SaveCallCount);
    }

    [TestMethod]
    public void ResetPassword_WithAdminAndExistingUser_UpdatesPassword()
    {
        var repository = new InMemoryRepository<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 10),
        ]);
        var controller = this.CreateController(repository: repository);

        var result = controller.ResetPassword(10, "NewPass456", TestDataFactory.CreateAdminUser());

        Assert.IsTrue(result);
        Assert.AreEqual("NewPass456", repository.LastSavedSnapshot!.Single().Password);
    }

    [TestMethod]
    public void ResetPassword_WithNonAdmin_ThrowsUnauthorizedAccessException()
    {
        var controller = this.CreateController([TestDataFactory.CreateStandardUser(userId: 10)]);

        Assert.Throws<UnauthorizedAccessException>(
            () => controller.ResetPassword(10, "NewPass456", TestDataFactory.CreateStandardUser(userId: 99)));
    }

    [TestMethod]
    public void ResetPassword_WithUnknownUser_ReturnsFalse()
    {
        var repository = new InMemoryRepository<User>();
        var controller = this.CreateController(repository: repository);

        var result = controller.ResetPassword(10, "NewPass456", TestDataFactory.CreateAdminUser());

        Assert.IsFalse(result);
        Assert.AreEqual(0, repository.SaveCallCount);
    }

    [TestMethod]
    public void DeactivateUser_WithAdmin_StoresInactiveStatus()
    {
        var repository = new InMemoryRepository<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 10, isActive: true),
        ]);
        var controller = this.CreateController(repository: repository);

        var result = controller.DeactivateUser(10, TestDataFactory.CreateAdminUser());

        Assert.IsTrue(result);
        Assert.IsFalse(repository.LastSavedSnapshot!.Single().IsActive);
    }

    [TestMethod]
    public void ActivateUser_WithAdmin_StoresActiveStatus()
    {
        var repository = new InMemoryRepository<User>(
        [
            TestDataFactory.CreateStandardUser(userId: 10, isActive: false),
        ]);
        var controller = this.CreateController(repository: repository);

        var result = controller.ActivateUser(10, TestDataFactory.CreateAdminUser());

        Assert.IsTrue(result);
        Assert.IsTrue(repository.LastSavedSnapshot!.Single().IsActive);
    }

    [TestMethod]
    public void DeactivateUser_WithNonAdmin_ThrowsUnauthorizedAccessException()
    {
        var controller = this.CreateController([TestDataFactory.CreateStandardUser(userId: 10)]);

        Assert.Throws<UnauthorizedAccessException>(
            () => controller.DeactivateUser(10, TestDataFactory.CreateStandardUser(userId: 99)));
    }

    [TestMethod]
    public void ActivateUser_WithUnknownUser_ReturnsFalse()
    {
        var controller = this.CreateController([]);

        var result = controller.ActivateUser(10, TestDataFactory.CreateAdminUser());

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetAllUsers_ReturnsLoadedUsers()
    {
        var users = new List<User>
        {
            TestDataFactory.CreateStandardUser(userId: 1),
            TestDataFactory.CreateStandardUser(userId: 2, email: "other@test.com"),
        };
        var controller = this.CreateController(users);

        var result = controller.GetAllUsers();

        Assert.HasCount(2, result);
        Assert.AreSame(users[0], result[0]);
    }

    private UserController CreateController(IEnumerable<User>? users = null, InMemoryRepository<User>? repository = null)
    {
        return new UserController(repository ?? new InMemoryRepository<User>(users));
    }
}
