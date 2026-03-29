namespace ClassViews
{
    partial class LoginView
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
            btnLogin = new Button();
            btnRegister = new Button();
            label1 = new Label();
            label2 = new Label();
            txtUser = new TextBox();
            txtPassword = new TextBox();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(228, 264);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(155, 47);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(389, 264);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(155, 47);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(228, 132);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(228, 167);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(291, 132);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(253, 23);
            txtUser.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(291, 167);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(253, 23);
            txtPassword.TabIndex = 5;
            // 
            // LoginView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtPassword);
            Controls.Add(txtUser);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Name = "LoginView";
            Text = "LoginView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private Button btnRegister;
        private Label label1;
        private Label label2;
        private TextBox txtUser;
        private TextBox txtPassword;
    }
}