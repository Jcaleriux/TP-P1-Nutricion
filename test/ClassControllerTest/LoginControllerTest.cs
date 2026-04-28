using ClassController;

namespace ClassControllerTest;

[TestClass]
public class LoginControllerTest
{
    [TestMethod]
    public void Login_WhenUserControllerAuthenticates_ReturnsUser()
    {
        var userController = new UserControllerDouble
        {
            LoginResult = TestDataFactory.CreateStandardUser(),
        };
        var controller = new LoginController(userController);

        var result = controller.Login("user@test.com", "Password123");

        Assert.IsNotNull(result);
        Assert.AreEqual("user@test.com", userController.LastLogin!.Value.Email);
    }

    [TestMethod]
    public void Login_WhenCredentialsAreInvalid_ReturnsNull()
    {
        var controller = new LoginController(new UserControllerDouble { LoginResult = null });

        var result = controller.Login("user@test.com", "bad-password");

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Register_WhenUserControllerAcceptsUser_ReturnsTrue()
    {
        var user = TestDataFactory.CreateStandardUser();
        var userController = new UserControllerDouble { RegisterResult = true };
        var controller = new LoginController(userController);

        var result = controller.Register(user);

        Assert.IsTrue(result);
        Assert.AreSame(user, userController.LastRegisteredUser);
    }

    [TestMethod]
    public void Register_WhenUserControllerRejectsDuplicate_ReturnsFalse()
    {
        var controller = new LoginController(new UserControllerDouble { RegisterResult = false });

        var result = controller.Register(TestDataFactory.CreateStandardUser());

        Assert.IsFalse(result);
    }
}
