using ClassController;
using ClassModels;

namespace ClassControllerTest;

[TestClass]
public class MenuControllerTest
{
    [TestMethod]
    public void CreateMenu_WithValidProducts_SavesMenuAndMenuProducts()
    {
        var menuRepository = new InMemoryRepository<Menu>();
        var menuProductRepository = new InMemoryRepository<MenuProduct>();
        var controller = this.CreateController(menuRepository: menuRepository, menuProductRepository: menuProductRepository);

        var result = controller.CreateMenu(
            7,
            new DateTime(2026, 4, 20),
            [TestDataFactory.CreateMenuProduct(0, 0, 1, 2m)]);

        Assert.IsTrue(result);
        Assert.AreEqual(1, menuRepository.SaveCallCount);
        Assert.AreEqual(1, menuProductRepository.SaveCallCount);
        Assert.AreEqual(1, menuRepository.LastSavedSnapshot!.Single().MenuId);
        Assert.AreEqual(1, menuProductRepository.LastSavedSnapshot!.Single().MenuId);
    }

    [TestMethod]
    public void CreateMenu_WithNullProducts_ReturnsFalse()
    {
        var controller = this.CreateController();

        var result = controller.CreateMenu(7, new DateTime(2026, 4, 20), null!);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateMenu_WithEmptyProducts_ReturnsFalse()
    {
        var controller = this.CreateController();

        var result = controller.CreateMenu(7, new DateTime(2026, 4, 20), []);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void UpdateMenu_WithExistingMenu_ReplacesProductsAndUpdatesDate()
    {
        var menuRepository = new InMemoryRepository<Menu>(
        [
            TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
        ]);
        var menuProductRepository = new InMemoryRepository<MenuProduct>(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
        ]);
        var controller = this.CreateController(menuRepository, menuProductRepository);

        var result = controller.UpdateMenu(
            1,
            new DateTime(2026, 4, 21),
            [TestDataFactory.CreateMenuProduct(0, 0, 2, 3m, "Lunch")]);

        Assert.IsTrue(result);
        Assert.AreEqual(new DateTime(2026, 4, 21), menuRepository.LastSavedSnapshot!.Single().Date);
        Assert.HasCount(1, menuProductRepository.LastSavedSnapshot!);
        Assert.AreEqual(2, menuProductRepository.LastSavedSnapshot!.Single().ProductId);
    }

    [TestMethod]
    public void UpdateMenu_WithUnknownMenu_ReturnsFalse()
    {
        var menuRepository = new InMemoryRepository<Menu>();
        var menuProductRepository = new InMemoryRepository<MenuProduct>();
        var controller = this.CreateController(menuRepository, menuProductRepository);

        var result = controller.UpdateMenu(99, new DateTime(2026, 4, 21), [TestDataFactory.CreateMenuProduct(0, 0, 2, 3m)]);

        Assert.IsFalse(result);
        Assert.AreEqual(0, menuRepository.SaveCallCount);
    }

    [TestMethod]
    public void UpdateMenu_WithEmptyProducts_ReturnsFalse()
    {
        var controller = this.CreateController(
            new InMemoryRepository<Menu>([TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20))]),
            new InMemoryRepository<MenuProduct>());

        var result = controller.UpdateMenu(1, new DateTime(2026, 4, 21), []);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void DeleteMenu_WithExistingMenu_RemovesMenuAndRelations()
    {
        var menuRepository = new InMemoryRepository<Menu>(
        [
            TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
        ]);
        var menuProductRepository = new InMemoryRepository<MenuProduct>(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
            TestDataFactory.CreateMenuProduct(2, 1, 2, 1m),
        ]);
        var controller = this.CreateController(menuRepository, menuProductRepository);

        var result = controller.DeleteMenu(1);

        Assert.IsTrue(result);
        Assert.IsEmpty(menuRepository.LastSavedSnapshot!);
        Assert.IsEmpty(menuProductRepository.LastSavedSnapshot!);
    }

    [TestMethod]
    public void DeleteMenu_WithUnknownMenu_ReturnsFalse()
    {
        var controller = this.CreateController();

        var result = controller.DeleteMenu(99);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetMenusByUser_ReturnsOnlyMatchingUserMenusOrderedByDate()
    {
        var controller = this.CreateController(
            new InMemoryRepository<Menu>(
            [
                TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 21)),
                TestDataFactory.CreateMenu(2, 8, new DateTime(2026, 4, 19)),
                TestDataFactory.CreateMenu(3, 7, new DateTime(2026, 4, 20)),
            ]),
            new InMemoryRepository<MenuProduct>());

        var result = controller.GetMenusByUser(7);

        CollectionAssert.AreEqual(new[] { 3, 1 }, result.Select(item => item.MenuId).ToArray());
    }

    [TestMethod]
    public void GetMenuProducts_ReturnsOnlyMenuProductsForMenu()
    {
        var controller = this.CreateController(
            new InMemoryRepository<Menu>(),
            new InMemoryRepository<MenuProduct>(
            [
                TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
                TestDataFactory.CreateMenuProduct(2, 2, 1, 1m),
                TestDataFactory.CreateMenuProduct(3, 1, 2, 2m),
            ]));

        var result = controller.GetMenuProducts(1);

        CollectionAssert.AreEqual(new[] { 1, 3 }, result.Select(item => item.MenuProductId).ToArray());
    }

    [TestMethod]
    public void CalculateTotals_SumsNutritionValuesUsingProductQuantities()
    {
        var productController = new ProductControllerDouble();
        productController.Products.AddRange(
        [
            TestDataFactory.CreateProduct(1, "Apple", 50m, 1m, 10m, 0.5m),
            TestDataFactory.CreateProduct(2, "Chicken", 150m, 20m, 0m, 3m),
        ]);
        var controller = new MenuController(new InMemoryRepository<Menu>(), new InMemoryRepository<MenuProduct>(), productController);

        var result = controller.CalculateTotals(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 1, 2m),
            TestDataFactory.CreateMenuProduct(2, 1, 2, 1.5m),
        ]);

        Assert.AreEqual(325m, result.Calories);
        Assert.AreEqual(32m, result.Protein);
        Assert.AreEqual(20m, result.Carbs);
        Assert.AreEqual(5.5m, result.Fat);
    }

    [TestMethod]
    public void CalculateTotals_IgnoresUnknownProducts()
    {
        var productController = new ProductControllerDouble();
        productController.Products.Add(TestDataFactory.CreateProduct(1, "Apple", 50m, 1m, 10m, 0.5m));
        var controller = new MenuController(new InMemoryRepository<Menu>(), new InMemoryRepository<MenuProduct>(), productController);

        var result = controller.CalculateTotals(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 999, 2m),
            TestDataFactory.CreateMenuProduct(2, 1, 1, 1m),
        ]);

        Assert.AreEqual(50m, result.Calories);
        Assert.AreEqual(1m, result.Protein);
        Assert.AreEqual(10m, result.Carbs);
        Assert.AreEqual(0.5m, result.Fat);
    }

    private MenuController CreateController(
        InMemoryRepository<Menu>? menuRepository = null,
        InMemoryRepository<MenuProduct>? menuProductRepository = null,
        ProductControllerDouble? productController = null)
    {
        return new MenuController(
            menuRepository ?? new InMemoryRepository<Menu>(),
            menuProductRepository ?? new InMemoryRepository<MenuProduct>(),
            productController ?? new ProductControllerDouble());
    }
}
