namespace ClassViews
{
    using ClassController;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// View for displaying nutritional information of the authenticated user.
    /// </summary>
    public partial class NutritionInfoView : Form
    {
        private readonly User currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="NutritionInfoView"/> class.
        /// </summary>
        /// <param name="currentUser">The authenticated user.</param>
        public NutritionInfoView(User currentUser)
        {
            this.InitializeComponent();
            this.currentUser = currentUser;
        }

        private void NutritionInfoView_Load(object sender, EventArgs e)
        {
            this.LoadNutritionInfo();
        }

        private void LoadNutritionInfo()
        {
            var bmi = NutritionCalculator.CalculateBmi(this.currentUser);
            var bmiCategory = NutritionCalculator.GetBmiCategory(bmi);
            var maintenanceCalories = NutritionCalculator.CalculateMaintenanceCalories(this.currentUser);
            var goalCalories = NutritionCalculator.CalculateGoalCalories(this.currentUser);
            var macros = NutritionCalculator.CalculateMacroTargets(this.currentUser);

            this.lblMaintenanceValue.Text = $"{maintenanceCalories.ToString("F2", CultureInfo.InvariantCulture)} kcal";
            this.lblGoalValue.Text = $"{goalCalories.ToString("F2", CultureInfo.InvariantCulture)} kcal";
            this.lblProteinValue.Text = $"{macros.Protein.ToString("F2", CultureInfo.InvariantCulture)} g";
            this.lblCarbsValue.Text = $"{macros.Carbs.ToString("F2", CultureInfo.InvariantCulture)} g";
            this.lblFatValue.Text = $"{macros.Fat.ToString("F2", CultureInfo.InvariantCulture)} g";
            this.lblBmiValue.Text = bmi.ToString("F2", CultureInfo.InvariantCulture);
            this.lblBmiCategoryValue.Text = bmiCategory;
        }
    }
}
