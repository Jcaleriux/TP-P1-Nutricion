namespace ClassController
{
    using ClassController.Abstractions;
    using ClassModels;

    /// <summary>
    /// Generates daily and range-based nutrition statistics from registered menus.
    /// </summary>
    public class NutritionStatisticsController : INutritionStatisticsController
    {
        private const decimal GoalTolerancePercentage = 0.10m;
        private readonly IMenuController menuController;

        /// <summary>
        /// Initializes a new instance of the <see cref="NutritionStatisticsController"/> class.
        /// </summary>
        /// <param name="menuController">The menu controller used to access registered menus.</param>
        public NutritionStatisticsController(IMenuController menuController)
        {
            this.menuController = menuController;
        }

        /// <summary>
        /// Gets the nutrition consumed by a user on a specific day.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The consumed nutrition for the requested day.</returns>
        public DailyNutritionStats GetDailyConsumption(int userId, DateTime date)
        {
            var menus = this.menuController
                .GetMenusByUser(userId)
                .Where(menu => menu.Date.Date == date.Date)
                .ToList();

            var selectedMenuProducts = menus
                .SelectMany(menu => this.menuController.GetMenuProducts(menu.MenuId))
                .ToList();

            var totals = this.menuController.CalculateTotals(selectedMenuProducts);

            return new DailyNutritionStats
            {
                Date = date.Date,
                CaloriesConsumed = totals.Calories,
                ProteinConsumed = totals.Protein,
                CarbsConsumed = totals.Carbs,
                FatConsumed = totals.Fat,
                HasRegisteredMeals = selectedMenuProducts.Count > 0,
            };
        }

        /// <summary>
        /// Gets the comparison between daily consumption and the user's goals.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The daily comparison for the requested date.</returns>
        public DailyNutritionComparison GetDailyComparison(User user, DateTime date)
        {
            var dailyConsumption = this.GetDailyConsumption(user.UserId, date);
            var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
            var macroGoals = NutritionCalculator.CalculateMacroTargets(user);

            var comparison = new DailyNutritionComparison
            {
                Date = dailyConsumption.Date,
                CaloriesConsumed = dailyConsumption.CaloriesConsumed,
                ProteinConsumed = dailyConsumption.ProteinConsumed,
                CarbsConsumed = dailyConsumption.CarbsConsumed,
                FatConsumed = dailyConsumption.FatConsumed,
                HasRegisteredMeals = dailyConsumption.HasRegisteredMeals,
                CaloriesGoal = calorieGoal,
                ProteinGoal = macroGoals.Protein,
                CarbsGoal = macroGoals.Carbs,
                FatGoal = macroGoals.Fat,
                CaloriesDifference = dailyConsumption.CaloriesConsumed - calorieGoal,
                ProteinDifference = dailyConsumption.ProteinConsumed - macroGoals.Protein,
                CarbsDifference = dailyConsumption.CarbsConsumed - macroGoals.Carbs,
                FatDifference = dailyConsumption.FatConsumed - macroGoals.Fat,
            };

            comparison.IsGoalMet = comparison.HasRegisteredMeals &&
                IsWithinTolerance(comparison.CaloriesConsumed, comparison.CaloriesGoal) &&
                IsWithinTolerance(comparison.ProteinConsumed, comparison.ProteinGoal) &&
                IsWithinTolerance(comparison.CarbsConsumed, comparison.CarbsGoal) &&
                IsWithinTolerance(comparison.FatConsumed, comparison.FatGoal);

            return comparison;
        }

        /// <summary>
        /// Gets a nutrition summary for a selected date range.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>The nutrition summary for the selected range.</returns>
        public RangeNutritionSummary GetRangeSummary(User user, DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date)
            {
                (startDate, endDate) = (endDate, startDate);
            }

            var summary = new RangeNutritionSummary
            {
                StartDate = startDate.Date,
                EndDate = endDate.Date,
            };

            for (var currentDate = summary.StartDate; currentDate <= summary.EndDate; currentDate = currentDate.AddDays(1))
            {
                summary.DailyComparisons.Add(this.GetDailyComparison(user, currentDate));
            }

            var daysWithMenus = summary.DailyComparisons
                .Where(item => item.HasRegisteredMeals)
                .ToList();

            summary.DaysInRange = (summary.EndDate - summary.StartDate).Days + 1;
            summary.DaysWithMenus = daysWithMenus.Count;
            summary.DaysGoalMet = daysWithMenus.Count(item => item.IsGoalMet);
            summary.DaysGoalNotMet = daysWithMenus.Count(item => !item.IsGoalMet);
            summary.TotalCaloriesConsumed = daysWithMenus.Sum(item => item.CaloriesConsumed);
            summary.TotalProteinConsumed = daysWithMenus.Sum(item => item.ProteinConsumed);
            summary.TotalCarbsConsumed = daysWithMenus.Sum(item => item.CarbsConsumed);
            summary.TotalFatConsumed = daysWithMenus.Sum(item => item.FatConsumed);
            summary.DailyComparisons = daysWithMenus;

            if (summary.DaysWithMenus > 0)
            {
                summary.AverageCaloriesConsumed = summary.TotalCaloriesConsumed / summary.DaysWithMenus;
                summary.AverageProteinConsumed = summary.TotalProteinConsumed / summary.DaysWithMenus;
                summary.AverageCarbsConsumed = summary.TotalCarbsConsumed / summary.DaysWithMenus;
                summary.AverageFatConsumed = summary.TotalFatConsumed / summary.DaysWithMenus;
            }

            return summary;
        }

        /// <summary>
        /// Gets the compliance summary for a specific month.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <param name="year">The year to evaluate.</param>
        /// <param name="month">The month to evaluate.</param>
        /// <returns>The compliance summary for the requested month.</returns>
        public RangeNutritionSummary GetMonthlyCompliance(User user, int year, int month)
        {
            var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Local);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return this.GetRangeSummary(user, startDate, endDate);
        }

        /// <summary>
        /// Gets the user's progress for the current day.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <returns>The comparison for the current day.</returns>
        public DailyNutritionComparison GetTodayProgress(User user)
        {
            return this.GetDailyComparison(user, DateTime.Today);
        }

        private static bool IsWithinTolerance(decimal consumed, decimal goal)
        {
            if (goal == 0)
            {
                return consumed == 0;
            }

            var minimumAccepted = goal * (1 - GoalTolerancePercentage);
            var maximumAccepted = goal * (1 + GoalTolerancePercentage);

            return consumed >= minimumAccepted && consumed <= maximumAccepted;
        }
    }
}
