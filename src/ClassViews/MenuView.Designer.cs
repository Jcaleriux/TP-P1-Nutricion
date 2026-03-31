namespace ClassViews
{
    partial class MenuView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblDate = new Label();
            dtpMenuDate = new DateTimePicker();
            lblMealTime = new Label();
            cmbMealTime = new ComboBox();
            lblProduct = new Label();
            cmbProducts = new ComboBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            btnAddProduct = new Button();
            btnCreateMenu = new Button();
            btnDeleteMenu = new Button();
            dgvSelectedProducts = new DataGridView();
            dgvMenus = new DataGridView();
            lblTotals = new Label();
            btnUpdateMenu = new Button();
            btnRemoveProduct = new Button();
            btnClear = new Button();
            lblMenusSection = new Label();
            lblMenuProductsSection = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSelectedProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMenus).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(87, 154);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(294, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Menu Management";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(54, 249);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(41, 20);
            lblDate.TabIndex = 1;
            lblDate.Text = "Date";
            // 
            // dtpMenuDate
            // 
            dtpMenuDate.Location = new Point(156, 249);
            dtpMenuDate.Name = "dtpMenuDate";
            dtpMenuDate.Size = new Size(279, 27);
            dtpMenuDate.TabIndex = 2;
            // 
            // lblMealTime
            // 
            lblMealTime.AutoSize = true;
            lblMealTime.Location = new Point(54, 299);
            lblMealTime.Name = "lblMealTime";
            lblMealTime.Size = new Size(79, 20);
            lblMealTime.TabIndex = 3;
            lblMealTime.Text = "Meal Time";
            // 
            // cmbMealTime
            // 
            cmbMealTime.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMealTime.FormattingEnabled = true;
            cmbMealTime.Location = new Point(156, 299);
            cmbMealTime.Name = "cmbMealTime";
            cmbMealTime.Size = new Size(279, 28);
            cmbMealTime.TabIndex = 4;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(54, 349);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(60, 20);
            lblProduct.TabIndex = 5;
            lblProduct.Text = "Product";
            // 
            // cmbProducts
            // 
            cmbProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducts.FormattingEnabled = true;
            cmbProducts.Location = new Point(156, 349);
            cmbProducts.Name = "cmbProducts";
            cmbProducts.Size = new Size(279, 28);
            cmbProducts.TabIndex = 6;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(54, 398);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(65, 20);
            lblQuantity.TabIndex = 7;
            lblQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(156, 398);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(279, 27);
            txtQuantity.TabIndex = 8;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(856, 645);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(142, 29);
            btnAddProduct.TabIndex = 9;
            btnAddProduct.Text = "Add Product";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnCreateMenu
            // 
            btnCreateMenu.Location = new Point(1069, 337);
            btnCreateMenu.Name = "btnCreateMenu";
            btnCreateMenu.Size = new Size(105, 29);
            btnCreateMenu.TabIndex = 10;
            btnCreateMenu.Text = "Create Menu";
            btnCreateMenu.UseVisualStyleBackColor = true;
            btnCreateMenu.Click += btnCreateMenu_Click;
            // 
            // btnDeleteMenu
            // 
            btnDeleteMenu.Enabled = false;
            btnDeleteMenu.Location = new Point(1189, 338);
            btnDeleteMenu.Name = "btnDeleteMenu";
            btnDeleteMenu.Size = new Size(105, 29);
            btnDeleteMenu.TabIndex = 11;
            btnDeleteMenu.Text = "Delete Menu";
            btnDeleteMenu.UseVisualStyleBackColor = true;
            btnDeleteMenu.Click += btnDeleteMenu_Click;
            // 
            // dgvSelectedProducts
            // 
            dgvSelectedProducts.AllowUserToAddRows = false;
            dgvSelectedProducts.AllowUserToDeleteRows = false;
            dgvSelectedProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSelectedProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSelectedProducts.Location = new Point(493, 398);
            dgvSelectedProducts.MultiSelect = false;
            dgvSelectedProducts.Name = "dgvSelectedProducts";
            dgvSelectedProducts.ReadOnly = true;
            dgvSelectedProducts.RowHeadersWidth = 51;
            dgvSelectedProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSelectedProducts.Size = new Size(801, 226);
            dgvSelectedProducts.TabIndex = 12;
            dgvSelectedProducts.CellClick += dgvSelectedProducts_CellClick;
            // 
            // dgvMenus
            // 
            dgvMenus.AllowUserToAddRows = false;
            dgvMenus.AllowUserToDeleteRows = false;
            dgvMenus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMenus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMenus.Location = new Point(493, 92);
            dgvMenus.MultiSelect = false;
            dgvMenus.Name = "dgvMenus";
            dgvMenus.ReadOnly = true;
            dgvMenus.RowHeadersWidth = 51;
            dgvMenus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMenus.Size = new Size(801, 227);
            dgvMenus.TabIndex = 13;
            dgvMenus.CellClick += dgvMenus_CellClick;
            // 
            // lblTotals
            // 
            lblTotals.AutoSize = true;
            lblTotals.Location = new Point(493, 337);
            lblTotals.Name = "lblTotals";
            lblTotals.Size = new Size(48, 20);
            lblTotals.TabIndex = 14;
            lblTotals.Text = "Totals";
            // 
            // btnUpdateMenu
            // 
            btnUpdateMenu.Location = new Point(1152, 645);
            btnUpdateMenu.Name = "btnUpdateMenu";
            btnUpdateMenu.Size = new Size(142, 29);
            btnUpdateMenu.TabIndex = 15;
            btnUpdateMenu.Text = "Update Menu";
            btnUpdateMenu.UseVisualStyleBackColor = true;
            btnUpdateMenu.Click += btnUpdateMenu_Click;
            // 
            // btnRemoveProduct
            // 
            btnRemoveProduct.Location = new Point(1004, 645);
            btnRemoveProduct.Name = "btnRemoveProduct";
            btnRemoveProduct.Size = new Size(142, 29);
            btnRemoveProduct.TabIndex = 16;
            btnRemoveProduct.Text = "Remove Product";
            btnRemoveProduct.UseVisualStyleBackColor = true;
            btnRemoveProduct.Click += btnRemoveProduct_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(300, 461);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(135, 29);
            btnClear.TabIndex = 17;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblMenusSection
            // 
            lblMenusSection.AutoSize = true;
            lblMenusSection.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMenusSection.Location = new Point(855, 64);
            lblMenusSection.Name = "lblMenusSection";
            lblMenusSection.Size = new Size(61, 25);
            lblMenusSection.TabIndex = 18;
            lblMenusSection.Text = "Menu";
            // 
            // lblMenuProductsSection
            // 
            lblMenuProductsSection.AutoSize = true;
            lblMenuProductsSection.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMenuProductsSection.Location = new Point(812, 370);
            lblMenuProductsSection.Name = "lblMenuProductsSection";
            lblMenuProductsSection.Size = new Size(151, 25);
            lblMenuProductsSection.TabIndex = 19;
            lblMenuProductsSection.Text = "Menu's Products";
            // 
            // MenuView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1327, 701);
            Controls.Add(lblMenuProductsSection);
            Controls.Add(lblMenusSection);
            Controls.Add(btnClear);
            Controls.Add(btnRemoveProduct);
            Controls.Add(btnUpdateMenu);
            Controls.Add(lblTotals);
            Controls.Add(dgvMenus);
            Controls.Add(dgvSelectedProducts);
            Controls.Add(btnDeleteMenu);
            Controls.Add(btnCreateMenu);
            Controls.Add(btnAddProduct);
            Controls.Add(txtQuantity);
            Controls.Add(lblQuantity);
            Controls.Add(cmbProducts);
            Controls.Add(lblProduct);
            Controls.Add(cmbMealTime);
            Controls.Add(lblMealTime);
            Controls.Add(dtpMenuDate);
            Controls.Add(lblDate);
            Controls.Add(lblTitle);
            Name = "MenuView";
            Text = "MenuView";
            Load += MenuView_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSelectedProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMenus).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblDate;
        private DateTimePicker dtpMenuDate;
        private Label lblMealTime;
        private ComboBox cmbMealTime;
        private Label lblProduct;
        private ComboBox cmbProducts;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Button btnAddProduct;
        private Button btnCreateMenu;
        private Button btnDeleteMenu;
        private DataGridView dgvSelectedProducts;
        private DataGridView dgvMenus;
        private Label lblTotals;
        private Button btnUpdateMenu;
        private Button btnRemoveProduct;
        private Button btnClear;
        private Label lblMenusSection;
        private Label lblMenuProductsSection;
    }
}
