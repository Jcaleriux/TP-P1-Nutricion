namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Implements product-related operations such as registration, updating, activation, and deactivation.
    /// </summary>
    public class ProductController : IProductController
    {
        private readonly List<Product> products;
        private readonly IRepository<Product> repository;
        private readonly IRepository<Menu> menuRepository;
        private readonly IRepository<MenuProduct> menuProductRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="repository">The repository for product data access.</param>
        /// <param name="menuRepository">The repository for menu data access.</param>
        /// <param name="menuProductRepository">The repository for menu-product data access.</param>
        public ProductController(IRepository<Product> repository, IRepository<Menu> menuRepository, IRepository<MenuProduct> menuProductRepository)
        {
            this.repository = repository;
            this.products = repository.LoadData();
            this.menuRepository = menuRepository;
            this.menuProductRepository = menuProductRepository;
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
        /// Gets all active products.
        /// </summary>
        /// <returns>The list of active products.</returns>
        public List<Product> GetActiveProducts()
        {
            return this.products
                .Where(product => product.IsActive)
                .OrderBy(product => product.ProductId)
                .ToList();
        }

        /// <summary>
        /// Registers a new product.
        /// </summary>
        /// <param name="product">The product to register.</param>
        /// <returns>True if the product is registered successfully; otherwise, false.</returns>
        public bool Register(Product product, User user)
        {
            if (user.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admin can create products");
            }
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
        public bool Update(Product product, User user)
        {
            if (user.Role != "Admin")
            {
                throw new UnauthorizedAccessException("Only admin can update products");
            }
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
            existingProduct.IsActive = product.IsActive;

            return this.repository.SaveData(this.products);
        }

        /// <summary>
        /// Deactivates a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="user">The user performing the action.</param>
        /// <returns>True if the product was deactivated; otherwise, false.</returns>
        public bool DeactivateProduct(int productId, User user)
        {
            return this.SetProductStatus(productId, false, user, "deactivate");
        }

        /// <summary>
        /// Activates a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="user">The user performing the action.</param>
        /// <returns>True if the product was activated; otherwise, false.</returns>
        public bool ActivateProduct(int productId, User user)
        {
            return this.SetProductStatus(productId, true, user, "activate");
        }
        /// <summary>
        /// Gets the most consumed product for a user within a specified date range.
        /// </summary>
        public Product? GetMostConsumedProduct(DateTime startDate, DateTime endDate)
        {
            var menusInRange = this.menuRepository.LoadData().Where(m => m.Date.Date >= startDate.Date && m.Date.Date <= endDate.Date).ToList();
            if (!menusInRange.Any())
            {
                return null;
            }

            var menuIds = menusInRange.Select(m => m.MenuId).ToList();
            var menuProducts = this.menuProductRepository.LoadData().Where(mp => menuIds.Contains(mp.MenuId)).ToList();
            if (!menuProducts.Any())
            {
                return null;
            }

            var mostConsumed = menuProducts.GroupBy(mp => mp.ProductId).OrderByDescending(g => g.Sum(mp => mp.Quantity)).First();
            var productId = mostConsumed.Key;
            return this.products.FirstOrDefault(p => p.ProductId == productId);
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

        private bool SetProductStatus(int productId, bool isActive, User user, string action)
        {
            if (user.Role != "Admin")
            {
                throw new UnauthorizedAccessException($"Only admin can {action} products");
            }

            var existingProduct = this.products
                .FirstOrDefault(item => item.ProductId == productId);

            if (existingProduct is null)
            {
                return false;
            }

            existingProduct.IsActive = isActive;
            return this.repository.SaveData(this.products);
        }
    }
}
