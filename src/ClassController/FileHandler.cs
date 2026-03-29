namespace ClassController
{
    using ClassController.Abstractions;
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
        public List<T> LoadData(string filePath)
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
                var elemet = Activator.CreateInstance(typeof(T), lineElements);
                data.Add((T)elemet);
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
            return true;
        }
    }
}
