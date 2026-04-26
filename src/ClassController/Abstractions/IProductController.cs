using ClassModels;

namespace ClassController.Abstractions
{
    /// <summary>
    /// An interface to handle product-related operations.
    /// </summary>
    /// <remarks>The methods provide basic CRUD functionality for product entities and are intended to be used by higher-level
    /// application components to interact with product data.</remarks>
    public interface IProductController
    {
        /// <summary>
        /// Retrieves a list of all available products.
        /// </summary>
        /// <returns>A list of <see cref="Product"/> objects representing all products. The list will be empty if no products are
        /// available.</returns>
        public List<Product> GetAllProducts();

        /// <summary>
        /// Attempts to register the specified product in the system.
        /// </summary>
        /// <param name="product">The product to register. Cannot be null.</param>
        /// <returns>true if the product was successfully registered; otherwise, false.</returns>
        public bool Register(Product product, User user);

        /// <summary>
        /// Updates the specified product in the data store.
        /// </summary>
        /// <param name="product">The product to update. Cannot be null. The product must have a valid identifier corresponding to an existing
        /// entry.</param>
        /// <returns>true if the product was successfully updated; otherwise, false.</returns>
        public bool Update(Product product, User user);
    }
}
