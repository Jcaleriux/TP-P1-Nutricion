namespace ClassViews
{
    using ClassController;
    using ClassModels;

    /// <summary>
    /// View for the registration form.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class RegisterView : Form
    {
        private LoginController loginController;
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterView"/> class.
        /// </summary>
        /// <param name="loginController">The login controller.</param>
        public RegisterView(LoginController loginController)
        {
            InitializeComponent();
            this.loginController = loginController;
            this.LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cmbGoal.Items.Add("Maintain");
            cmbGoal.Items.Add("LoseFat");
            cmbGoal.Items.Add("GainMass");

            cmbActivityLevel.Items.Add("Sedentary");
            cmbActivityLevel.Items.Add("Light");
            cmbActivityLevel.Items.Add("Moderate");
            cmbActivityLevel.Items.Add("High");

            cmbSex.Items.Add("Male");
            cmbSex.Items.Add("Female");
            cmbSex.Items.Add("Other");

            cmbDietType.Items.Add("Standard");
            cmbDietType.Items.Add("Keto");
            cmbDietType.Items.Add("Vegetarian");
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields())
            {
                return;
            }

            var user = new User(
                0,
                txtName.Text,
                txtEmail.Text,
                txtPassword.Text,
                cmbGoal.Text,
                cmbActivityLevel.Text,
                decimal.Parse(txtWeightKg.Text, System.Globalization.CultureInfo.InvariantCulture),
                decimal.Parse(txtHeightCm.Text, System.Globalization.CultureInfo.InvariantCulture),
                int.Parse(txtAge.Text, System.Globalization.CultureInfo.InvariantCulture),
                cmbSex.Text,
                cmbDietType.Text);


            var result = this.loginController.Register(user);

            if (result)
            {
                MessageBox.Show("User registered successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("This email is already registered.");
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtWeightKg.Text) ||
                string.IsNullOrWhiteSpace(txtHeightCm.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(cmbGoal.Text) ||
                string.IsNullOrWhiteSpace(cmbActivityLevel.Text) ||
                string.IsNullOrWhiteSpace(cmbSex.Text) ||
                string.IsNullOrWhiteSpace(cmbDietType.Text))
            {
                MessageBox.Show("All fields are required.");
                return false;
            }

            if (!decimal.TryParse(txtWeightKg.Text.Trim(), out _))
            {
                MessageBox.Show("Weight must be a valid number.");
                return false;
            }

            if (!decimal.TryParse(txtHeightCm.Text.Trim(), out _))
            {
                MessageBox.Show("Height must be a valid number.");
                return false;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out _))
            {
                MessageBox.Show("Age must be a valid integer.");
                return false;
            }

            return true;
        }
    }
}
