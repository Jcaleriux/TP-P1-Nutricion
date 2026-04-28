using ClassController;
using ClassController.Abstractions;
using ClassModels;

namespace ClassControllerTest;

[TestClass]
public class NutritionStatisticsControllerTest
{
    [TestMethod]
    public void GetDailyConsumption_WithMeals_ReturnsAccumulatedTotals()
    {
        var menuController = new NutritionMenuControllerDouble
        {
            MenusByUser = new Dictionary<int, List<Menu>>
            {
                [7] =
                [
                    TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
                    TestDataFactory.CreateMenu(2, 7, new DateTime(2026, 4, 21)),
                ],
            },
            MenuProductsByMenu = new Dictionary<int, List<MenuProduct>>
            {
                [1] =
                [
                    TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
                    TestDataFactory.CreateMenuProduct(2, 1, 2, 2m),
                ],
            },
            TotalsByMenuId = new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (400m, 30m, 20m, 10m),
            },
        };
        var controller = new NutritionStatisticsController(menuController);

        var result = controller.GetDailyConsumption(7, new DateTime(2026, 4, 20));

        Assert.AreEqual(new DateTime(2026, 4, 20), result.Date);
        Assert.AreEqual(400m, result.CaloriesConsumed);
        Assert.AreEqual(30m, result.ProteinConsumed);
        Assert.IsTrue(result.HasRegisteredMeals);
    }

    [TestMethod]
    public void GetDailyConsumption_WithoutMeals_ReturnsZeroTotals()
    {
        var controller = new NutritionStatisticsController(new NutritionMenuControllerDouble());

        var result = controller.GetDailyConsumption(7, new DateTime(2026, 4, 20));

        Assert.AreEqual(0m, result.CaloriesConsumed);
        Assert.IsFalse(result.HasRegisteredMeals);
    }

    [TestMethod]
    public void GetDailyComparison_WhenConsumptionMatchesTargetsWithinTolerance_MarksGoalAsMet()
    {
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
        var menuController = new NutritionMenuControllerDouble
        {
            MenusByUser = new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20))],
            },
            MenuProductsByMenu = new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            TotalsByMenuId = new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            },
        };
        var controller = new NutritionStatisticsController(menuController);

        var result = controller.GetDailyComparison(user, new DateTime(2026, 4, 20));

        Assert.AreEqual(calorieGoal, result.CaloriesGoal);
        Assert.IsTrue(result.IsGoalMet);
        Assert.IsTrue(result.HasRegisteredMeals);
        Assert.AreEqual(0m, result.CaloriesDifference);
    }

    [TestMethod]
    public void GetDailyComparison_WhenNoMealsExist_DoesNotMarkGoalAsMet()
    {
        var controller = new NutritionStatisticsController(new NutritionMenuControllerDouble());

        var result = controller.GetDailyComparison(TestDataFactory.CreateStandardUser(), new DateTime(2026, 4, 20));

        Assert.IsFalse(result.HasRegisteredMeals);
        Assert.IsFalse(result.IsGoalMet);
    }

    [TestMethod]
    public void GetDailyComparison_WhenConsumptionFallsOutsideTolerance_MarksGoalAsNotMet()
    {
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var menuController = new NutritionMenuControllerDouble
        {
            MenusByUser = new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20))],
            },
            MenuProductsByMenu = new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            TotalsByMenuId = new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal + 1000m, 0m, 0m, 0m),
            },
        };
        var controller = new NutritionStatisticsController(menuController);

        var result = controller.GetDailyComparison(user, new DateTime(2026, 4, 20));

        Assert.IsFalse(result.IsGoalMet);
        Assert.IsGreaterThan(0m, result.CaloriesDifference);
    }

    [TestMethod]
    public void GetRangeSummary_WithDatesInReverseOrder_NormalizesRangeAndFiltersEmptyDays()
    {
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuController = new NutritionMenuControllerDouble
        {
            MenusByUser = new Dictionary<int, List<Menu>>
            {
                [7] =
                [
                    TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
                    TestDataFactory.CreateMenu(2, 7, new DateTime(2026, 4, 22)),
                ],
            },
            MenuProductsByMenu = new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
                [2] = [TestDataFactory.CreateMenuProduct(2, 2, 1, 1m)],
            },
            TotalsByMenuId = new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
                [2] = (calorieGoal + 500m, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            },
        };
        var controller = new NutritionStatisticsController(menuController);

        var result = controller.GetRangeSummary(user, new DateTime(2026, 4, 23), new DateTime(2026, 4, 20));

        Assert.AreEqual(new DateTime(2026, 4, 20), result.StartDate);
        Assert.AreEqual(new DateTime(2026, 4, 23), result.EndDate);
        Assert.AreEqual(4, result.DaysInRange);
        Assert.AreEqual(2, result.DaysWithMenus);
        Assert.AreEqual(1, result.DaysGoalMet);
        Assert.AreEqual(1, result.DaysGoalNotMet);
        Assert.HasCount(2, result.DailyComparisons);
    }

    [TestMethod]
    public void GetRangeSummary_WithoutMenus_ReturnsZeroAverages()
    {
        var controller = new NutritionStatisticsController(new NutritionMenuControllerDouble());

        var result = controller.GetRangeSummary(TestDataFactory.CreateStandardUser(), new DateTime(2026, 4, 20), new DateTime(2026, 4, 22));

        Assert.AreEqual(3, result.DaysInRange);
        Assert.AreEqual(0, result.DaysWithMenus);
        Assert.AreEqual(0m, result.AverageCaloriesConsumed);
        Assert.IsEmpty(result.DailyComparisons);
    }

    [TestMethod]
    public void GetMonthlyCompliance_ReturnsSummaryForFullMonth()
    {
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuController = new NutritionMenuControllerDouble
        {
            MenusByUser = new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 15))],
            },
            MenuProductsByMenu = new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            TotalsByMenuId = new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            },
        };
        var controller = new NutritionStatisticsController(menuController);

        var result = controller.GetMonthlyCompliance(user, 2026, 4);

        Assert.AreEqual(new DateTime(2026, 4, 1), result.StartDate);
        Assert.AreEqual(new DateTime(2026, 4, 30), result.EndDate);
        Assert.AreEqual(30, result.DaysInRange);
        Assert.AreEqual(1, result.DaysWithMenus);
    }

    [TestMethod]
    public void GetTodayProgress_UsesTodayDate()
    {
        var user = TestDataFactory.CreateStandardUser(userId: 7);
        var calorieGoal = NutritionCalculator.CalculateGoalCalories(user);
        var macroGoals = NutritionCalculator.CalculateMacroTargets(user);
        var menuController = new NutritionMenuControllerDouble
        {
            MenusByUser = new Dictionary<int, List<Menu>>
            {
                [7] = [TestDataFactory.CreateMenu(1, 7, DateTime.Today)],
            },
            MenuProductsByMenu = new Dictionary<int, List<MenuProduct>>
            {
                [1] = [TestDataFactory.CreateMenuProduct(1, 1, 1, 1m)],
            },
            TotalsByMenuId = new Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)>
            {
                [1] = (calorieGoal, macroGoals.Protein, macroGoals.Carbs, macroGoals.Fat),
            },
        };
        var controller = new NutritionStatisticsController(menuController);

        var result = controller.GetTodayProgress(user);

        Assert.AreEqual(DateTime.Today, result.Date);
        Assert.IsTrue(result.HasRegisteredMeals);
    }

    private sealed class NutritionMenuControllerDouble : IMenuController
    {
        public Dictionary<int, List<Menu>> MenusByUser { get; init; } = new();

        public Dictionary<int, List<MenuProduct>> MenuProductsByMenu { get; init; } = new();

        public Dictionary<int, (decimal Calories, decimal Protein, decimal Carbs, decimal Fat)> TotalsByMenuId { get; init; } = new();

        public List<Menu> GetMenusByUser(int userId)
        {
            return this.MenusByUser.TryGetValue(userId, out var menus)
                ? menus.ToList()
                : new List<Menu>();
        }

        public List<MenuProduct> GetMenuProducts(int menuId)
        {
            return this.MenuProductsByMenu.TryGetValue(menuId, out var products)
                ? products.ToList()
                : new List<MenuProduct>();
        }

        public bool CreateMenu(int userId, DateTime date, List<MenuProduct> menuProducts)
        {
            throw new NotSupportedException();
        }

        public bool UpdateMenu(int menuId, DateTime date, List<MenuProduct> menuProducts)
        {
            throw new NotSupportedException();
        }

        public (decimal Calories, decimal Protein, decimal Carbs, decimal Fat) CalculateTotals(List<MenuProduct> menuProducts)
        {
            if (menuProducts.Count == 0)
            {
                return (0m, 0m, 0m, 0m);
            }

            var menuId = menuProducts[0].MenuId;
            return this.TotalsByMenuId.TryGetValue(menuId, out var totals)
                ? totals
                : (0m, 0m, 0m, 0m);
        }

        public bool DeleteMenu(int menuId)
        {
            throw new NotSupportedException();
        }
    }
}
