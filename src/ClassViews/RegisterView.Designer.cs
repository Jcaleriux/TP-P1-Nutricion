namespace ClassViews
{
    partial class RegisterView
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
            txtEmail = new TextBox();
            txtName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btnRegister = new Button();
            txtPassword = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtAge = new TextBox();
            txtHeightCm = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtWeightKg = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            cmbGoal = new ComboBox();
            cmbActivityLevel = new ComboBox();
            cmbDietType = new ComboBox();
            cmbSex = new ComboBox();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(245, 80);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(316, 23);
            txtEmail.TabIndex = 11;
            // 
            // txtName
            // 
            txtName.Location = new Point(245, 36);
            txtName.Name = "txtName";
            txtName.Size = new Size(316, 23);
            txtName.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(245, 62);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 9;
            label2.Text = "Email";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(245, 18);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 8;
            label1.Text = "Name";
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(245, 368);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(316, 47);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(245, 125);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(316, 23);
            txtPassword.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(245, 151);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 13;
            label3.Text = "Goal";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(245, 107);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 12;
            label4.Text = "Password";
            // 
            // txtAge
            // 
            txtAge.Location = new Point(245, 297);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(155, 23);
            txtAge.TabIndex = 23;
            // 
            // txtHeightCm
            // 
            txtHeightCm.Location = new Point(406, 254);
            txtHeightCm.Name = "txtHeightCm";
            txtHeightCm.Size = new Size(155, 23);
            txtHeightCm.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(245, 279);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 21;
            label5.Text = "Age";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(406, 236);
            label6.Name = "label6";
            label6.Size = new Size(78, 15);
            label6.TabIndex = 20;
            label6.Text = "Height in Cm";
            // 
            // txtWeightKg
            // 
            txtWeightKg.Location = new Point(245, 254);
            txtWeightKg.Name = "txtWeightKg";
            txtWeightKg.Size = new Size(155, 23);
            txtWeightKg.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(245, 236);
            label7.Name = "label7";
            label7.Size = new Size(75, 15);
            label7.TabIndex = 17;
            label7.Text = "Weight in Kg";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(245, 192);
            label8.Name = "label8";
            label8.Size = new Size(77, 15);
            label8.TabIndex = 16;
            label8.Text = "Activity Level";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(406, 279);
            label9.Name = "label9";
            label9.Size = new Size(24, 15);
            label9.TabIndex = 24;
            label9.Text = "Sex";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(245, 321);
            label10.Name = "label10";
            label10.Size = new Size(53, 15);
            label10.TabIndex = 26;
            label10.Text = "DietType";
            // 
            // cmbGoal
            // 
            cmbGoal.FormattingEnabled = true;
            cmbGoal.Location = new Point(245, 169);
            cmbGoal.Name = "cmbGoal";
            cmbGoal.Size = new Size(316, 23);
            cmbGoal.TabIndex = 28;
            // 
            // cmbActivityLevel
            // 
            cmbActivityLevel.FormattingEnabled = true;
            cmbActivityLevel.Location = new Point(245, 210);
            cmbActivityLevel.Name = "cmbActivityLevel";
            cmbActivityLevel.Size = new Size(316, 23);
            cmbActivityLevel.TabIndex = 29;
            // 
            // cmbDietType
            // 
            cmbDietType.FormattingEnabled = true;
            cmbDietType.Location = new Point(245, 339);
            cmbDietType.Name = "cmbDietType";
            cmbDietType.Size = new Size(316, 23);
            cmbDietType.TabIndex = 30;
            // 
            // cmbSex
            // 
            cmbSex.FormattingEnabled = true;
            cmbSex.Location = new Point(406, 297);
            cmbSex.Name = "cmbSex";
            cmbSex.Size = new Size(155, 23);
            cmbSex.TabIndex = 31;
            // 
            // RegisterView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbSex);
            Controls.Add(cmbDietType);
            Controls.Add(cmbActivityLevel);
            Controls.Add(cmbGoal);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(txtAge);
            Controls.Add(txtHeightCm);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(txtWeightKg);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(txtPassword);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRegister);
            Name = "RegisterView";
            Text = "RegisterView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private TextBox txtName;
        private Label label2;
        private Label label1;
        private Button btnRegister;
        private TextBox txtPassword;
        private Label label3;
        private Label label4;
        private TextBox txtAge;
        private TextBox txtHeightCm;
        private Label label5;
        private Label label6;
        private TextBox txtWeightKg;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private ComboBox cmbGoal;
        private ComboBox cmbActivityLevel;
        private ComboBox cmbDietType;
        private ComboBox cmbSex;
    }
}