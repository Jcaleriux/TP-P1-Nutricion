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
        private LoginController loginController;
        private ProductController productController;

        private string Email => this.txtEmail.Text;
        private string Password => this.txtPassword.Text;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        /// <param name="loginController">The login controller.</param>
        public LoginView(LoginController loginController, ProductController productController)
        {
            this.InitializeComponent();
            this.loginController = loginController;
            this.productController = productController;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var authenticatedUser = this.loginController.Login(this.Email, this.Password);

            if (authenticatedUser is not null)
            {
                MessageBox.Show("Login successful! Welcome!");
                var principalForm = new MainForm(this.productController, authenticatedUser);
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
