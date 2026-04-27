using System.Globalization;

namespace ClassModels
{
    /// <summary>
    /// Model to represent the products of the system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="calories"></param>
        /// <param name="protein"></param>
        /// <param name="carbs"></param>
        /// <param name="fat"></param>
        /// <param name="unit"></param>
        public Product
            (
            int productId,
            string name,
            decimal calories,
            decimal protein,
            decimal carbs,
            decimal fat,
            string unit,
            bool isActive = true
            ) 
        {
            this.ProductId = productId;
            this.Name = name;
            this.Calories = calories;
            this.Protein = protein;
            this.Carbs = carbs;
            this.Fat = fat;
            this.Unit = unit;
            this.IsActive = isActive;
        }
        
        /// <summary>
        /// Initializes a new instance of the Product class using the specified product data array.
        /// </summary>
        /// <remarks>Each element in the productData array is parsed and assigned to the corresponding
        /// property. The method expects numeric values to be in a format compatible with invariant culture.</remarks>
        /// <param name="productData">An array of strings containing product information..</param>
        public Product(string[] productData)
        {
            if (productData.Length < 7)
            { 
                throw new ArgumentException("Invalid product data. Expected 7 elements.");
            }

            this.ProductId = int.Parse(productData[0], CultureInfo.InvariantCulture);
            this.Name = productData[1];
            this.Calories = decimal.Parse(productData[2], CultureInfo.InvariantCulture);
            this.Protein = decimal.Parse(productData[3], CultureInfo.InvariantCulture);
            this.Carbs = decimal.Parse(productData[4], CultureInfo.InvariantCulture);
            this.Fat = decimal.Parse(productData[5], CultureInfo.InvariantCulture);
            this.Unit = productData[6];
            this.IsActive = productData.Length <= 7 || bool.Parse(productData[7]);
        }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of calories contained in the item.
        /// </summary>
        public decimal Calories { get; set; }

        /// <summary>
        /// Gets or sets the amount of protein contained in the item.
        /// </summary>
        public decimal Protein { get; set; }

        /// <summary>
        /// Gets or sets the amount of carbohydrates associated with the item
        /// </summary>
        public decimal Carbs { get; set; }

        /// <summary>
        /// Gets or sets the amount of fat.
        /// </summary>
        public decimal Fat { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement associated with the value.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
