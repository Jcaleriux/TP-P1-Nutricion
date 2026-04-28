using ClassController.Abstractions;
using ClassModels;

namespace ClassControllerTest;

internal sealed class UserControllerDouble : IUserController
{
    public User? LoginResult { get; set; }

    public bool RegisterResult { get; set; } = true;

    public User? LastRegisteredUser { get; private set; }

    public (string Email, string Password)? LastLogin { get; private set; }

    public User? Login(string email, string password)
    {
        this.LastLogin = (email, password);
        return this.LoginResult;
    }

    public bool Register(User user)
    {
        this.LastRegisteredUser = user;
        return this.RegisterResult;
    }

    public List<User> GetAllUsers()
    {
        return new List<User>();
    }

    public bool ResetPassword(int userId, string password, User adminUser)
    {
        throw new NotSupportedException();
    }

    public bool DeactivateUser(int userId, User adminUser)
    {
        throw new NotSupportedException();
    }

    public bool ActivateUser(int userId, User adminUser)
    {
        throw new NotSupportedException();
    }
}
