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
            var loginController = LoadServices();
            Application.Run(new LoginView(loginController));
        }
        private static LoginController LoadServices()
        {
            var fileHandler = new FileHandler<User>(ConfigurationItems.UserFilePath);
            var userController = new UserController(fileHandler);
            var loginController = new LoginController(userController);
            return loginController;
        }
    }
}