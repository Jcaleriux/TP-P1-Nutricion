namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// Repository that manages menu persistence through CSV files.
    /// </summary>
    public class MenuRepository : IRepository<Menu>
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuRepository"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public MenuRepository(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads menu data from the file.
        /// </summary>
        /// <returns>The loaded menu data.</returns>
        public List<Menu> LoadData()
        {
            if (string.IsNullOrWhiteSpace(this.filePath) || !File.Exists(this.filePath))
            {
                throw new FileNotFoundException($"The file '{this.filePath}' was not found.");
            }

            var data = new List<Menu>();
            var lines = File.ReadAllLines(this.filePath);

            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                var lineElements = lines[i].Split(',');
                var menu = new Menu(lineElements);
                data.Add(menu);
            }

            return data;
        }

        /// <summary>
        /// Saves menu data to the file.
        /// </summary>
        /// <param name="data">The menu data.</param>
        /// <returns>True if the data is saved successfully; otherwise, false.</returns>
        public bool SaveData(List<Menu> data)
        {
            var lines = new List<string>
            {
                "MenuId,UserId,Date",
            };

            foreach (var menu in data)
            {
                lines.Add(
                    $"{menu.MenuId},{menu.UserId},{menu.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)}");
            }

            File.WriteAllLines(this.filePath, lines);
            return true;
        }
    }
}
