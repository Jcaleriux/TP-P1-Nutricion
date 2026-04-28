using ClassController;
using ClassController.Abstractions;
using ClassModels;
using Moq;

namespace ClassControllerTest;

/// <summary>
/// Test class for <see cref="MenuController"/>, covering menu creation, updates, deletion, retrieval,
/// and nutrition total calculations.
/// </summary>
[TestClass]
public class MenuControllerTest
{
    /// <summary>
    /// Creates the menu with valid products saves menu and menu products.
    /// </summary>
    [TestMethod]
    public void CreateMenu_WithValidProducts_SavesMenuAndMenuProducts()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>();
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>();
        var controller = this.CreateController(
            menuRepositoryContext: menuRepositoryContext,
            menuProductRepositoryContext: menuProductRepositoryContext);

        // Act
        var result = controller.CreateMenu(
            7,
            new DateTime(2026, 4, 20),
            [TestDataFactory.CreateMenuProduct(0, 0, 1, 2m)]);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(1, menuRepositoryContext.LastSavedSnapshot!.Single().MenuId);
        Assert.AreEqual(1, menuProductRepositoryContext.LastSavedSnapshot!.Single().MenuId);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Once);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Once);
    }

    /// <summary>
    /// Creates the menu with null products returns false.
    /// </summary>
    [TestMethod]
    public void CreateMenu_WithNullProducts_ReturnsFalse()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>();
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>();
        var controller = this.CreateController(
            menuRepositoryContext: menuRepositoryContext,
            menuProductRepositoryContext: menuProductRepositoryContext);

        // Act
        var result = controller.CreateMenu(7, new DateTime(2026, 4, 20), null!);

        // Assert
        Assert.IsFalse(result);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Never);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Never);
    }

    /// <summary>
    /// Creates the menu with empty products returns false.
    /// </summary>
    [TestMethod]
    public void CreateMenu_WithEmptyProducts_ReturnsFalse()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>();
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>();
        var controller = this.CreateController(
            menuRepositoryContext: menuRepositoryContext,
            menuProductRepositoryContext: menuProductRepositoryContext);

        // Act
        var result = controller.CreateMenu(7, new DateTime(2026, 4, 20), []);

        // Assert
        Assert.IsFalse(result);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Never);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Never);
    }

    /// <summary>
    /// Updates the menu with existing menu replaces products and updates date.
    /// </summary>
    [TestMethod]
    public void UpdateMenu_WithExistingMenu_ReplacesProductsAndUpdatesDate()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>(
        [
            TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
        ]);
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
        ]);
        var controller = this.CreateController(menuRepositoryContext, menuProductRepositoryContext);

        // Act
        var result = controller.UpdateMenu(
            1,
            new DateTime(2026, 4, 21),
            [TestDataFactory.CreateMenuProduct(0, 0, 2, 3m, "Lunch")]);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(new DateTime(2026, 4, 21), menuRepositoryContext.LastSavedSnapshot!.Single().Date);
        Assert.AreEqual(1, menuProductRepositoryContext.LastSavedSnapshot!.Count);
        Assert.AreEqual(2, menuProductRepositoryContext.LastSavedSnapshot!.Single().ProductId);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Once);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Once);
    }
    /// <summary>
    /// Updates the menu with unknown menu returns false.
    /// </summary>
    [TestMethod]
    public void UpdateMenu_WithUnknownMenu_ReturnsFalse()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>();
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>();
        var controller = this.CreateController(menuRepositoryContext, menuProductRepositoryContext);

        // Act
        var result = controller.UpdateMenu(99, new DateTime(2026, 4, 21), [TestDataFactory.CreateMenuProduct(0, 0, 2, 3m)]);

        // Assert
        Assert.IsFalse(result);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Never);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Never);
    }

    /// <summary>
    /// Updates the menu with empty products returns false.
    /// </summary>
    [TestMethod]
    public void UpdateMenu_WithEmptyProducts_ReturnsFalse()
    {
        // Arrange
        var controller = this.CreateController(
            new RepositoryMockContext<Menu>([TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20))]),
            new RepositoryMockContext<MenuProduct>());

        // Act
        var result = controller.UpdateMenu(1, new DateTime(2026, 4, 21), []);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    /// Deletes the menu with existing menu removes menu and relations.
    /// </summary>
    [TestMethod]
    public void DeleteMenu_WithExistingMenu_RemovesMenuAndRelations()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>(
        [
            TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 20)),
        ]);
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
            TestDataFactory.CreateMenuProduct(2, 1, 2, 1m),
        ]);
        var controller = this.CreateController(menuRepositoryContext, menuProductRepositoryContext);

        // Act
        var result = controller.DeleteMenu(1);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(0, menuRepositoryContext.LastSavedSnapshot!.Count);
        Assert.AreEqual(0, menuProductRepositoryContext.LastSavedSnapshot!.Count);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Once);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Once);
    }

    /// <summary>
    /// Deletes the menu with unknown menu returns false.
    /// </summary>
    [TestMethod]
    public void DeleteMenu_WithUnknownMenu_ReturnsFalse()
    {
        // Arrange
        var menuRepositoryContext = new RepositoryMockContext<Menu>();
        var menuProductRepositoryContext = new RepositoryMockContext<MenuProduct>();
        var controller = this.CreateController(menuRepositoryContext, menuProductRepositoryContext);

        // Act
        var result = controller.DeleteMenu(99);

        // Assert
        Assert.IsFalse(result);
        menuRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Menu>>()), Times.Never);
        menuProductRepositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<MenuProduct>>()), Times.Never);
    }

    /// <summary>
    /// Gets menus by user returns only matching user menus ordered by date.
    /// </summary>
    [TestMethod]
    public void GetMenusByUser_ReturnsOnlyMatchingUserMenusOrderedByDate()
    {
        // Arrange
        var controller = this.CreateController(
            new RepositoryMockContext<Menu>(
            [
                TestDataFactory.CreateMenu(1, 7, new DateTime(2026, 4, 21)),
                TestDataFactory.CreateMenu(2, 8, new DateTime(2026, 4, 19)),
                TestDataFactory.CreateMenu(3, 7, new DateTime(2026, 4, 20)),
            ]),
            new RepositoryMockContext<MenuProduct>());

        // Act
        var result = controller.GetMenusByUser(7);

        // Assert
        CollectionAssert.AreEqual(new[] { 3, 1 }, result.Select(item => item.MenuId).ToArray());
    }

    /// <summary>
    /// Gets menu products returns only menu products for menu.
    /// </summary>
    [TestMethod]
    public void GetMenuProducts_ReturnsOnlyMenuProductsForMenu()
    {
        // Arrange
        var controller = this.CreateController(
            new RepositoryMockContext<Menu>(),
            new RepositoryMockContext<MenuProduct>(
            [
                TestDataFactory.CreateMenuProduct(1, 1, 1, 1m),
                TestDataFactory.CreateMenuProduct(2, 2, 1, 1m),
                TestDataFactory.CreateMenuProduct(3, 1, 2, 2m),
            ]));

        // Act
        var result = controller.GetMenuProducts(1);

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 3 }, result.Select(item => item.MenuProductId).ToArray());
    }

    /// <summary>
    /// Calculates totals sums nutrition values using product quantities.
    /// </summary>
    [TestMethod]
    public void CalculateTotals_SumsNutritionValuesUsingProductQuantities()
    {
        // Arrange
        var productControllerMock = new Mock<IProductController>(MockBehavior.Strict);
        productControllerMock
            .Setup(controller => controller.GetAllProducts())
            .Returns(
            [
                TestDataFactory.CreateProduct(1, "Apple", 50m, 1m, 10m, 0.5m),
                TestDataFactory.CreateProduct(2, "Chicken", 150m, 20m, 0m, 3m),
            ]);
        var controller = new MenuController(
            new RepositoryMockContext<Menu>().Mock.Object,
            new RepositoryMockContext<MenuProduct>().Mock.Object,
            productControllerMock.Object);

        // Act
        var result = controller.CalculateTotals(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 1, 2m),
            TestDataFactory.CreateMenuProduct(2, 1, 2, 1.5m),
        ]);

        // Assert
        Assert.AreEqual(325m, result.Calories);
        Assert.AreEqual(32m, result.Protein);
        Assert.AreEqual(20m, result.Carbs);
        Assert.AreEqual(5.5m, result.Fat);
        productControllerMock.Verify(mock => mock.GetAllProducts(), Times.Once);
    }

    /// <summary>
    /// Calculates totals ignores unknown products.
    /// </summary>
    [TestMethod]
    public void CalculateTotals_IgnoresUnknownProducts()
    {
        // Arrange
        var productControllerMock = new Mock<IProductController>(MockBehavior.Strict);
        productControllerMock
            .Setup(controller => controller.GetAllProducts())
            .Returns([TestDataFactory.CreateProduct(1, "Apple", 50m, 1m, 10m, 0.5m)]);
        var controller = new MenuController(
            new RepositoryMockContext<Menu>().Mock.Object,
            new RepositoryMockContext<MenuProduct>().Mock.Object,
            productControllerMock.Object);

        // Act
        var result = controller.CalculateTotals(
        [
            TestDataFactory.CreateMenuProduct(1, 1, 999, 2m),
            TestDataFactory.CreateMenuProduct(2, 1, 1, 1m),
        ]);

        // Assert
        Assert.AreEqual(50m, result.Calories);
        Assert.AreEqual(1m, result.Protein);
        Assert.AreEqual(10m, result.Carbs);
        Assert.AreEqual(0.5m, result.Fat);
        productControllerMock.Verify(mock => mock.GetAllProducts(), Times.Once);
    }

    private MenuController CreateController(
        RepositoryMockContext<Menu>? menuRepositoryContext = null,
        RepositoryMockContext<MenuProduct>? menuProductRepositoryContext = null,
        Mock<IProductController>? productControllerMock = null)
    {
        return new MenuController(
            (menuRepositoryContext ?? new RepositoryMockContext<Menu>()).Mock.Object,
            (menuProductRepositoryContext ?? new RepositoryMockContext<MenuProduct>()).Mock.Object,
            (productControllerMock ?? new Mock<IProductController>(MockBehavior.Strict)).Object);
    }
}
