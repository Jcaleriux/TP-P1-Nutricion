namespace ClassViews.Configuration
{
    /// <summary>
    /// Items for the configuration of the application.
    /// </summary>
    public static class ConfigurationItems
    {
        /// <summary>
        /// Gets the absolute path to the project root directory, relative to the application's base directory.
        /// </summary>
        /// <remarks>This value is determined by traversing up several directory levels from the
        /// application's base directory. The resulting path may vary depending on the application's deployment or
        /// execution context. Use this property when you need to locate files or resources relative to the project root
        /// during development or testing.</remarks>
        public static readonly string ProjectRootPath =
            Path.GetFullPath(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    "..",
                    "..",
                    ".."));

        /// <summary>
        /// Gets the full path to the application's data folder.
        /// </summary>
        /// <remarks>The data folder is located within the project root directory and is intended for
        /// storing application-specific data files. The path is constructed by combining the project root path with the
        /// "data" subdirectory.</remarks>
        public static readonly string DataFolderPath =
            Path.Combine(ProjectRootPath, "data");

        /// <summary>
        /// Gets the full file system path to the user data file.
        /// </summary>
        /// <remarks>The path is constructed by combining the application data folder path with the file
        /// name "users.csv". Use this value to access or store user-related data in a consistent location.</remarks>
        public static readonly string UserFilePath =
            Path.Combine(DataFolderPath, "users.csv");

        /// <summary>
        /// Gets the full file system path to the products data file.
        /// </summary>
        /// <remarks>The path is constructed by combining the application's data folder path with the file
        /// name "products.csv". Use this value to access or store product data in a consistent location.</remarks>
        public static readonly string ProductsFilePath =
            Path.Combine(DataFolderPath, "products.csv");

        /// <summary>
        /// Gets the full file system path to the menus data file.
        /// </summary>
        /// <remarks>The path is constructed by combining the application's data folder path with the file
        /// name "menus.csv". Use this value to access or store menu data in a consistent location.</remarks>
        public static readonly string MenusFilePath =
            Path.Combine(DataFolderPath, "menus.csv");

        /// <summary>
        /// Represents the file path to the menu products data file.
        /// </summary>
        /// <remarks>The path is constructed by combining the application's data folder path with the file
        /// name 'menuProducts.csv'. Use this value when reading from or writing to the menu products data
        /// file.</remarks>
        public static readonly string MenuProductsFilePath =
            Path.Combine(DataFolderPath, "menuProducts.csv");
    }
}
