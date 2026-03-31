using System.Globalization;

namespace ClassModels
{
    /// <summary>
    /// Model to represent a product included in a menu.
    /// </summary>
    public class MenuProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProduct"/> class.
        /// </summary>
        /// <param name="menuProductId">The menu product identifier.</param>
        /// <param name="menuId">The menu identifier.</param>
        /// <param name="mealTime">The meal time.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="quantity">The quantity consumed.</param>
        public MenuProduct(int menuProductId, int menuId, string mealTime, int productId, decimal quantity)
        {
            this.MenuProductId = menuProductId;
            this.MenuId = menuId;
            this.MealTime = mealTime;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProduct"/> class from CSV data.
        /// </summary>
        /// <param name="menuProductData">The menu product data.</param>
        public MenuProduct(string[] menuProductData)
        {
            if (menuProductData.Length < 5)
            {
                throw new ArgumentException("Invalid menu product data. Expected 5 elements.");
            }

            this.MenuProductId = int.Parse(menuProductData[0], CultureInfo.InvariantCulture);
            this.MenuId = int.Parse(menuProductData[1], CultureInfo.InvariantCulture);
            this.MealTime = menuProductData[2];
            this.ProductId = int.Parse(menuProductData[3], CultureInfo.InvariantCulture);
            this.Quantity = decimal.Parse(menuProductData[4], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets or sets the menu product identifier.
        /// </summary>
        public int MenuProductId { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// Gets or sets the meal time.
        /// </summary>
        public string MealTime { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity consumed.
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
