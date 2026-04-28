using ClassController;

namespace ClassControllerTest;

[TestClass]
public class NutritionCalculatorTest
{
    [TestMethod]
    public void CalculateBmi_WithPositiveHeight_ReturnsExpectedValue()
    {
        var user = TestDataFactory.CreateStandardUser(weightKg: 81m, heightCm: 180m);

        var result = NutritionCalculator.CalculateBmi(user);

        Assert.AreEqual(25m, decimal.Round(result, 0));
    }

    [TestMethod]
    public void CalculateBmi_WithZeroHeight_ReturnsZero()
    {
        var user = TestDataFactory.CreateStandardUser(heightCm: 0m);

        var result = NutritionCalculator.CalculateBmi(user);

        Assert.AreEqual(0m, result);
    }

    [TestMethod]
    public void GetBmiCategory_ReturnsExpectedCategoryPerRange()
    {
        Assert.AreEqual("Underweight", NutritionCalculator.GetBmiCategory(17m));
        Assert.AreEqual("Normal weight", NutritionCalculator.GetBmiCategory(22m));
        Assert.AreEqual("Overweight", NutritionCalculator.GetBmiCategory(27m));
        Assert.AreEqual("Obesity", NutritionCalculator.GetBmiCategory(32m));
    }

    [TestMethod]
    public void CalculateMaintenanceCalories_UsesMaleFormulaAndActivityFactor()
    {
        var user = TestDataFactory.CreateStandardUser(
            weightKg: 80m,
            heightCm: 180m,
            age: 30,
            sex: "Male",
            activityLevel: "High");

        var result = NutritionCalculator.CalculateMaintenanceCalories(user);

        Assert.AreEqual(3070.5m, result);
    }

    [TestMethod]
    public void CalculateGoalCalories_AdjustsByGoal()
    {
        var loseFatUser = TestDataFactory.CreateStandardUser(goal: "LoseFat");
        var gainMassUser = TestDataFactory.CreateStandardUser(goal: "GainMass");
        var maintainUser = TestDataFactory.CreateStandardUser(goal: "Maintain");

        var loseFatCalories = NutritionCalculator.CalculateGoalCalories(loseFatUser);
        var gainMassCalories = NutritionCalculator.CalculateGoalCalories(gainMassUser);
        var maintainCalories = NutritionCalculator.CalculateGoalCalories(maintainUser);

        Assert.AreEqual(maintainCalories - 400m, loseFatCalories);
        Assert.AreEqual(maintainCalories + 300m, gainMassCalories);
    }

    [TestMethod]
    public void CalculateMacroTargets_ReturnsDifferentDistributionsPerGoal()
    {
        var loseFatUser = TestDataFactory.CreateStandardUser(goal: "LoseFat");
        var gainMassUser = TestDataFactory.CreateStandardUser(goal: "GainMass");
        var maintainUser = TestDataFactory.CreateStandardUser(goal: "Maintain");

        var loseFat = NutritionCalculator.CalculateMacroTargets(loseFatUser);
        var gainMass = NutritionCalculator.CalculateMacroTargets(gainMassUser);
        var maintain = NutritionCalculator.CalculateMacroTargets(maintainUser);

        var loseFatGoalCalories = NutritionCalculator.CalculateGoalCalories(loseFatUser);
        var gainMassGoalCalories = NutritionCalculator.CalculateGoalCalories(gainMassUser);
        var maintainGoalCalories = NutritionCalculator.CalculateGoalCalories(maintainUser);

        Assert.AreEqual((loseFatGoalCalories * 0.35m) / 4m, loseFat.Protein);
        Assert.AreEqual((gainMassGoalCalories * 0.50m) / 4m, gainMass.Carbs);
        Assert.AreEqual((maintainGoalCalories * 0.30m) / 9m, maintain.Fat);
    }
}
