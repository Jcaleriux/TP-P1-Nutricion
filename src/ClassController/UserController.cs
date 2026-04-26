namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Implements the user-related operations
    /// </summary>
    public class UserController : IUserController
    {
        private readonly List<User> users;
        private readonly IRepository<User> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="repository">The user repository.</param>
        public UserController(IRepository<User> repository)
        {
            this.repository = repository;
            this.users = repository.LoadData();
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authenticated user if the login is successful; otherwise, null</returns>
        public User? Login(string email, string password)
        {
            if (this.users != null)
            {
                return this.GetUserByCredentials(email, password);
            }
            return null;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// True if the register is successful; otherwise, false.
        /// </returns>
        public bool Register(User user)
        {
            if (this.users != null)
            {
                if (this.ExistsEmail(user.Email))
                {
                    return false;
                }

                user.UserId = this.GetNextUserId();
                this.users.Add(user);
                var result = this.repository.SaveData(this.users);
                return result;
            }
            return false;
        }

        /// <summary>
        /// This method allows an admin user to reset the password of another user. It checks if the adminUser has the "Admin" role before allowing the password reset. If the specified UserId does not exist, it returns false. If the password reset is successful, it saves the updated user list to the repository and returns true.
        /// </summary>
        /// <param name="password">The new password.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="adminUser">The admin user performing the reset.</param>
        /// <returns>True if the password reset is successful; otherwise, false.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public bool ResetPassword(int userId, string password, User adminUser)
        {
            if (adminUser.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admin users can reset passwords.");
            }

            var user = this.users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return false;
            }

            user.Password = password;

            return this.repository.SaveData(this.users);
        }

        /// <summary>
        /// Deactivates a user account identified by the specified user ID, if performed by an administrator.
        /// </summary>
        /// <param name="UserId">The unique identifier of the user to deactivate.</param>
        /// <param name="adminUser">The user performing the deactivation. Must have an administrator role.</param>
        /// <returns>true if the user was found and successfully deactivated; otherwise, false.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user performing the operation does not have administrator privileges.</exception>
        public bool DeactivateUser(int userId, User adminUser)
        {
            if (adminUser.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admin users can deactivate users.");
            }
            var user = this.users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            return this.repository.SaveData(this.users);
        }

        /// <summary>
        /// Activates a user account identified by the specified user ID, if the operation is performed by an
        /// administrator.
        /// </summary>
        /// <param name="UserId">The unique identifier of the user to activate.</param>
        /// <param name="adminUser">The user performing the activation. Must have an administrator role.</param>
        /// <returns>true if the user was found and successfully activated; otherwise, false.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user performing the operation does not have administrator privileges.</exception>
        public bool ActivateUser(int userId, User adminUser)
        {
            if (adminUser.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admin users can activate users.");
            }
            var user = this.users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }
            user.IsActive = true;
            return this.repository.SaveData(this.users);
        }

        /// <summary>
        /// Retrieves a list of all users in the system. This method can be used by administrators to view all registered users and their details.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public List<User> GetAllUsers()
        {
            return this.users;
        } 

        private User? GetUserByCredentials(string email, string password)
        {
            foreach (var user in this.users)
            {
                if (user.Email == email && user.Password == password)
                {
                    if (!user.IsActive)
                    {
                        return null;
                    }
                    return user;
                }
            }

            return null;
        }

        private bool ExistsEmail(string Email)
        {
            foreach (var user in this.users)
            {
                if (user.Email == Email)
                {
                    return true;
                }
            }
            return false;
        }
        private int GetNextUserId()
        {
            if (this.users.Count == 0)
            {
                return 1;
            }

            return this.users.Max(user => user.UserId) + 1;
        }
    } 
}
