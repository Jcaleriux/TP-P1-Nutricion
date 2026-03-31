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
        private readonly IDataHandler<User> dataHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="dataHandler">The data handler.</param>
        public UserController(IDataHandler<User> dataHandler)
        {
            this.dataHandler = dataHandler;
            this.users = dataHandler.LoadData();
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// True if the login is successful; otherwise, false.
        /// </returns>
        public bool Login(string email, string password)
        {
            if (this.users != null && this.ExistsUser(email, password))
            {
                return true;
            }
            return false;
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
                var result = this.dataHandler.SaveData(this.users);
                return result;
            }
            return false;
        }

        private bool ExistsUser(string email, string password)
        {
            foreach (var user in this.users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return true;
                }
            }

            return false;
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
