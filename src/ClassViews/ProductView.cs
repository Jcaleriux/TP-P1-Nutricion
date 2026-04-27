namespace ClassViews
{
    using ClassController;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// View for product management.
    /// </summary>
    public partial class ProductView : Form
    {
        private readonly ProductController productController;
        private readonly User currentUser;
        private int selectedProductId;
        private bool selectedProductIsActive = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductView"/> class.
        /// </summary>
        /// <param name="productController">The product controller.</param>
        /// <param name="currentUser">The authenticated user.</param>
        public ProductView(ProductController productController, User currentUser)
        {
            this.InitializeComponent();
            this.productController = productController;
            this.currentUser = currentUser;
        }

        /// <summary>
        /// Loads the product data when the form is displayed.
        /// </summary>
        private void ProductView_Load(object sender, EventArgs e)
        {
            this.LoadProducts();
        }

        private void LoadProducts()
        {
            this.dgvProducts.DataSource = null;
            this.dgvProducts.DataSource = this.currentUser.Role == "Admin"
                ? this.productController.GetAllProducts()
                : this.productController.GetActiveProducts();
            this.dgvProducts.ClearSelection();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields())
            {
                return;
            }

            var product = this.BuildProductFromInputs(0);
            bool result;

            try
            {
                result = this.productController.Register(product, this.currentUser);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (!result)
            {
                MessageBox.Show("A product with this name already exists.");
                return;
            }

            MessageBox.Show("Product registered successfully.");
            this.LoadProducts();
            this.ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.selectedProductId == 0)
            {
                MessageBox.Show("Select a product first.");
                return;
            }

            if (!this.ValidateFields())
            {
                return;
            }

            var product = this.BuildProductFromInputs(this.selectedProductId);
            bool result;

            try
            {
                result = this.productController.Update(product, this.currentUser);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (!result)
            {
                MessageBox.Show("The product could not be updated.");
                return;
            }

            MessageBox.Show("Product updated successfully.");
            this.LoadProducts();
            this.ClearFields();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (this.dgvProducts.Rows[e.RowIndex].DataBoundItem is not Product product)
            {
                return;
            }

            this.selectedProductId = product.ProductId;
            this.txtName.Text = product.Name;
            this.txtCalories.Text = product.Calories.ToString(CultureInfo.InvariantCulture);
            this.txtProtein.Text = product.Protein.ToString(CultureInfo.InvariantCulture);
            this.txtCarbs.Text = product.Carbs.ToString(CultureInfo.InvariantCulture);
            this.txtFat.Text = product.Fat.ToString(CultureInfo.InvariantCulture);
            this.txtUnit.Text = product.Unit;
            this.selectedProductIsActive = product.IsActive;
            this.btnUpdate.Enabled = true;
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text) ||
                string.IsNullOrWhiteSpace(this.txtCalories.Text) ||
                string.IsNullOrWhiteSpace(this.txtProtein.Text) ||
                string.IsNullOrWhiteSpace(this.txtCarbs.Text) ||
                string.IsNullOrWhiteSpace(this.txtFat.Text) ||
                string.IsNullOrWhiteSpace(this.txtUnit.Text))
            {
                MessageBox.Show("All fields are required.");
                return false;
            }

            if (!TryParseDecimal(this.txtCalories.Text, out _))
            {
                MessageBox.Show("Calories must be a valid number.");
                return false;
            }

            if (!TryParseDecimal(this.txtProtein.Text, out _))
            {
                MessageBox.Show("Protein must be a valid number.");
                return false;
            }

            if (!TryParseDecimal(this.txtCarbs.Text, out _))
            {
                MessageBox.Show("Carbs must be a valid number.");
                return false;
            }

            if (!TryParseDecimal(this.txtFat.Text, out _))
            {
                MessageBox.Show("Fat must be a valid number.");
                return false;
            }

            return true;
        }

        private Product BuildProductFromInputs(int productId)
        {
            return new Product(
                productId,
                this.txtName.Text.Trim(),
                ParseDecimal(this.txtCalories.Text),
                ParseDecimal(this.txtProtein.Text),
                ParseDecimal(this.txtCarbs.Text),
                ParseDecimal(this.txtFat.Text),
                this.txtUnit.Text.Trim(),
                this.selectedProductId == 0 ? true : this.selectedProductIsActive);
        }

        private static bool TryParseDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value.Trim(), out result) ||
                decimal.TryParse(value.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }

        private static decimal ParseDecimal(string value)
        {
            if (decimal.TryParse(value.Trim(), out var result))
            {
                return result;
            }

            return decimal.Parse(value.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture);
        }

        private void ClearFields()
        {
            this.selectedProductId = 0;
            this.selectedProductIsActive = true;
            this.txtName.Clear();
            this.txtCalories.Clear();
            this.txtProtein.Clear();
            this.txtCarbs.Clear();
            this.txtFat.Clear();
            this.txtUnit.Clear();
            this.btnUpdate.Enabled = false;
            this.dgvProducts.ClearSelection();
        }
    }
}
