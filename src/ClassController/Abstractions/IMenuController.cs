namespace ClassController.Abstractions
{
    using ClassModels;

    /// <summary>
    /// Interface to handle menu-related operations.
    /// </summary>
    public interface IMenuController
    {
        /// <summary>
        /// Gets all menus registered by a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of menus for the specified user.</returns>
        public List<Menu> GetMenusByUser(int userId);

        /// <summary>
        /// Gets all products belonging to a menu.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <returns>The list of products in the specified menu.</returns>
        public List<MenuProduct> GetMenuProducts(int menuId);

        /// <summary>
        /// Creates a menu and its related products.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The menu date.</param>
        /// <param name="menuProducts">The products to include in the menu.</param>
        /// <returns>True if the menu is created successfully; otherwise, false.</returns>
        public bool CreateMenu(int userId, DateTime date, List<MenuProduct> menuProducts);

        /// <summary>
        /// Updates an existing menu and its related products.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <param name="date">The updated menu date.</param>
        /// <param name="menuProducts">The updated products to include in the menu.</param>
        /// <returns>True if the menu is updated successfully; otherwise, false.</returns>
        public bool UpdateMenu(int menuId, DateTime date, List<MenuProduct> menuProducts);

        /// <summary>
        /// Calculates total calories and macronutrients for a list of menu products.
        /// </summary>
        /// <param name="menuProducts">The menu products.</param>
        /// <returns>The nutrition totals.</returns>
        public (decimal Calories, decimal Protein, decimal Carbs, decimal Fat) CalculateTotals(List<MenuProduct> menuProducts);

        /// <summary>
        /// Deletes a menu and all its related menu products.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <returns>True if the menu is deleted successfully; otherwise, false.</returns>
        public bool DeleteMenu(int menuId);
    }
}
