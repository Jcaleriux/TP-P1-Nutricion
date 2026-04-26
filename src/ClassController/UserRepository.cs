namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;
    using System.Collections.Generic;

    /// <summary>
    /// Repository that manages user persistence through CSV files.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public UserRepository(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads the persisted users.
        /// </summary>
        /// <returns>The loaded users.</returns>
        public List<User> LoadData()
        {
            if (string.IsNullOrEmpty(this.filePath) || !File.Exists(this.filePath))
            {
                throw new FileNotFoundException($"The file '{this.filePath}' was not found.");
            }

            var data = new List<User>();
            var lines = File.ReadAllLines(this.filePath);

            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                var lineElements = lines[i].Split(',');
                var user = new User(lineElements);
                data.Add(user);
            }

            return data;
        }

        /// <summary>
        /// Saves the provided users.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>True if the data is saved successfully; otherwise, false.</returns>
        public bool SaveData(List<User> data)
        {
            var lines = new List<string>
            {
                "UserId,Name,Email,Password,Goal,ActivityLevel,WeightKg,HeightCm,Age,Sex,DietType,Role",
            };

            foreach (var user in data)
            {
                lines.Add(
                    $"{user.UserId},{user.Name},{user.Email},{user.Password},{user.Goal},{user.ActivityLevel},{user.WeightKg},{user.HeightCm},{user.Age},{user.Sex},{user.DietType},{user.Role}");
            }

            File.WriteAllLines(this.filePath, lines);
            return true;
        }
    }
}
