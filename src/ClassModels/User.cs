namespace ClassModels
{
    /// <summary>
    /// Model to represetn the user of the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Email">The email.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Goal">The goal.</param>
        /// <param name="ActivityLevel">The activity level.</param>
        /// <param name="WeightKg">The weight kg.</param>
        /// <param name="HeightCm">The height cm.</param>
        /// <param name="Age">The age.</param>
        /// <param name="Sex">The sex.</param>
        /// <param name="DietType">Type of the diet.</param>
        ///  <param name="Role">The role.</param>
        ///  <param name="IsActive">Indicates whether the user is active.</param>
        public User( 
            int UserId, 
            string Name,
            string Email,
            string Password,
            string Goal,
            string ActivityLevel,
            decimal WeightKg,
            decimal HeightCm,
            int Age,
            string Sex,
            string DietType,
            string Role = "User",
            bool IsActive = true
            ) 
        { 
            this.UserId = UserId;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.Goal = Goal;
            this.ActivityLevel = ActivityLevel;
            this.WeightKg = WeightKg;
            this.HeightCm = HeightCm;
            this.Age = Age;
            this.Sex = Sex;
            this.DietType = DietType;
            this.Role = Role;
            this.IsActive = IsActive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userData">The user data.</param>
        public User(string[] userData)
        {
            this.UserId = int.Parse(userData[0]);
            this.Name = userData[1];
            this.Email = userData[2];
            this.Password = userData[3];
            this.Goal = userData[4];
            this.ActivityLevel = userData[5];
            this.WeightKg = decimal.Parse(userData[6]);
            this.HeightCm = decimal.Parse(userData[7]);
            this.Age = int.Parse(userData[8]);
            this.Sex = userData[9];
            this.DietType = userData[10];
            this.Role = userData.Length > 11 ? userData[11] : "User";
            this.IsActive = userData.Length <= 12 || bool.Parse(userData[12]);
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>

        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the goal.
        /// </summary>
        /// <value>
        /// The goal.
        /// </value>
        public string Goal { get; set; }

        /// <summary>
        /// Gets or sets the activity level.
        /// </summary>
        /// <value>
        /// The activity level.
        /// </value>
        public string ActivityLevel { get; set; }

        /// <summary>
        /// Gets or sets the weight kg.
        /// </summary>
        /// <value>
        /// The weight kg.
        /// </value>
        public decimal WeightKg { get; set; }

        /// <summary>
        /// Gets or sets the height cm.
        /// </summary>
        /// <value>
        /// The height cm.
        /// </value>
        public decimal HeightCm { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the sex.
        /// </summary>
        /// <value>
        /// The sex.
        /// </value>
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets the type of the diet.
        /// </summary>
        /// <value>
        /// The type of the diet.
        /// </value>
        public string DietType { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; } = "User";
        /// <summary>
        /// Gets or sets a value indicating whether the entity is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if the entity is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; } = true;
    }
}
