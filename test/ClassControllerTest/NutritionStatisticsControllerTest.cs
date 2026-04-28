using ClassController;
using ClassController.Abstractions;
using ClassModels;
using Moq;

namespace ClassControllerTest;

/// <summary>
/// Test class for <see cref="NutritionStatisticsController"/>, covering daily and range-based nutrition summaries
/// and goal compliance calculations.
/// </summary>
[TestClass]
public class NutritionStatisticsControllerTest
{
    /// <summary>
    /// Gets daily consumption with meals returns accumulated totals.
    /// </summary>
    [TestMethod]
    public void GetDailyConsumption_WithMeals_ReturnsAccumulatedTotals()
    {
        // Arrange
        var menuControllerMock = CreateMenuControllerMock(
            menusByUser: new Dictionary<int, List<Menu>>
            {
                [7] =
                [
                    TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
                    TestDataFactory.CreateMenu(2, 7, new DateTime(2026, 4, 21)),
                ],
            },
            menuProductsByMenu: new Dictionary<int, List<MenuProduct>>
            {
                [1] =
                [
                    TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
                    TestDataFactory.CreateMenuProduct(2, 1, 2, 2m),
                ],
            },
            totalsByMenuId: new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (400m, 30m, 20m, 10m),
            });
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetDailyConsumption(7, new DateTime(2026, 4, 20));

        // Assert
        Assert.AreEqual(new DateTime(2026, 4, 20), result.Date);
        Assert.AreEqual(400m, result.CaloriesConsumed);
        Assert.AreEqual(30m, result.ProteinConsumed);
        Assert.IsTrue(result.HasRegisteredMeals);
    }

    /// <summary>
    /// Gets daily consumption without meals returns zero totals.
    /// </summary>
    [TestMethod]
    public void GetDailyConsumption_WithoutMeals_ReturnsZeroTotals()
    {
        // Arrange
        var menuControllerMock = CreateMenuControllerMock();
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetDailyConsumption(7, new DateTime(2026, 4, 20));

        // Assert
        Assert.AreEqual(0m, result.CaloriesConsumed);
        Assert.IsFalse(result.HasRegisteredMeals);
    }

    /// <summary>
    /// Gets daily comparison when consumption matches targets within tolerance marks goal as met.
    /// </summary>
    [TestMethod]
    public void GetDailyComparison_WhenConsumptionMatchesTargetsWithinTolerance_MarksGoalAsMet()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(
            userId: 7,
            goal: "Maintain",
            activityLevel: "Moderate",
            weightKg: 70m,
            heightCm: 175m,
            age: 30,
            sex: "Male");
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuControllerMock = CreateMenuControllerMock(
            menusByUser: new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20))],
            },
            menuProductsByMenu: new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            totalsByMenuId: new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            });
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetDailyComparison(user, new DateTime(2026, 4, 20));

        // Assert
        Assert.AreEqual(calorieGoal, result.CaloriesGoal);
        Assert.IsTrue(result.IsGoalMet);
        Assert.IsTrue(result.HasRegisteredMeals);
        Assert.AreEqual(0m, result.CaloriesDifference);
    }

    /// <summary>
    /// Gets daily comparison when no meals exist does not mark goal as met.
    /// </summary>
    [TestMethod]
    public void GetDailyComparison_WhenNoMealsExist_DoesNotMarkGoalAsMet()
    {
        // Arrange
        var menuControllerMock = CreateMenuControllerMock();
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetDailyComparison(TestDataFactory.CreateStandardUser(), new DateTime(2026, 4, 20));

        // Assert
        Assert.IsFalse(result.HasRegisteredMeals);
        Assert.IsFalse(result.IsGoalMet);
    }

    /// <summary>
    /// Gets daily comparison when consumption falls outside tolerance marks goal as not met.
    /// </summary>
    [TestMethod]
    public void GetDailyComparison_WhenConsumptionFallsOutsideTolerance_MarksGoalAsNotMet()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var menuControllerMock = CreateMenuControllerMock(
            menusByUser: new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20))],
            },
            menuProductsByMenu: new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            totalsByMenuId: new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal + 1000m, 0m, 0m, 0m),
            });
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetDailyComparison(user, new DateTime(2026, 4, 20));

        // Assert
        Assert.IsFalse(result.IsGoalMet);
        Assert.IsTrue(result.CaloriesDifference > 0m);
    }

    /// <summary>
    /// Gets range summary with dates in reverse order normalizes range and filters empty days.
    /// </summary>
    [TestMethod]
    public void GetRangeSummary_WithDatesInReverseOrder_NormalizesRangeAndFiltersEmptyDays()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuControllerMock = CreateMenuControllerMock(
            menusByUser: new Dictionary<int, List<Menu>>
            {
                [7] =
                [
                    TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
                    TestDataFactory.CreateMenu(2, 7, new DateTime(2026, 4, 22)),
                ],
            },
            menuProductsByMenu: new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
                [2] = [TestDataFactory.CreateMenuProduct(2, 2, 1, 1m)],
            },
            totalsByMenuId: new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
                [2] = (calorieGoal + 500m, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            });
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetRangeSummary(user, new DateTime(2026, 4, 23), new DateTime(2026, 4, 20));

        // Assert
        Assert.AreEqual(new DateTime(2026, 4, 20), result.StartDate);
        Assert.AreEqual(new DateTime(2026, 4, 23), result.EndDate);
        Assert.AreEqual(4, result.DaysInRange);
        Assert.AreEqual(2, result.DaysWithMenus);
        Assert.AreEqual(1, result.DaysGoalMet);
        Assert.AreEqual(1, result.DaysGoalNotMet);
        Assert.AreEqual(2, result.DailyComparisons.Count);
    }

    /// <summary>
    /// Gets range summary without menus returns zero averages.
    /// </summary>
    [TestMethod]
    public void GetRangeSummary_WithoutMenus_ReturnsZeroAverages()
    {
        // Arrange
        var menuControllerMock = CreateMenuControllerMock();
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetRangeSummary(TestDataFactory.CreateStandardUser(), new DateTime(2026, 4, 20), new DateTime(2026, 4, 22));

        // Assert
        Assert.AreEqual(3, result.DaysInRange);
        Assert.AreEqual(0, result.DaysWithMenus);
        Assert.AreEqual(0m, result.AverageCaloriesConsumed);
        Assert.AreEqual(0, result.DailyComparisons.Count);
    }

    /// <summary>
    /// Gets monthly compliance returns summary for full month.
    /// </summary>
    [TestMethod]
    public void GetMonthlyCompliance_ReturnsSummaryForFullMonth()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuControllerMock = CreateMenuControllerMock(
            menusByUser: new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 15))],
            },
            menuProductsByMenu: new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            totalsByMenuId: new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            });
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetMonthlyCompliance(user, 2026, 4);

        // Assert
        Assert.AreEqual(new DateTime(2026, 4, 1), result.StartDate);
        Assert.AreEqual(new DateTime(2026, 4, 30), result.EndDate);
        Assert.AreEqual(30, result.DaysInRange);
        Assert.AreEqual(1, result.DaysWithMenus);
    }

    /// <summary>
    /// Gets today progress uses today date.
    /// </summary>
    [TestMethod]
    public void GetTodayProgress_UsesTodayDate()
    {
        // Arrange
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuControllerMock = CreateMenuControllerMock(
            menusByUser: new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, DateTime.Today)],
            },
            menuProductsByMenu: new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            totalsByMenuId: new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            });
        var controller = new NutritionStatisticsController(menuControllerMock.Object);

        // Act
        var result = controller.GetTodayProgress(user);

        // Assert
        Assert.AreEqual(DateTime.Today, result.Date);
        Assert.IsTrue(result.HasRegisteredMeals);
    }

    private static Mock<IMenuController> CreateMenuControllerMock(
        Dictionary<int, List<Menu>>? menusByUser = null,
        Dictionary<int, List<MenuProduct>>? menuProductsByMenu = null,
        Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>? totalsByMenuId = null)
    {
        var menuControllerMock = new Mock<IMenuController>(MockBehavior.Strict);
        var menus = menusByUser ?? new Dictionary<int, List<Menu>>();
        var menuProducts = menuProductsByMenu ?? new Dictionary<int, List<MenuProduct>>();
        var totals = totalsByMenuId ?? new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>();

        menuControllerMock
            .Setup(controller => controller.GetMenusByUser(It.IsAny<int>()))
            .Returns<int>(userId =>
                menus.TryGetValue(userId, out var userMenus)
                    ? userMenus.ToList()
                    : new List<Menu>());

        menuControllerMock
            .Setup(controller => controller.GetMenuProducts(It.IsAny<int>()))
            .Returns<int>(menuId =>
                menuProducts.TryGetValue(menuId, out var products)
                    ? products.ToList()
                    : new List<MenuProduct>());

        menuControllerMock
            .Setup(controller => controller.CalculateTotals(It.IsAny<List<MenuProduct>>()))
            .Returns<List<MenuProduct>>(selectedMenuProducts =>
            {
                if (selectedMenuProducts.Count == 0)
                {
                    return (0m, 0m, 0m, 0m);
                }

                var menuId = selectedMenuProducts[0].MenuId;
                return totals.TryGetValue(menuId, out var selectedTotals)
                    ? selectedTotals
                    : (0m, 0m, 0m, 0m);
            });

        return menuControllerMock;
    }
}
