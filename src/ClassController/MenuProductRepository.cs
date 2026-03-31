namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// Repository that manages menu-product persistence through CSV files.
    /// </summary>
    public class MenuProductRepository : IRepository<MenuProduct>
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProductRepository"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public MenuProductRepository(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads menu product data from the file.
        /// </summary>
        /// <returns>The loaded menu product data.</returns>
        public List<MenuProduct> LoadData()
        {
            if (string.IsNullOrWhiteSpace(this.filePath) || !File.Exists(this.filePath))
            {
                throw new FileNotFoundException($"The file '{this.filePath}' was not found.");
            }

            var data = new List<MenuProduct>();
            var lines = File.ReadAllLines(this.filePath);

            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                var lineElements = lines[i].Split(',');
                var menuProduct = new MenuProduct(lineElements);
                data.Add(menuProduct);
            }

            return data;
        }

        /// <summary>
        /// Saves menu product data to the file.
        /// </summary>
        /// <param name="data">The menu product data.</param>
        /// <returns>True if the data is saved successfully; otherwise, false.</returns>
        public bool SaveData(List<MenuProduct> data)
        {
            var lines = new List<string>
            {
                "MenuProductId,MenuId,MealTime,ProductId,Quantity",
            };

            foreach (var menuProduct in data)
            {
                lines.Add(
                    $"{menuProduct.MenuProductId},{menuProduct.MenuId},{menuProduct.MealTime},{menuProduct.ProductId},{menuProduct.Quantity.ToString(CultureInfo.InvariantCulture)}");
            }

            File.WriteAllLines(this.filePath, lines);
            return true;
        }
    }
}
