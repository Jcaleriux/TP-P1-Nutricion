namespace ClassViews
{
    using ClassController;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// View for menu management operations.
    /// </summary>
    public partial class MenuView : Form
    {
        private readonly MenuController menuController;
        private readonly ProductController productController;
        private readonly User currentUser;
        private readonly List<MenuProduct> pendingMenuProducts;
        private int selectedMenuId;
        private int selectedPendingProductIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView"/> class.
        /// </summary>
        /// <param name="menuController">The menu controller.</param>
        /// <param name="productController">The product controller.</param>
        /// <param name="currentUser">The authenticated user.</param>
        public MenuView(MenuController menuController, ProductController productController, User currentUser)
        {
            this.InitializeComponent();
            this.menuController = menuController;
            this.productController = productController;
            this.currentUser = currentUser;
            this.pendingMenuProducts = new List<MenuProduct>();
        }

        private void MenuView_Load(object sender, EventArgs e)
        {
            this.LoadMealTimes();
            this.LoadProducts();
            this.LoadMenus();
            this.ResetEditor();
        }

        private void LoadMealTimes()
        {
            this.cmbMealTime.Items.Clear();
            this.cmbMealTime.Items.AddRange(new object[] { "Breakfast", "Snack", "Lunch", "Dinner" });
            this.cmbMealTime.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            var products = this.productController.GetAllProducts();
            this.cmbProducts.DataSource = null;
            this.cmbProducts.DataSource = products;
            this.cmbProducts.DisplayMember = "Name";
            this.cmbProducts.ValueMember = "ProductId";
            this.cmbProducts.SelectedIndex = -1;
        }

        private void LoadMenus()
        {
            var menuRows = this.menuController
                .GetMenusByUser(this.currentUser.UserId)
                .Select(menu =>
                {
                    var products = this.menuController.GetMenuProducts(menu.MenuId);
                    var totals = this.menuController.CalculateTotals(products);

                    return new MenuGridItem
                    {
                        MenuId = menu.MenuId,
                        Date = menu.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                        Calories = totals.Calories,
                        Protein = totals.Protein,
                        Carbs = totals.Carbs,
                        Fat = totals.Fat,
                    };
                })
                .ToList();

            this.dgvMenus.DataSource = null;
            this.dgvMenus.DataSource = menuRows;
            this.dgvMenus.ClearSelection();
        }

        private void LoadPendingProducts()
        {
            var products = this.productController.GetAllProducts();

            var rows = this.pendingMenuProducts
                .Select(menuProduct =>
                {
                    var product = products.FirstOrDefault(item => item.ProductId == menuProduct.ProductId);

                    return new PendingProductGridItem
                    {
                        MealTime = menuProduct.MealTime,
                        ProductName = product?.Name ?? "Unknown",
                        Quantity = menuProduct.Quantity,
                        Calories = (product?.Calories ?? 0) * menuProduct.Quantity,
                        Protein = (product?.Protein ?? 0) * menuProduct.Quantity,
                        Carbs = (product?.Carbs ?? 0) * menuProduct.Quantity,
                        Fat = (product?.Fat ?? 0) * menuProduct.Quantity,
                    };
                })
                .ToList();

            this.dgvSelectedProducts.DataSource = null;
            this.dgvSelectedProducts.DataSource = rows;
            this.dgvSelectedProducts.ClearSelection();
        }

        private void LoadMenuIntoEditor(int menuId)
        {
            var menu = this.menuController
                .GetMenusByUser(this.currentUser.UserId)
                .FirstOrDefault(item => item.MenuId == menuId);

            if (menu is null)
            {
                return;
            }

            this.selectedMenuId = menuId;
            this.dtpMenuDate.Value = menu.Date;
            this.pendingMenuProducts.Clear();

            var existingProducts = this.menuController.GetMenuProducts(menuId);

            foreach (var menuProduct in existingProducts)
            {
                this.pendingMenuProducts.Add(
                    new MenuProduct(
                        0,
                        0,
                        menuProduct.MealTime,
                        menuProduct.ProductId,
                        menuProduct.Quantity));
            }

            this.LoadPendingProducts();
            this.UpdateTotalsLabel();
            this.btnDeleteMenu.Enabled = true;
            this.btnUpdateMenu.Enabled = true;
        }

        private void ResetEditor()
        {
            this.selectedMenuId = 0;
            this.selectedPendingProductIndex = -1;
            this.pendingMenuProducts.Clear();
            this.dtpMenuDate.Value = DateTime.Today;
            this.cmbMealTime.SelectedIndex = 0;
            this.cmbProducts.SelectedIndex = -1;
            this.txtQuantity.Clear();
            this.btnDeleteMenu.Enabled = false;
            this.btnUpdateMenu.Enabled = false;
            this.LoadPendingProducts();
            this.UpdateTotalsLabel();
            this.dgvMenus.ClearSelection();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (this.cmbMealTime.SelectedItem is null)
            {
                MessageBox.Show("Select a meal time.");
                return;
            }

            if (this.cmbProducts.SelectedItem is not Product product)
            {
                MessageBox.Show("Select a product.");
                return;
            }

            if (!TryParseDecimal(this.txtQuantity.Text, out var quantity) || quantity <= 0)
            {
                MessageBox.Show("Quantity must be a valid number greater than zero.");
                return;
            }

            var menuProduct = new MenuProduct(
                0,
                0,
                this.cmbMealTime.SelectedItem.ToString()!,
                product.ProductId,
                quantity);

            this.pendingMenuProducts.Add(menuProduct);
            this.LoadPendingProducts();
            this.UpdateTotalsLabel();
            this.txtQuantity.Clear();
            this.cmbProducts.SelectedIndex = -1;
        }

        private void btnCreateMenu_Click(object sender, EventArgs e)
        {
            if (this.pendingMenuProducts.Count == 0)
            {
                MessageBox.Show("Add at least one product to the menu.");
                return;
            }

            var result = this.menuController.CreateMenu(
                this.currentUser.UserId,
                this.dtpMenuDate.Value.Date,
                this.pendingMenuProducts);

            if (!result)
            {
                MessageBox.Show("The menu could not be created.");
                return;
            }

            MessageBox.Show("Menu created successfully.");
            this.LoadMenus();
            this.ResetEditor();
        }

        private void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            if (this.selectedMenuId == 0)
            {
                MessageBox.Show("Select a menu first.");
                return;
            }

            var confirmation = MessageBox.Show(
                "Are you sure you want to delete this menu?",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            var result = this.menuController.DeleteMenu(this.selectedMenuId);

            if (!result)
            {
                MessageBox.Show("The menu could not be deleted.");
                return;
            }

            MessageBox.Show("Menu deleted successfully.");
            this.LoadMenus();
            this.ResetEditor();
        }


        private void dgvMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (this.dgvMenus.Rows[e.RowIndex].DataBoundItem is not MenuGridItem menu)
            {
                return;
            }

            this.LoadMenuIntoEditor(menu.MenuId);
        }

        private void UpdateTotalsLabel()
        {
            var totals = this.menuController.CalculateTotals(this.pendingMenuProducts);

            this.lblTotals.Text =
                $"Totals - Calories: {totals.Calories:F2} | Protein: {totals.Protein:F2} | Carbs: {totals.Carbs:F2} | Fat: {totals.Fat:F2}";
        }

        private static bool TryParseDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value.Trim(), out result) ||
                decimal.TryParse(value.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }

        private sealed class MenuGridItem
        {
            /// <summary>
            /// Gets or sets the menu identifier.
            /// </summary>
            public int MenuId { get; set; }

            /// <summary>
            /// Gets or sets the menu date.
            /// </summary>
            public string Date { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the total calories.
            /// </summary>
            public decimal Calories { get; set; }

            /// <summary>
            /// Gets or sets the total protein.
            /// </summary>
            public decimal Protein { get; set; }

            /// <summary>
            /// Gets or sets the total carbohydrates.
            /// </summary>
            public decimal Carbs { get; set; }

            /// <summary>
            /// Gets or sets the total fat.
            /// </summary>
            public decimal Fat { get; set; }
        }

        private sealed class PendingProductGridItem
        {
            /// <summary>
            /// Gets or sets the meal time.
            /// </summary>
            public string MealTime { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the product name.
            /// </summary>
            public string ProductName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the quantity.
            /// </summary>
            public decimal Quantity { get; set; }

            /// <summary>
            /// Gets or sets the calories.
            /// </summary>
            public decimal Calories { get; set; }

            /// <summary>
            /// Gets or sets the protein.
            /// </summary>
            public decimal Protein { get; set; }

            /// <summary>
            /// Gets or sets the carbohydrates.
            /// </summary>
            public decimal Carbs { get; set; }

            /// <summary>
            /// Gets or sets the fat.
            /// </summary>
            public decimal Fat { get; set; }
        }

        private void dgvSelectedProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.RowIndex >= this.pendingMenuProducts.Count)
            {
                return;
            }

            this.selectedPendingProductIndex = e.RowIndex;
            var selectedProduct = this.pendingMenuProducts[e.RowIndex];

            this.cmbMealTime.SelectedItem = selectedProduct.MealTime;
            this.cmbProducts.SelectedValue = selectedProduct.ProductId;
            this.txtQuantity.Text = selectedProduct.Quantity.ToString(CultureInfo.InvariantCulture);
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (this.selectedPendingProductIndex < 0 || this.selectedPendingProductIndex >= this.pendingMenuProducts.Count)
            {
                MessageBox.Show("Select a product from the selected products table first.");
                return;
            }

            this.pendingMenuProducts.RemoveAt(this.selectedPendingProductIndex);
            this.selectedPendingProductIndex = -1;
            this.LoadPendingProducts();
            this.UpdateTotalsLabel();
            this.cmbProducts.SelectedIndex = -1;
            this.txtQuantity.Clear();
        }

        private void btnUpdateMenu_Click(object sender, EventArgs e)
        {
            if (this.selectedMenuId == 0)
            {
                MessageBox.Show("Select a menu first.");
                return;
            }

            if (this.pendingMenuProducts.Count == 0)
            {
                MessageBox.Show("Add at least one product to the menu.");
                return;
            }

            var result = this.menuController.UpdateMenu(
                this.selectedMenuId,
                this.dtpMenuDate.Value.Date,
                this.pendingMenuProducts);

            if (!result)
            {
                MessageBox.Show("The menu could not be updated.");
                return;
            }

            MessageBox.Show("Menu updated successfully.");
            this.LoadMenus();
            this.ResetEditor();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ResetEditor();
        }
    }
}
