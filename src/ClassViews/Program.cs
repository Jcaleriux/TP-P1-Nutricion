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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var (loginController, productController) = LoadServices();
            Application.Run(new LoginView(loginController, productController));
        }
        private static (LoginController LoginController, ProductController ProductController )LoadServices()
        {
            var userFileHandler = new UserFileHandler(ConfigurationItems.UserFilePath);
            var userController = new UserController(userFileHandler);
            var loginController = new LoginController(userController);

            var productFileHandler = new ProductFileHandler(ConfigurationItems.ProductsFilePath);
            var productController = new ProductController(productFileHandler);

            return (loginController, productController);
        }
    }
}