namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;
    using System.Collections.Generic;

    /// <summary>
    /// Class in charge of handling the user data operation by files.
    /// </summary>
    public class UserFileHandler : IDataHandler<User>
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileHandler{T}"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public UserFileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads user the data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The loaded data.
        /// </returns>
        public List<User> LoadData()
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            { 
                throw new FileNotFoundException($"The file '{filePath}' was not found.");
            }

            var data = new List<User>();
            var lines = File.ReadAllLines(filePath);

            for (var i = 1; i< lines.Length; i++)
            {
                var lineElements = lines[i].Split(',');
                var user = new User(lineElements);
                data.Add(user);
            }

            return data;
        }

        /// <summary>
        /// Saves User the data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// True if the data is saved successfully; otherwise, false.
        /// </returns>
        public bool SaveData(List<User> data)
        {
            var lines = new List<string>();
            lines.Add("UserId,Name,Email,Password,Goal,ActivityLevel,WeightKg,HeightCm,Age,Sex,DietType");

            foreach (var user in data)
            {
                lines.Add(
                    $"{user.UserId},{user.Name},{user.Email},{user.Password},{user.Goal},{user.ActivityLevel},{user.WeightKg},{user.HeightCm},{user.Age},{user.Sex},{user.DietType}");
            }

            File.WriteAllLines(filePath, lines);
            return true;
        }
    }
}
