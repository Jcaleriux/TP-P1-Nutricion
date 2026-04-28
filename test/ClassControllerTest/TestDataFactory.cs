using ClassModels;

namespace ClassControllerTest;

internal static class TestDataFactory
{
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

    public static Menu CreateMenu(int menuId, int userId, DateTime date)
    {
        return new Menu(menuId, userId, date);
    }

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
