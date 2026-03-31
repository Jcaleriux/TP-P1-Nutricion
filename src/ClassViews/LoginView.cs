namespace ClassViews
{
    using ClassController;
    using ClassModels;

    /// <summary>
    /// View for the login form.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class LoginView : Form
    {
        private readonly LoginController loginController;
        private readonly ProductController productController;
        private readonly MenuController menuController;
        private readonly NutritionCalculator nutritionCalculator;

        private string Email => this.txtEmail.Text;
        private string Password => this.txtPassword.Text;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        /// <param name="loginController">The login controller.</param>
        /// <param name="productController">The product controller.</param>
        /// <param name="menuController">The menu controller.</param>
        /// <param name="nutritionCalculator">The nutrition calculator.</param>
        public LoginView(
            LoginController loginController,
            ProductController productController,
            MenuController menuController,
            NutritionCalculator nutritionCalculator)
        {
            this.InitializeComponent();
            this.loginController = loginController;
            this.productController = productController;
            this.menuController = menuController;
            this.nutritionCalculator = nutritionCalculator;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var authenticatedUser = this.loginController.Login(this.Email, this.Password);

            if (authenticatedUser is not null)
            {
                MessageBox.Show("Login successful! Welcome!");
                var principalForm = new MainForm(
                    this.productController,
                    this.menuController,
                    this.nutritionCalculator,
                    authenticatedUser);

                principalForm.FormClosed += (_, _) => this.Close();
                principalForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your email and password.");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using var registerView = new RegisterView(this.loginController);
            registerView.ShowDialog();
        }
    }
}
