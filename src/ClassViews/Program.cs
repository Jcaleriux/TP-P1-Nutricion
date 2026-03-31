using ClassController;
using ClassModels;
using ClassViews.Configuration;

namespace ClassViews
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var (loginController, productController, menuController, nutritionCalculator) = LoadServices();
            using var loginView = new LoginView(loginController, productController, menuController, nutritionCalculator);
            Application.Run(loginView);
        }

        private static (LoginController LoginController, ProductController ProductController, MenuController MenuController, NutritionCalculator NutritionCalculator) LoadServices()
        {
            var userFileHandler = new UserFileHandler(ConfigurationItems.UserFilePath);
            var userController = new UserController(userFileHandler);
            var loginController = new LoginController(userController);

            var productFileHandler = new ProductFileHandler(ConfigurationItems.ProductsFilePath);
            var productController = new ProductController(productFileHandler);

            var menuFileHandler = new MenuFileHandler(ConfigurationItems.MenusFilePath);
            var menuProductFileHandler = new MenuProductFileHandler(ConfigurationItems.MenuProductsFilePath);
            var menuController = new MenuController(menuFileHandler, menuProductFileHandler, productController);

            var nutritionCalculator = new NutritionCalculator();

            return (loginController, productController, menuController, nutritionCalculator);
        }
    }
}
