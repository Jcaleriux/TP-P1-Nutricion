namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Implements the user-related operations
    /// </summary>
    public class UserController : IuserController
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
        /// Logins the specified userName.
        /// </summary>
        /// <param name="userName">The userName.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// True if the login is successful; otherwise, false.
        /// </returns>
        public bool Login(string userName, string password)
        {
            if (this.users != null && this.ExistsUser(userName, password))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Registers the specified userName.
        /// </summary>
        /// <param name="userName">The userName.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// True if the login is successful; otherwise, false.
        /// </returns>
        public bool Register(string userName, string password)
        {
            if (this.users != null)
            {
                if (this.ExistsUser(userName, password))
                {
                    return false;
                }

                this.users.Add(new User(userName, password));
                var result = this.dataHandler.SaveData(this.users);
                return result;
            }
            return false;
        }

        private bool ExistsUser(string userName, string password)
        {
            foreach (var user in this.users)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    return true;
                }
            }
            return false;

        }
    } 
}
