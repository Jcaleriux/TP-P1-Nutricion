namespace ClassController.Abstractions
{
    using ClassModels;

    /// <summary>
    /// An interface to handle user-related operations.
    /// </summary>
    public interface IUserController
    {
        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authenticated if the login is successful; otherwise, null.</returns>
        public User? Login(string email, string password);

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// True if the register is successful; otherwise, false.
        /// </returns>
        public bool Register(User user);

    }
}
