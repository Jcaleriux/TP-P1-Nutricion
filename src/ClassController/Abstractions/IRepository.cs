namespace ClassController.Abstractions
{
    /// <summary>
    /// Defines a repository contract for loading and saving domain entities.
    /// </summary>
    /// <typeparam name="T">The type of entity managed by the repository.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Persists a collection of entities.
        /// </summary>
        /// <param name="data">The entities to save.</param>
        /// <returns>True if the data is saved successfully; otherwise, false.</returns>
        public bool SaveData(List<T> data);

        /// <summary>
        /// Loads the stored entities.
        /// </summary>
        /// <returns>The loaded data.</returns>
        public List<T> LoadData();
    }
}
