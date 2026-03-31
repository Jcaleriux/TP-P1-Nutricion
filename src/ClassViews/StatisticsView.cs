namespace ClassViews
{
    using ClassController;
    using ClassController.Abstractions;
    using ClassModels;
    using System.Globalization;

    /// <summary>
    /// View for displaying daily and range-based nutrition statistics.
    /// </summary>
    public partial class StatisticsView : Form
    {
        private readonly User currentUser;
        private readonly INutritionStatisticsController nutritionStatisticsController;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsView"/> class.
        /// </summary>
        /// <param name="currentUser">The authenticated user.</param>
        /// <param name="nutritionStatisticsController">The nutrition statistics controller.</param>
        public StatisticsView(
            User currentUser,
            INutritionStatisticsController nutritionStatisticsController)
        {
            this.InitializeComponent();
            this.currentUser = currentUser;
            this.nutritionStatisticsController = nutritionStatisticsController;
        }

        private void StatisticsView_Load(object sender, EventArgs e)
        {
            this.dtpSelectedDate.Value = DateTime.Today;
            this.dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0, DateTimeKind.Local);
            this.dtpEndDate.Value = DateTime.Today;

            this.LoadDailyStatistics(this.dtpSelectedDate.Value.Date);
            this.LoadTodayProgress();
            this.LoadRangeStatistics(this.dtpStartDate.Value.Date, this.dtpEndDate.Value.Date);
        }

        private void btnLoadDaily_Click(object sender, EventArgs e)
        {
            this.LoadDailyStatistics(this.dtpSelectedDate.Value.Date);
        }

        private void btnLoadRange_Click(object sender, EventArgs e)
        {
            this.LoadRangeStatistics(this.dtpStartDate.Value.Date, this.dtpEndDate.Value.Date);
        }

        private void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            var currentMonthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0, DateTimeKind.Local);
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);

            this.dtpStartDate.Value = currentMonthStart;
            this.dtpEndDate.Value = currentMonthEnd;
            this.LoadRangeStatistics(currentMonthStart, currentMonthEnd);
        }

        private void LoadDailyStatistics(DateTime selectedDate)
        {
            var dailyComparison = this.nutritionStatisticsController.GetDailyComparison(this.currentUser, selectedDate);

            this.lblDailyConsumedValue.Text =
                $"Calories: {dailyComparison.CaloriesConsumed:F2} kcal | Protein: {dailyComparison.ProteinConsumed:F2} g | Carbs: {dailyComparison.CarbsConsumed:F2} g | Fat: {dailyComparison.FatConsumed:F2} g";

            this.lblDailyGoalsValue.Text =
                $"Calories: {dailyComparison.CaloriesGoal:F2} kcal | Protein: {dailyComparison.ProteinGoal:F2} g | Carbs: {dailyComparison.CarbsGoal:F2} g | Fat: {dailyComparison.FatGoal:F2} g";

            this.lblDailyDifferenceValue.Text =
                $"Calories: {FormatDifference(dailyComparison.CaloriesDifference, "kcal")} | Protein: {FormatDifference(dailyComparison.ProteinDifference, "g")} | Carbs: {FormatDifference(dailyComparison.CarbsDifference, "g")} | Fat: {FormatDifference(dailyComparison.FatDifference, "g")}";

            this.lblDailyStatusValue.Text = GetDailyStatusText(dailyComparison);
        }

        private void LoadTodayProgress()
        {
            var todayProgress = this.nutritionStatisticsController.GetTodayProgress(this.currentUser);

            this.lblTodayProgressValue.Text =
                $"Today -> Calories: {FormatDifference(todayProgress.CaloriesDifference, "kcal")} | Protein: {FormatDifference(todayProgress.ProteinDifference, "g")} | Carbs: {FormatDifference(todayProgress.CarbsDifference, "g")} | Fat: {FormatDifference(todayProgress.FatDifference, "g")}";
        }

        private void LoadRangeStatistics(DateTime startDate, DateTime endDate)
        {
            var rangeSummary = this.nutritionStatisticsController.GetRangeSummary(this.currentUser, startDate, endDate);

            this.lblDaysWithMenusValue.Text = rangeSummary.DaysWithMenus.ToString(CultureInfo.InvariantCulture);
            this.lblDaysGoalMetValue.Text = rangeSummary.DaysGoalMet.ToString(CultureInfo.InvariantCulture);
            this.lblDaysGoalNotMetValue.Text = rangeSummary.DaysGoalNotMet.ToString(CultureInfo.InvariantCulture);
            this.lblAverageCaloriesValue.Text = $"{rangeSummary.AverageCaloriesConsumed:F2} kcal";
            this.lblAverageMacrosValue.Text =
                $"Protein: {rangeSummary.AverageProteinConsumed:F2} g | Carbs: {rangeSummary.AverageCarbsConsumed:F2} g | Fat: {rangeSummary.AverageFatConsumed:F2} g";

            var rangeRows = rangeSummary.DailyComparisons
                .Select(item => new RangeStatisticsGridItem
                {
                    Date = item.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Calories = item.CaloriesConsumed,
                    Protein = item.ProteinConsumed,
                    Carbs = item.CarbsConsumed,
                    Fat = item.FatConsumed,
                    Status = GetRangeStatusText(item),
                })
                .ToList();

            this.dgvRangeStatistics.DataSource = null;
            this.dgvRangeStatistics.DataSource = rangeRows;
            this.dgvRangeStatistics.ClearSelection();
        }

        private static string FormatDifference(decimal difference, string unit)
        {
            if (difference > 0)
            {
                return $"Exceeded {difference:F2} {unit}";
            }

            if (difference < 0)
            {
                return $"Remaining {Math.Abs(difference):F2} {unit}";
            }

            return $"On target 0.00 {unit}";
        }

        private static string GetDailyStatusText(DailyNutritionComparison dailyComparison)
        {
            if (!dailyComparison.HasRegisteredMeals)
            {
                return "No meals registered";
            }

            return dailyComparison.IsGoalMet ? "Goal met" : "Goal not met";
        }

        private static string GetRangeStatusText(DailyNutritionComparison dailyComparison)
        {
            if (!dailyComparison.HasRegisteredMeals)
            {
                return "No meals";
            }

            return dailyComparison.IsGoalMet ? "Met" : "Not met";
        }

        private sealed class RangeStatisticsGridItem
        {
            public string Date { get; set; } = string.Empty;

            public decimal Calories { get; set; }

            public decimal Protein { get; set; }

            public decimal Carbs { get; set; }

            public decimal Fat { get; set; }

            public string Status { get; set; } = string.Empty;
        }
    }
}
