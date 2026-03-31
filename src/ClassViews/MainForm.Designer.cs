namespace ClassViews
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnManageProducts = new Button();
            lblTitle = new Label();
            lblWelcome = new Label();
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(lblWelcome);
            Controls.Add(lblTitle);
            Controls.Add(btnManageProducts);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Main Form";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnManageProducts;
        private Label lblTitle;
        private Label lblWelcome;
    }
}
