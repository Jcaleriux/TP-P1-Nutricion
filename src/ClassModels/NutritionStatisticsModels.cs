namespace ClassModels
{
    /// <summary>
    /// Represents the nutrition consumed by a user on a specific day.
    /// </summary>
    public class DailyNutritionStats
    {
        /// <summary>
        /// Gets or sets the date associated with the statistics.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the total calories consumed on the day.
        /// </summary>
        public decimal CaloriesConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total protein consumed on the day.
        /// </summary>
        public decimal ProteinConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total carbohydrates consumed on the day.
        /// </summary>
        public decimal CarbsConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total fat consumed on the day.
        /// </summary>
        public decimal FatConsumed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user registered meals on the day.
        /// </summary>
        public bool HasRegisteredMeals { get; set; }
    }

    /// <summary>
    /// Represents the comparison between daily consumption and the user's nutrition goals.
    /// </summary>
    public class DailyNutritionComparison : DailyNutritionStats
    {
        /// <summary>
        /// Gets or sets the calorie goal for the day.
        /// </summary>
        public decimal CaloriesGoal { get; set; }

        /// <summary>
        /// Gets or sets the protein goal for the day.
        /// </summary>
        public decimal ProteinGoal { get; set; }

        /// <summary>
        /// Gets or sets the carbohydrate goal for the day.
        /// </summary>
        public decimal CarbsGoal { get; set; }

        /// <summary>
        /// Gets or sets the fat goal for the day.
        /// </summary>
        public decimal FatGoal { get; set; }

        /// <summary>
        /// Gets or sets the calorie difference for the day. Positive values mean the goal was exceeded.
        /// </summary>
        public decimal CaloriesDifference { get; set; }

        /// <summary>
        /// Gets or sets the protein difference for the day. Positive values mean the goal was exceeded.
        /// </summary>
        public decimal ProteinDifference { get; set; }

        /// <summary>
        /// Gets or sets the carbohydrate difference for the day. Positive values mean the goal was exceeded.
        /// </summary>
        public decimal CarbsDifference { get; set; }

        /// <summary>
        /// Gets or sets the fat difference for the day. Positive values mean the goal was exceeded.
        /// </summary>
        public decimal FatDifference { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user met the daily nutrition goal.
        /// </summary>
        public bool IsGoalMet { get; set; }
    }

    /// <summary>
    /// Represents the nutrition summary for a selected date range.
    /// </summary>
    public class RangeNutritionSummary
    {
        /// <summary>
        /// Gets or sets the start date of the range.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the range.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of days included in the range.
        /// </summary>
        public int DaysInRange { get; set; }

        /// <summary>
        /// Gets or sets the amount of days with at least one registered meal.
        /// </summary>
        public int DaysWithMenus { get; set; }

        /// <summary>
        /// Gets or sets the amount of days where the goal was met.
        /// </summary>
        public int DaysGoalMet { get; set; }

        /// <summary>
        /// Gets or sets the amount of days where the goal was not met.
        /// </summary>
        public int DaysGoalNotMet { get; set; }

        /// <summary>
        /// Gets or sets the total calories consumed in the range.
        /// </summary>
        public decimal TotalCaloriesConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total protein consumed in the range.
        /// </summary>
        public decimal TotalProteinConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total carbohydrates consumed in the range.
        /// </summary>
        public decimal TotalCarbsConsumed { get; set; }

        /// <summary>
        /// Gets or sets the total fat consumed in the range.
        /// </summary>
        public decimal TotalFatConsumed { get; set; }

        /// <summary>
        /// Gets or sets the average calories consumed per day in the range.
        /// </summary>
        public decimal AverageCaloriesConsumed { get; set; }

        /// <summary>
        /// Gets or sets the average protein consumed per day in the range.
        /// </summary>
        public decimal AverageProteinConsumed { get; set; }

        /// <summary>
        /// Gets or sets the average carbohydrates consumed per day in the range.
        /// </summary>
        public decimal AverageCarbsConsumed { get; set; }

        /// <summary>
        /// Gets or sets the average fat consumed per day in the range.
        /// </summary>
        public decimal AverageFatConsumed { get; set; }

        /// <summary>
        /// Gets or sets the daily comparisons generated for the range.
        /// </summary>
        public List<DailyNutritionComparison> DailyComparisons { get; set; } = new List<DailyNutritionComparison>();
    }
}
