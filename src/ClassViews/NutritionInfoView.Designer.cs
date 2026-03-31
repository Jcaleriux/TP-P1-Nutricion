namespace ClassViews
{
    partial class NutritionInfoView
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
            lblMaintenanceTitle = new Label();
            lblMaintenanceValue = new Label();
            lblGoalTitle = new Label();
            lblGoalValue = new Label();
            lblProteinTitle = new Label();
            lblProteinValue = new Label();
            lblCarbsTitle = new Label();
            lblCarbsValue = new Label();
            lblBmiCategoryValue = new Label();
            lblBmiCategoryTitle = new Label();
            lblBmiValue = new Label();
            lblBmiTitle = new Label();
            lblFatValue = new Label();
            lblFatTitle = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(227, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(328, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Nutrition Information";
            // 
            // lblMaintenanceTitle
            // 
            lblMaintenanceTitle.AutoSize = true;
            lblMaintenanceTitle.Location = new Point(80, 102);
            lblMaintenanceTitle.Name = "lblMaintenanceTitle";
            lblMaintenanceTitle.Size = new Size(151, 20);
            lblMaintenanceTitle.TabIndex = 1;
            lblMaintenanceTitle.Text = "Maintenance Calories";
            // 
            // lblMaintenanceValue
            // 
            lblMaintenanceValue.AutoSize = true;
            lblMaintenanceValue.Location = new Point(237, 102);
            lblMaintenanceValue.Name = "lblMaintenanceValue";
            lblMaintenanceValue.Size = new Size(15, 20);
            lblMaintenanceValue.TabIndex = 2;
            lblMaintenanceValue.Text = "-";
            // 
            // lblGoalTitle
            // 
            lblGoalTitle.AutoSize = true;
            lblGoalTitle.Location = new Point(80, 147);
            lblGoalTitle.Name = "lblGoalTitle";
            lblGoalTitle.Size = new Size(97, 20);
            lblGoalTitle.TabIndex = 3;
            lblGoalTitle.Text = "Goal Calories";
            // 
            // lblGoalValue
            // 
            lblGoalValue.AutoSize = true;
            lblGoalValue.Location = new Point(237, 147);
            lblGoalValue.Name = "lblGoalValue";
            lblGoalValue.Size = new Size(15, 20);
            lblGoalValue.TabIndex = 4;
            lblGoalValue.Text = "-";
            // 
            // lblProteinTitle
            // 
            lblProteinTitle.AutoSize = true;
            lblProteinTitle.Location = new Point(80, 194);
            lblProteinTitle.Name = "lblProteinTitle";
            lblProteinTitle.Size = new Size(101, 20);
            lblProteinTitle.TabIndex = 5;
            lblProteinTitle.Text = "Protein Target";
            // 
            // lblProteinValue
            // 
            lblProteinValue.AutoSize = true;
            lblProteinValue.Location = new Point(237, 194);
            lblProteinValue.Name = "lblProteinValue";
            lblProteinValue.Size = new Size(15, 20);
            lblProteinValue.TabIndex = 6;
            lblProteinValue.Text = "-";
            // 
            // lblCarbsTitle
            // 
            lblCarbsTitle.AutoSize = true;
            lblCarbsTitle.Location = new Point(80, 248);
            lblCarbsTitle.Name = "lblCarbsTitle";
            lblCarbsTitle.Size = new Size(91, 20);
            lblCarbsTitle.TabIndex = 7;
            lblCarbsTitle.Text = "Carbs Target";
            // 
            // lblCarbsValue
            // 
            lblCarbsValue.AutoSize = true;
            lblCarbsValue.Location = new Point(237, 248);
            lblCarbsValue.Name = "lblCarbsValue";
            lblCarbsValue.Size = new Size(15, 20);
            lblCarbsValue.TabIndex = 8;
            lblCarbsValue.Text = "-";
            // 
            // lblBmiCategoryValue
            // 
            lblBmiCategoryValue.AutoSize = true;
            lblBmiCategoryValue.Location = new Point(237, 379);
            lblBmiCategoryValue.Name = "lblBmiCategoryValue";
            lblBmiCategoryValue.Size = new Size(15, 20);
            lblBmiCategoryValue.TabIndex = 14;
            lblBmiCategoryValue.Text = "-";
            // 
            // lblBmiCategoryTitle
            // 
            lblBmiCategoryTitle.AutoSize = true;
            lblBmiCategoryTitle.Location = new Point(80, 379);
            lblBmiCategoryTitle.Name = "lblBmiCategoryTitle";
            lblBmiCategoryTitle.Size = new Size(99, 20);
            lblBmiCategoryTitle.TabIndex = 13;
            lblBmiCategoryTitle.Text = "BMI Category";
            // 
            // lblBmiValue
            // 
            lblBmiValue.AutoSize = true;
            lblBmiValue.Location = new Point(237, 332);
            lblBmiValue.Name = "lblBmiValue";
            lblBmiValue.Size = new Size(15, 20);
            lblBmiValue.TabIndex = 12;
            lblBmiValue.Text = "-";
            // 
            // lblBmiTitle
            // 
            lblBmiTitle.AutoSize = true;
            lblBmiTitle.Location = new Point(80, 332);
            lblBmiTitle.Name = "lblBmiTitle";
            lblBmiTitle.Size = new Size(35, 20);
            lblBmiTitle.TabIndex = 11;
            lblBmiTitle.Text = "BMI";
            // 
            // lblFatValue
            // 
            lblFatValue.AutoSize = true;
            lblFatValue.Location = new Point(237, 287);
            lblFatValue.Name = "lblFatValue";
            lblFatValue.Size = new Size(15, 20);
            lblFatValue.TabIndex = 10;
            lblFatValue.Text = "-";
            // 
            // lblFatTitle
            // 
            lblFatTitle.AutoSize = true;
            lblFatTitle.Location = new Point(80, 287);
            lblFatTitle.Name = "lblFatTitle";
            lblFatTitle.Size = new Size(73, 20);
            lblFatTitle.TabIndex = 9;
            lblFatTitle.Text = "Fat Target";
            // 
            // NutritionInfoView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblBmiCategoryValue);
            Controls.Add(lblBmiCategoryTitle);
            Controls.Add(lblBmiValue);
            Controls.Add(lblBmiTitle);
            Controls.Add(lblFatValue);
            Controls.Add(lblFatTitle);
            Controls.Add(lblCarbsValue);
            Controls.Add(lblCarbsTitle);
            Controls.Add(lblProteinValue);
            Controls.Add(lblProteinTitle);
            Controls.Add(lblGoalValue);
            Controls.Add(lblGoalTitle);
            Controls.Add(lblMaintenanceValue);
            Controls.Add(lblMaintenanceTitle);
            Controls.Add(lblTitle);
            Name = "NutritionInfoView";
            Text = "Nutrition Information";
            Load += NutritionInfoView_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblMaintenanceTitle;
        private Label lblMaintenanceValue;
        private Label lblGoalTitle;
        private Label lblGoalValue;
        private Label lblProteinTitle;
        private Label lblProteinValue;
        private Label lblCarbsTitle;
        private Label lblCarbsValue;
        private Label lblBmiCategoryValue;
        private Label lblBmiCategoryTitle;
        private Label lblBmiValue;
        private Label lblBmiTitle;
        private Label lblFatValue;
        private Label lblFatTitle;
    }
}