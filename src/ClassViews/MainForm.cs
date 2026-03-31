namespace ClassViews
{
    using ClassController;

    /// <summary>
    /// View for the main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ProductController productController;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="productController"></param>
        public MainForm(ProductController productController)
        {
            this.InitializeComponent();
            this.productController = productController;
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            var productView = new ProductView(this.productController);
            productView.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
