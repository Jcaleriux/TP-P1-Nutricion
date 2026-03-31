namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Implements menu-related operations.
    /// </summary>
    public class MenuController : IMenuController
    {
        private readonly List<Menu> menus;
        private readonly List<MenuProduct> menuProducts;
        private readonly IRepository<Menu> menuRepository;
        private readonly IRepository<MenuProduct> menuProductRepository;
        private readonly IProductController productController;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuController"/> class.
        /// </summary>
        /// <param name="menuRepository">The menu repository.</param>
        /// <param name="menuProductRepository">The menu-product repository.</param>
        /// <param name="productController">The product controller.</param>
        public MenuController(
            IRepository<Menu> menuRepository,
            IRepository<MenuProduct> menuProductRepository,
            IProductController productController)
        {
            this.menuRepository = menuRepository;
            this.menuProductRepository = menuProductRepository;
            this.productController = productController;

            this.menus = menuRepository.LoadData();
            this.menuProducts = menuProductRepository.LoadData();
        }

        /// <summary>
        /// Gets all menus registered by a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of menus for the specified user.</returns>
        public List<Menu> GetMenusByUser(int userId)
        {
            return this.menus
                .Where(menu => menu.UserId == userId)
                .OrderBy(menu => menu.Date)
                .ToList();
        }

        /// <summary>
        /// Gets all products belonging to a menu.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <returns>The list of products in the specified menu.</returns>
        public List<MenuProduct> GetMenuProducts(int menuId)
        {
            return this.menuProducts
                .Where(menuProduct => menuProduct.MenuId == menuId)
                .ToList();
        }

        /// <summary>
        /// Creates a menu and its related products.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The menu date.</param>
        /// <param name="menuProducts">The products to include in the menu.</param>
        /// <returns>True if the menu is created successfully; otherwise, false.</returns>
        public bool CreateMenu(int userId, DateTime date, List<MenuProduct> menuProducts)
        {
            if (menuProducts == null || menuProducts.Count == 0)
            {
                return false;
            }

            var menuId = this.GetNextMenuId();
            var menu = new Menu(menuId, userId, date);
            this.menus.Add(menu);

            foreach (var menuProduct in menuProducts)
            {
                var item = new MenuProduct(
                    this.GetNextMenuProductId(),
                    menuId,
                    menuProduct.MealTime,
                    menuProduct.ProductId,
                    menuProduct.Quantity);

                this.menuProducts.Add(item);
            }

            var menuSaved = this.menuRepository.SaveData(this.menus);
            var menuProductsSaved = this.menuProductRepository.SaveData(this.menuProducts);

            return menuSaved && menuProductsSaved;
        }

        /// <summary>
        /// Updates an existing menu and its related products.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <param name="date">The updated menu date.</param>
        /// <param name="menuProducts">The updated products to include in the menu.</param>
        /// <returns>True if the menu is updated successfully; otherwise, false.</returns>
        public bool UpdateMenu(int menuId, DateTime date, List<MenuProduct> menuProducts)
        {
            if (menuProducts == null || menuProducts.Count == 0)
            {
                return false;
            }

            var existingMenu = this.menus.FirstOrDefault(item => item.MenuId == menuId);

            if (existingMenu is null)
            {
                return false;
            }

            existingMenu.Date = date;
            this.menuProducts.RemoveAll(item => item.MenuId == menuId);

            foreach (var menuProduct in menuProducts)
            {
                var item = new MenuProduct(
                    this.GetNextMenuProductId(),
                    menuId,
                    menuProduct.MealTime,
                    menuProduct.ProductId,
                    menuProduct.Quantity);

                this.menuProducts.Add(item);
            }

            var menuSaved = this.menuRepository.SaveData(this.menus);
            var menuProductsSaved = this.menuProductRepository.SaveData(this.menuProducts);

            return menuSaved && menuProductsSaved;
        }

        /// <summary>
        /// Calculates total calories and macronutrients for a list of menu products.
        /// </summary>
        /// <param name="menuProducts">The menu products.</param>
        /// <returns>The nutrition totals.</returns>
        public (decimal Calories, decimal Protein, decimal Carbs, decimal Fat) CalculateTotals(List<MenuProduct> menuProducts)
        {
            decimal totalCalories = 0;
            decimal totalProtein = 0;
            decimal totalCarbs = 0;
            decimal totalFat = 0;

            var products = this.productController.GetAllProducts();

            foreach (var menuProduct in menuProducts)
            {
                var product = products.FirstOrDefault(item => item.ProductId == menuProduct.ProductId);

                if (product is null)
                {
                    continue;
                }

                totalCalories += product.Calories * menuProduct.Quantity;
                totalProtein += product.Protein * menuProduct.Quantity;
                totalCarbs += product.Carbs * menuProduct.Quantity;
                totalFat += product.Fat * menuProduct.Quantity;
            }

            return (totalCalories, totalProtein, totalCarbs, totalFat);
        }

        /// <summary>
        /// Deletes a menu and all its related menu products.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <returns>True if the menu is deleted successfully; otherwise, false.</returns>
        public bool DeleteMenu(int menuId)
        {
            var menu = this.menus.FirstOrDefault(item => item.MenuId == menuId);

            if (menu is null)
            {
                return false;
            }

            this.menus.Remove(menu);
            this.menuProducts.RemoveAll(item => item.MenuId == menuId);

            var menuSaved = this.menuRepository.SaveData(this.menus);
            var menuProductsSaved = this.menuProductRepository.SaveData(this.menuProducts);

            return menuSaved && menuProductsSaved;
        }

        private int GetNextMenuId()
        {
            if (this.menus.Count == 0)
            {
                return 1;
            }

            return this.menus.Max(menu => menu.MenuId) + 1;
        }

        private int GetNextMenuProductId()
        {
            if (this.menuProducts.Count == 0)
            {
                return 1;
            }

            return this.menuProducts.Max(menuProduct => menuProduct.MenuProductId) + 1;
        }
    }
}
