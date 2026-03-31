namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Implements product-related operations.
    /// </summary>
    public class ProductController : IProductController
    {
        private readonly List<Product> products;
        private readonly IRepository<Product> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="repository">The product repository.</param>
        public ProductController(IRepository<Product> repository)
        {
            this.repository = repository;
            this.products = repository.LoadData();
        }

        /// <summary>
        /// Gets all registered products.
        /// </summary>
        /// <returns>The list of products.</returns>
        public List<Product> GetAllProducts()
        {
            return this.products
                .OrderBy(product => product.ProductId)
                .ToList();
        }

        /// <summary>
        /// Registers a new product.
        /// </summary>
        /// <param name="product">The product to register.</param>
        /// <returns>True if the product is registered successfully; otherwise, false.</returns>
        public bool Register(Product product)
        {
            if (this.ExistsProductName(product.Name, 0))
            {
                return false;
            }

            product.ProductId = this.GetNextProductId();
            this.products.Add(product);

            return this.repository.SaveData(this.products);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product with updated data.</param>
        /// <returns>True if the product is updated successfully; otherwise, false.</returns>
        public bool Update(Product product)
        {
            var existingProduct = this.products
                .FirstOrDefault(item => item.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                return false;
            }

            if (this.ExistsProductName(product.Name, product.ProductId))
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Calories = product.Calories;
            existingProduct.Protein = product.Protein;
            existingProduct.Carbs = product.Carbs;
            existingProduct.Fat = product.Fat;
            existingProduct.Unit = product.Unit;

            return this.repository.SaveData(this.products);
        }

        private bool ExistsProductName(string name, int excludedProductId)
        {
            var normalizedName = name.Trim().ToUpperInvariant();

            return this.products.Any(
                product => product.ProductId != excludedProductId &&
                    product.Name.Trim().ToUpperInvariant() == normalizedName);
        }

        private int GetNextProductId()
        {
            if (this.products.Count == 0)
            {
                return 1;
            }

            return this.products.Max(product => product.ProductId) + 1;
        }
    }
}
