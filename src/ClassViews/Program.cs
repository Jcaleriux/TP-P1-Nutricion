using ClassController;
using ClassController.Abstractions;
using ClassModels;
using ClassViews.Configuration;

namespace ClassViews
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var (loginController, productController, menuController, nutritionStatisticsController) = LoadServices();
            using var loginView = new LoginView(
                loginController,
                productController,
                menuController,
                nutritionStatisticsController);

            Application.Run(loginView);
        }

        private static (
            LoginController LoginController,
            ProductController ProductController,
            MenuController MenuController,
            INutritionStatisticsController NutritionStatisticsController) LoadServices()
        {
            var userRepository = new UserRepository(ConfigurationItems.UserFilePath);
            var userController = new UserController(userRepository);
            var loginController = new LoginController(userController);

            var productRepository = new ProductRepository(ConfigurationItems.ProductsFilePath);
            var productController = new ProductController(productRepository);

            var menuRepository = new MenuRepository(ConfigurationItems.MenusFilePath);
            var menuProductRepository = new MenuProductRepository(ConfigurationItems.MenuProductsFilePath);
            var menuController = new MenuController(menuRepository, menuProductRepository, productController);

            var nutritionStatisticsController = new NutritionStatisticsController(menuController);

            return (loginController, productController, menuController, nutritionStatisticsController);
        }
    }
}
