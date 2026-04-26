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

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers();

        /// <summary>
        /// Resets the password for the specified user using the provided administrator credentials.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose password will be reset.</param>
        /// <param name="password">The new password to assign to the user. Cannot be null or empty.</param>
        /// <param name="adminUser">The administrator performing the password reset. Must have sufficient privileges.</param>
        /// <returns>true if the password was successfully reset; otherwise, false.</returns>
        public bool ResetPassword(int userId, string password, User adminUser);
        
        /// <summary>
        /// Deactivates the specified user using the provided administrator credentials.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to deactivate.</param>
        /// <param name="adminUser">The administrator performing the deactivation. Must have sufficient privileges.</param>
        /// <returns>true if the user was successfully deactivated; otherwise, false.</returns>
        public bool DeactivateUser(int userId, User adminUser);
        /// <summary>
        /// Activates the specified user account using the provided administrator credentials.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to activate. Must correspond to an existing user.</param>
        /// <param name="adminUser">The administrator performing the activation. Cannot be null and must have sufficient privileges.</param>
        /// <returns>true if the user account was successfully activated; otherwise, false.</returns>
        public bool ActivateUser(int userId, User adminUser);
    }
}
