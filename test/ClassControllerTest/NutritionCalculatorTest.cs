using ClassController;

namespace ClassControllerTest;

/// <summary>
/// Test class for <see cref="NutritionCalculator"/>, covering BMI, calorie goals, and macro target calculations.
/// </summary>
[TestClass]
public class NutritionCalculatorTest
{
    /// <summary>
    /// Calculates BMI with positive height returns expected value.
    /// </summary>
    [TestMethod]
    public void CalculateBmi_WithPositiveHeight_ReturnsExpectedValue()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(weightKg: 81m, heightCm: 180m);

        // Act
        var result = NutritionCalculator.CalculateBmi(user);

        // Assert
        Assert.AreEqual(25m, decimal.Round(result, 0));
    }

    /// <summary>
    /// Calculates BMI with zero height returns zero.
    /// </summary>
    [TestMethod]
    public void CalculateBmi_WithZeroHeight_ReturnsZero()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(heightCm: 0m);

        // Act
        var result = NutritionCalculator.CalculateBmi(user);

        // Assert
        Assert.AreEqual(0m, result);
    }

    /// <summary>
    /// Gets BMI category returns expected category per range.
    /// </summary>
    [TestMethod]
    public void GetBmiCategory_ReturnsExpectedCategoryPerRange()
    {
        // Arrange
        const decimal underweightBmi = 17m;
        const decimal normalWeightBmi = 22m;
        const decimal overweightBmi = 27m;
        const decimal obesityBmi = 32m;

        // Act
        var underweightCategory = NutritionCalculator.GetBmiCategory(underweightBmi);
        var normalWeightCategory = NutritionCalculator.GetBmiCategory(normalWeightBmi);
        var overweightCategory = NutritionCalculator.GetBmiCategory(overweightBmi);
        var obesityCategory = NutritionCalculator.GetBmiCategory(obesityBmi);

        // Assert
        Assert.AreEqual("Underweight", underweightCategory);
        Assert.AreEqual("Normal weight", normalWeightCategory);
        Assert.AreEqual("Overweight", overweightCategory);
        Assert.AreEqual("Obesity", obesityCategory);
    }

    /// <summary>
    /// Calculates maintenance calories uses male formula and activity factor.
    /// </summary>
    [TestMethod]
    public void CalculateMaintenanceCalories_UsesMaleFormulaAndActivityFactor()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(
            weightKg: 80m,
            heightCm: 180m,
            age: 30,
            sex: "Male",
            activityLevel: "High");

        // Act
        var result = NutritionCalculator.CalculateMaintenanceCalories(user);

        // Assert
        Assert.AreEqual(3070.5m, result);
    }

    /// <summary>
    /// Calculates goal calories adjusts by goal.
    /// </summary>
    [TestMethod]
    public void CalculateGoalCalories_AdjustsByGoal()
    {
        // Arrange
        var loseFatUser = TestDataFactory.CreateStandardUser(goal: "LoseFat");
        var gainMassUser = TestDataFactory.CreateStandardUser(goal: "GainMass");
        var maintainUser = TestDataFactory.CreateStandardUser(goal: "Maintain");

        // Act
        var loseFatCalories = NutritionCalculator.CalculateGoalCalories(loseFatUser);
        var gainMassCalories = NutritionCalculator.CalculateGoalCalories(gainMassUser);
        var maintainCalories = NutritionCalculator.CalculateGoalCalories(maintainUser);

        // Assert
        Assert.AreEqual(maintainCalories - 400m, loseFatCalories);
        Assert.AreEqual(maintainCalories + 300m, gainMassCalories);
    }

    /// <summary>
    /// Calculates macro targets returns different distributions per goal.
    /// </summary>
    [TestMethod]
    public void CalculateMacroTargets_ReturnsDifferentDistributionsPerGoal()
    {
        // Arrange
        var loseFatUser = TestDataFactory.CreateStandardUser(goal: "LoseFat");
        var gainMassUser = TestDataFactory.CreateStandardUser(goal: "GainMass");
        var maintainUser = TestDataFactory.CreateStandardUser(goal: "Maintain");

        // Act
        var loseFat = NutritionCalculator.CalculateMacroTargets(loseFatUser);
        var gainMass = NutritionCalculator.CalculateMacroTargets(gainMassUser);
        var maintain = NutritionCalculator.CalculateMacroTargets(maintainUser);
        var loseFatGoalCalories = NutritionCalculator.CalculateGoalCalories(loseFatUser);
        var gainMassGoalCalories = NutritionCalculator.CalculateGoalCalories(gainMassUser);
        var maintainGoalCalories = NutritionCalculator.CalculateGoalCalories(maintainUser);

        // Assert
        Assert.AreEqual((loseFatGoalCalories * 0.35m) / 4m, loseFat.Protein);
        Assert.AreEqual((gainMassGoalCalories * 0.50m) / 4m, gainMass.Carbs);
        Assert.AreEqual((maintainGoalCalories * 0.30m) / 9m, maintain.Fat);
    }
}
