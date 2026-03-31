namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Controller for Login operations.
    /// </summary>
    public class LoginController
    {
        private readonly IUserController userController;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="userController">The user controller.</param>
        public LoginController(IUserController userController) 
        {
            this.userController = userController;
        }

        /// <summary>
        /// Logins the specified userName.
        /// </summary>
        /// <param name="userName">The userName.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authenticated user if the login is successful; otherwise, null.</returns>
        public User? Login(string userName, string password)
        {
            return this.userController.Login(userName,password);
        }

        /// <summary>
        /// Registers the specified userName.
        /// </summary>
        /// <param name="userName">The userName.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the registration is successful; otherwise, false.</returns>
        public bool Register(User user)
        {
            return this.userController.Register(user);
        }

    }
}
