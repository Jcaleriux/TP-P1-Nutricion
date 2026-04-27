namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// Repository that manages product persistence through CSV files.
    /// </summary>
    public class ProductRepository : IRepository<Product>
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public ProductRepository(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads product data from the file.
        /// </summary>
        /// <returns>The loaded product data.</returns>
        public List<Product> LoadData()
        {
            if (string.IsNullOrWhiteSpace(this.filePath) || !File.Exists(this.filePath))
            {
                throw new FileNotFoundException($"The file '{this.filePath}' was not found.");
            }

            var data = new List<Product>();
            var lines = File.ReadAllLines(this.filePath);

            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                var lineElements = lines[i].Split(',');
                var product = new Product(lineElements);
                data.Add(product);
            }

            return data;
        }

        /// <summary>
        /// Saves product data to the file.
        /// </summary>
        /// <param name="data">The product data.</param>
        /// <returns>True if the data is saved successfully; otherwise, false.</returns>
        public bool SaveData(List<Product> data)
        {
            var lines = new List<string>
            {
                "ProductId,Name,Calories,Protein,Carbs,Fat,Unit,IsActive",
            };

            foreach (var product in data)
            {
                lines.Add(
                    $"{product.ProductId},{product.Name},{product.Calories.ToString(CultureInfo.InvariantCulture)},{product.Protein.ToString(CultureInfo.InvariantCulture)},{product.Carbs.ToString(CultureInfo.InvariantCulture)},{product.Fat.ToString(CultureInfo.InvariantCulture)},{product.Unit},{product.IsActive}");
            }

            File.WriteAllLines(this.filePath, lines);
            return true;
        }
    }
}
