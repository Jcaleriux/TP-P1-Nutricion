namespace ClassViews
{
    partial class MainForm
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            btnManageProducts = new Button();
            lblTitle = new Label();
            lblWelcome = new Label();
            btnManageMenus = new Button();
            btnNutritionInfo = new Button();
            btnStatistics = new Button();
            SuspendLayout();
            // 
            // btnManageProducts
            // 
            btnManageProducts.Location = new Point(32, 140);
            btnManageProducts.Name = "btnManageProducts";
            btnManageProducts.Size = new Size(147, 29);
            btnManageProducts.TabIndex = 0;
            btnManageProducts.Text = "Manage Products";
            btnManageProducts.UseVisualStyleBackColor = true;
            btnManageProducts.Click += btnManageProducts_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(266, 39);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(341, 41);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Nutrition For Everyone";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(392, 97);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(71, 20);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "Welcome";
            // 
            // btnManageMenus
            // 
            btnManageMenus.Location = new Point(32, 189);
            btnManageMenus.Name = "btnManageMenus";
            btnManageMenus.Size = new Size(147, 29);
            btnManageMenus.TabIndex = 3;
            btnManageMenus.Text = "Manage Menus";
            btnManageMenus.UseVisualStyleBackColor = true;
            btnManageMenus.Click += btnManageMenus_Click;
            // 
            // btnNutritionInfo
            // 
            btnNutritionInfo.Location = new Point(32, 239);
            btnNutritionInfo.Name = "btnNutritionInfo";
            btnNutritionInfo.Size = new Size(147, 29);
            btnNutritionInfo.TabIndex = 4;
            btnNutritionInfo.Text = "Nutrition Info";
            btnNutritionInfo.UseVisualStyleBackColor = true;
            btnNutritionInfo.Click += btnNutritionInfo_Click;
            // 
            // btnStatistics
            // 
            btnStatistics.Location = new Point(32, 289);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(147, 29);
            btnStatistics.TabIndex = 5;
            btnStatistics.Text = "Statistics";
            btnStatistics.UseVisualStyleBackColor = true;
            btnStatistics.Click += btnStatistics_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(btnStatistics);
            Controls.Add(btnNutritionInfo);
            Controls.Add(btnManageMenus);
            Controls.Add(lblWelcome);
            Controls.Add(lblTitle);
            Controls.Add(btnManageProducts);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Main Form";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnManageProducts;
        private Label lblTitle;
        private Label lblWelcome;
        private Button btnManageMenus;
        private Button btnNutritionInfo;
        private Button btnStatistics;
    }
}
