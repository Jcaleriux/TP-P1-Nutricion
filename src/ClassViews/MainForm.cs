namespace ClassViews
{
    using ClassController;
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// View for the main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly User currentUser;
        private readonly ProductController productController;
        private readonly MenuController menuController;
        private readonly INutritionStatisticsController nutritionStatisticsController;

        /// <summary>
        /// Initializes a new instance of the MainForm.
        /// </summary>
        /// <param name="productController">The product controller.</param>
        /// <param name="menuController">The menu controller.</param>
        /// <param name="nutritionStatisticsController">The nutrition statistics controller.</param>
        /// <param name="currentUser">The authenticated user.</param>
        public MainForm(
            ProductController productController,
            MenuController menuController,
            INutritionStatisticsController nutritionStatisticsController,
            User currentUser)
        {
            this.InitializeComponent();
            this.productController = productController;
            this.menuController = menuController;
            this.nutritionStatisticsController = nutritionStatisticsController;
            this.currentUser = currentUser;
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            using var productView = new ProductView(this.productController);
            productView.ShowDialog();
        }

        private void btnManageMenus_Click(object sender, EventArgs e)
        {
            using var menuView = new MenuView(this.menuController, this.productController, this.currentUser);
            menuView.ShowDialog();
        }

        private void btnNutritionInfo_Click(object sender, EventArgs e)
        {
            using var nutritionInfoView = new NutritionInfoView(this.currentUser);
            nutritionInfoView.ShowDialog();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            using var statisticsView = new StatisticsView(
                this.currentUser,
                this.nutritionStatisticsController);

            statisticsView.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.lblWelcome.Text = $"Welcome {this.currentUser.Name}";
        }
    }
}
