namespace ClassController.Abstractions
{
    using ClassModels;

    /// <summary>
    /// Defines nutrition statistics operations for daily and range-based reports.
    /// </summary>
    public interface INutritionStatisticsController
    {
        /// <summary>
        /// Gets the nutrition consumed by a user on a specific day.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The consumed nutrition for the requested day.</returns>
        public DailyNutritionStats GetDailyConsumption(int userId, DateTime date);

        /// <summary>
        /// Gets the comparison between daily consumption and the user's goals.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <param name="date">The date to evaluate.</param>
        /// <returns>The daily comparison for the requested date.</returns>
        public DailyNutritionComparison GetDailyComparison(User user, DateTime date);

        /// <summary>
        /// Gets a nutrition summary for a selected date range.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>The nutrition summary for the selected range.</returns>
        public RangeNutritionSummary GetRangeSummary(User user, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the compliance summary for a specific month.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <param name="year">The year to evaluate.</param>
        /// <param name="month">The month to evaluate.</param>
        /// <returns>The compliance summary for the requested month.</returns>
        public RangeNutritionSummary GetMonthlyCompliance(User user, int year, int month);

        /// <summary>
        /// Gets the user's progress for the current day.
        /// </summary>
        /// <param name="user">The user to evaluate.</param>
        /// <returns>The comparison for the current day.</returns>
        public DailyNutritionComparison GetTodayProgress(User user);
    }
}
