namespace ClassController
{
    using ClassController.Abstractions;
    using System.Collections.Generic;

    /// <summary>
    /// Class in charge of handling the data operation by files.
    /// </summary>
    public class FileHandler<T> : IDataHandler<T>
    {
        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The loaded data.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<T> LoadData(string filePath)
        {
            return new List<T>();
        }

        /// <summary>
        /// Saves the data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// True if the data is saved successfully; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SaveData(string filePath, List<T> data)
        {
            return true;
        }
    }
}
