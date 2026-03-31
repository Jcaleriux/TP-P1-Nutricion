namespace ClassViews
{
    partial class ProductView
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
            lblName = new Label();
            lblCalories = new Label();
            lblProtein = new Label();
            lblCarbs = new Label();
            lblFat = new Label();
            lblUnit = new Label();
            txtName = new TextBox();
            txtCalories = new TextBox();
            txtProtein = new TextBox();
            txtCarbs = new TextBox();
            txtFat = new TextBox();
            txtUnit = new TextBox();
            btnRegister = new Button();
            btnUpdate = new Button();
            btnClear = new Button();
            dgvProducts = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(31, 36);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(194, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Product Management";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(31, 81);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // lblCalories
            // 
            lblCalories.AutoSize = true;
            lblCalories.Location = new Point(31, 126);
            lblCalories.Name = "lblCalories";
            lblCalories.Size = new Size(62, 20);
            lblCalories.TabIndex = 2;
            lblCalories.Text = "Calories";
            // 
            // lblProtein
            // 
            lblProtein.AutoSize = true;
            lblProtein.Location = new Point(31, 173);
            lblProtein.Name = "lblProtein";
            lblProtein.Size = new Size(56, 20);
            lblProtein.TabIndex = 3;
            lblProtein.Text = "Protein";
            // 
            // lblCarbs
            // 
            lblCarbs.AutoSize = true;
            lblCarbs.Location = new Point(31, 212);
            lblCarbs.Name = "lblCarbs";
            lblCarbs.Size = new Size(46, 20);
            lblCarbs.TabIndex = 4;
            lblCarbs.Text = "Carbs";
            // 
            // lblFat
            // 
            lblFat.AutoSize = true;
            lblFat.Location = new Point(31, 254);
            lblFat.Name = "lblFat";
            lblFat.Size = new Size(28, 20);
            lblFat.TabIndex = 5;
            lblFat.Text = "Fat";
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(31, 300);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(36, 20);
            lblUnit.TabIndex = 6;
            lblUnit.Text = "Unit";
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 81);
            txtName.Name = "txtName";
            txtName.Size = new Size(225, 27);
            txtName.TabIndex = 8;
            // 
            // txtCalories
            // 
            txtCalories.Location = new Point(100, 126);
            txtCalories.Name = "txtCalories";
            txtCalories.Size = new Size(225, 27);
            txtCalories.TabIndex = 9;
            // 
            // txtProtein
            // 
            txtProtein.Location = new Point(100, 170);
            txtProtein.Name = "txtProtein";
            txtProtein.Size = new Size(225, 27);
            txtProtein.TabIndex = 10;
            // 
            // txtCarbs
            // 
            txtCarbs.Location = new Point(100, 212);
            txtCarbs.Name = "txtCarbs";
            txtCarbs.Size = new Size(225, 27);
            txtCarbs.TabIndex = 11;
            // 
            // txtFat
            // 
            txtFat.Location = new Point(100, 254);
            txtFat.Name = "txtFat";
            txtFat.Size = new Size(225, 27);
            txtFat.TabIndex = 12;
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(100, 297);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(225, 27);
            txtUnit.TabIndex = 13;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(31, 368);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(94, 29);
            btnRegister.TabIndex = 14;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Enabled = false;
            btnUpdate.Location = new Point(131, 368);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 15;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(231, 368);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 16;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(351, 36);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(712, 361);
            dgvProducts.TabIndex = 17;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            // 
            // ProductView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1110, 450);
            Controls.Add(dgvProducts);
            Controls.Add(btnClear);
            Controls.Add(btnUpdate);
            Controls.Add(btnRegister);
            Controls.Add(txtUnit);
            Controls.Add(txtFat);
            Controls.Add(txtCarbs);
            Controls.Add(txtProtein);
            Controls.Add(txtCalories);
            Controls.Add(txtName);
            Controls.Add(lblUnit);
            Controls.Add(lblFat);
            Controls.Add(lblCarbs);
            Controls.Add(lblProtein);
            Controls.Add(lblCalories);
            Controls.Add(lblName);
            Controls.Add(lblTitle);
            Name = "ProductView";
            Text = "Product Management";
            Load += ProductView_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblName;
        private Label lblCalories;
        private Label lblProtein;
        private Label lblCarbs;
        private Label lblFat;
        private Label lblUnit;
        private TextBox txtName;
        private TextBox txtCalories;
        private TextBox txtProtein;
        private TextBox txtCarbs;
        private TextBox txtFat;
        private TextBox txtUnit;
        private Button btnRegister;
        private Button btnUpdate;
        private Button btnClear;
        private DataGridView dgvProducts;
    }
}