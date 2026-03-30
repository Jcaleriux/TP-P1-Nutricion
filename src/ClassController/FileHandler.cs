namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;
    using System.Collections.Generic;

    /// <summary>
    /// Class in charge of handling the data operation by files.
    /// </summary>
    public class FileHandler<T> : IDataHandler<T>
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileHandler{T}"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public FileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The loaded data.
        /// </returns>
        public List<T> LoadData()
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            { 
                throw new FileNotFoundException($"The file '{filePath}' was not found.");
            }

            var data = new List<T>();
            var lines = File.ReadAllLines(filePath);

            for (var i = 1; i< lines.Length; i++)
            {
                var lineElements = lines[i].Split(',');
                var element = Activator.CreateInstance(typeof(T), new object[] { lineElements });
                data.Add((T)element);
            }

            return data;
        }

        /// <summary>
        /// Saves the data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// True if the data is saved successfully; otherwise, false.
        /// </returns>
        public bool SaveData(List<T> data)
        {
            var lines = new List<string>();
            lines.Add("UserId,Name,Email,Password,Goal,ActivityLevel,WeightKg,HeightCm,Age,Sex,DietType");

            foreach (var item in data)
            {
                var user = item as User;

                if (user != null)
                {
                    lines.Add(
                        $"{user.UserId},{user.Name},{user.Email},{user.Password},{user.Goal},{user.ActivityLevel},{user.WeightKg},{user.HeightCm},{user.Age},{user.Sex},{user.DietType}");
                }
            }

            File.WriteAllLines(filePath, lines);
            return true;
        }
    }
}
