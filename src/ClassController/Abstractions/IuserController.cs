namespace ClassController.Abstractions
{
    using ClassModels;

    /// <summary>
    /// An interface to handle user-related operations.
    /// </summary>
    public interface IuserController
    {
        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the login is successful; otherwise, false.</returns>
        public bool Login(string username, string password);

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
