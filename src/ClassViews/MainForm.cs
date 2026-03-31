namespace ClassViews
{
    using ClassController;
    using ClassModels;  

    /// <summary>
    /// View for the main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly User currentUser;
        private readonly ProductController productController;

        /// <summary>
        /// Initializes a new instance of the MainForm.
        /// </summary>
        /// <param name="productController">The controller used to manage product-related operations within the form. Cannot be null.</param>
        /// <param name="currentUser">The user who is currently logged in and interacting with the form. Cannot be null.</param>
        public MainForm(ProductController productController, User currentUser)
        {
            this.InitializeComponent();
            this.productController = productController;
            this.currentUser = currentUser;
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            this.InitializeComponent();
            this.productController = productController;
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            this.lblWelcome.Text = $"Welcome {this.currentUser.Name}";
        }
    }
}
