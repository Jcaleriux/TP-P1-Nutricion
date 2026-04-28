using ClassModels;

namespace ClassControllerTest;

/// <summary>
/// Creates reusable test data for controller tests.
/// </summary>
internal static class TestDataFactory
{
    /// <summary>
    /// Creates an administrator user for tests.
    /// </summary>
    /// <param name="userId">The identifier assigned to the user.</param>
    /// <param name="email">The email assigned to the user.</param>
    /// <returns>A test administrator user.</returns>
    public static User CreateAdminUser(
        int userId = 1,
        string email = "admin@test.com")
    {
        return new User(
            userId,
            "Admin User",
            email,
            "Password123",
            "Maintain",
            "Moderate",
            72m,
            175m,
            30,
            "Male",
            "Balanced",
            "Admin");
    }

    /// <summary>
    /// Creates a standard user for tests.
    /// </summary>
    /// <param name="userId">The identifier assigned to the user.</param>
    /// <param name="email">The email assigned to the user.</param>
    /// <param name="isActive">Indicates whether the user is active.</param>
    /// <param name="goal">The nutrition goal assigned to the user.</param>
    /// <param name="activityLevel">The activity level assigned to the user.</param>
    /// <param name="weightKg">The weight assigned to the user in kilograms.</param>
    /// <param name="heightCm">The height assigned to the user in centimeters.</param>
    /// <param name="age">The age assigned to the user.</param>
    /// <param name="sex">The sex assigned to the user.</param>
    /// <returns>A test standard user.</returns>
    public static User CreateStandardUser(
        int userId = 2,
        string email = "user@test.com",
        bool isActive = true,
        string goal = "Maintain",
        string activityLevel = "Moderate",
        decimal weightKg = 70m,
        decimal heightCm = 170m,
        int age = 28,
        string sex = "Male")
    {
        return new User(
            userId,
            "Standard User",
            email,
            "Password123",
            goal,
            activityLevel,
            weightKg,
            heightCm,
            age,
            sex,
            "Balanced",
            "User",
            isActive);
    }

    /// <summary>
    /// Creates a product for tests.
    /// </summary>
    /// <param name="productId">The identifier assigned to the product.</param>
    /// <param name="name">The name assigned to the product.</param>
    /// <param name="calories">The calories assigned to the product.</param>
    /// <param name="protein">The protein assigned to the product.</param>
    /// <param name="carbs">The carbohydrates assigned to the product.</param>
    /// <param name="fat">The fat assigned to the product.</param>
    /// <param name="unit">The unit assigned to the product.</param>
    /// <param name="isActive">Indicates whether the product is active.</param>
    /// <returns>A test product.</returns>
    public static Product CreateProduct(
        int productId,
        string name,
        decimal calories = 100m,
        decimal protein = 10m,
        decimal carbs = 5m,
        decimal fat = 2m,
        string unit = "g",
        bool isActive = true)
    {
        return new Product(productId, name, calories, protein, carbs, fat, unit, isActive);
    }

    /// <summary>
    /// Creates a menu for tests.
    /// </summary>
    /// <param name="menuId">The identifier assigned to the menu.</param>
    /// <param name="userId">The identifier of the user who owns the menu.</param>
    /// <param name="date">The date assigned to the menu.</param>
    /// <returns>A test menu.</returns>
    public static Menu CreateMenu(int menuId, int userId, DateTime date)
    {
        return new Menu(menuId, userId, date);
    }

    /// <summary>
    /// Creates a menu product for tests.
    /// </summary>
    /// <param name="menuProductId">The identifier assigned to the menu product.</param>
    /// <param name="menuId">The identifier of the menu.</param>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="quantity">The quantity assigned to the menu product.</param>
    /// <param name="mealTime">The meal time assigned to the menu product.</param>
    /// <returns>A test menu product.</returns>
    public static MenuProduct CreateMenuProduct(
        int menuProductId,
        int menuId,
        int productId,
        decimal quantity,
        string mealTime = "Breakfast")
    {
        return new MenuProduct(menuProductId, menuId, mealTime, productId, quantity);
    }
}
