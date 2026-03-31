namespace ClassController
{
    using ClassModels;

    /// <summary>
    /// Provides nutritional calculations based on user information.
    /// </summary>
    public class NutritionCalculator
    {
        /// <summary>
        /// Calculates the body mass index of a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The BMI value.</returns>
        public decimal CalculateBmi(User user)
        {
            var heightInMeters = user.HeightCm / 100m;

            if (heightInMeters <= 0)
            {
                return 0;
            }

            return user.WeightKg / (heightInMeters * heightInMeters);
        }

        /// <summary>
        /// Gets the BMI category based on the BMI value.
        /// </summary>
        /// <param name="bmi">The BMI value.</param>
        /// <returns>The BMI category.</returns>
        public string GetBmiCategory(decimal bmi)
        {
            if (bmi < 18.5m)
            {
                return "Underweight";
            }

            if (bmi < 25m)
            {
                return "Normal weight";
            }

            if (bmi < 30m)
            {
                return "Overweight";
            }

            return "Obesity";
        }

        /// <summary>
        /// Calculates the maintenance calories of a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The maintenance calories.</returns>
        public decimal CalculateMaintenanceCalories(User user)
        {
            decimal bmr;

            if (user.Sex == "Male")
            {
                bmr = (10 * user.WeightKg) + (6.25m * user.HeightCm) - (5 * user.Age) + 5;
            }
            else
            {
                bmr = (10 * user.WeightKg) + (6.25m * user.HeightCm) - (5 * user.Age) - 161;
            }

            var activityFactor = this.GetActivityFactor(user.ActivityLevel);
            return bmr * activityFactor;
        }

        /// <summary>
        /// Calculates target calories based on the user's goal.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The target calories.</returns>
        public decimal CalculateGoalCalories(User user)
        {
            var maintenanceCalories = this.CalculateMaintenanceCalories(user);

            return user.Goal switch
            {
                "LoseFat" => maintenanceCalories - 400,
                "GainMass" => maintenanceCalories + 300,
                _ => maintenanceCalories,
            };
        }

        /// <summary>
        /// Calculates the daily macronutrient targets of a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The macro targets in grams.</returns>
        public (decimal Protein, decimal Carbs, decimal Fat) CalculateMacroTargets(User user)
        {
            var targetCalories = this.CalculateGoalCalories(user);

            decimal proteinPercentage;
            decimal carbsPercentage;
            decimal fatPercentage;

            switch (user.Goal)
            {
                case "LoseFat":
                    proteinPercentage = 0.35m;
                    carbsPercentage = 0.35m;
                    fatPercentage = 0.30m;
                    break;

                case "GainMass":
                    proteinPercentage = 0.25m;
                    carbsPercentage = 0.50m;
                    fatPercentage = 0.25m;
                    break;

                default:
                    proteinPercentage = 0.30m;
                    carbsPercentage = 0.40m;
                    fatPercentage = 0.30m;
                    break;
            }

            var proteinGrams = (targetCalories * proteinPercentage) / 4;
            var carbsGrams = (targetCalories * carbsPercentage) / 4;
            var fatGrams = (targetCalories * fatPercentage) / 9;

            return (proteinGrams, carbsGrams, fatGrams);
        }

        private decimal GetActivityFactor(string activityLevel)
        {
            return activityLevel switch
            {
                "Sedentary" => 1.2m,
                "Light" => 1.375m,
                "Moderate" => 1.55m,
                "High" => 1.725m,
                _ => 1.2m,
            };
        }
    }
}
