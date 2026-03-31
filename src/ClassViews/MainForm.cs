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
        /// <param name="productController">The product controller.</param>
        /// <param name="currentUser">The authenticated user.</param>
        public MainForm(ProductController productController, User currentUser)
        {
            this.InitializeComponent();
            this.productController = productController;
            this.currentUser = currentUser;
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            using var productView = new ProductView(this.productController);
            productView.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.lblWelcome.Text = $"Welcome {this.currentUser.Name}";
        }
    }
}
