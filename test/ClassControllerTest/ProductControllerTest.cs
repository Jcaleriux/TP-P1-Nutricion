using ClassController;
using ClassModels;
using Moq;

namespace ClassControllerTest;

/// <summary>
/// Test class for <see cref="ProductController"/>, covering product retrieval, registration, updates, status changes,
/// and most-consumed product calculations.
/// </summary>
[TestClass]
public class ProductControllerTest
{
    /// <summary>
    /// Gets all products returns products ordered by identifier.
    /// </summary>
    [TestMethod]
    public void GetAllProducts_ReturnsProductsOrderedById()
    {
        // Arrange
        var controller = this.CreateController(
            products:
            [
                TestDataFactory.CreateProduct(3, "Yogurt"),
                TestDataFactory.CreateProduct(1, "Apple"),
                TestDataFactory.CreateProduct(2, "Bread"),
            ]);

        // Act
        var result = controller.GetAllProducts();

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result.Select(item => item.ProductId).ToArray());
    }

    /// <summary>
    /// Gets active products returns only active products.
    /// </summary>
    [TestMethod]
    public void GetActiveProducts_ReturnsOnlyActiveProducts()
    {
        // Arrange
        var controller = this.CreateController(
            products:
            [
                TestDataFactory.CreateProduct(1, "Apple", isActive: true),
                TestDataFactory.CreateProduct(2, "Bread", isActive: false),
                TestDataFactory.CreateProduct(3, "Milk", isActive: true),
            ]);

        // Act
        var result = controller.GetActiveProducts();

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 3 }, result.Select(item => item.ProductId).ToArray());
    }

    /// <summary>
    /// Registers with admin and unique name saves product with next identifier.
    /// </summary>
    [TestMethod]
    public void Register_WithAdminAndUniqueName_SavesProductWithNextId()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(5, "Apple"),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);
        var admin = TestDataFactory.CreateAdminUser();
        var product = TestDataFactory.CreateProduct(0, "Banana");

        // Act
        var result = controller.Register(product, admin);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(6, product.ProductId);
        Assert.AreEqual(2, repositoryContext.LastSavedSnapshot!.Count);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Once);
    }

    /// <summary>
    /// Registers with duplicate name returns false without saving.
    /// </summary>
    [TestMethod]
    public void Register_WithDuplicateName_ReturnsFalseWithoutSaving()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);

        // Act
        var result = controller.Register(TestDataFactory.CreateProduct(0, " apple "), TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsFalse(result);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Never);
    }

    /// <summary>
    /// Registers with non-admin user throws <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    [TestMethod]
    public void Register_WithNonAdminUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var controller = this.CreateController();

        // Act
        void action() => controller.Register(TestDataFactory.CreateProduct(0, "Banana"), TestDataFactory.CreateStandardUser());

        // Assert
        Assert.ThrowsException<UnauthorizedAccessException>(action);
    }

    /// <summary>
    /// Updates with existing product and unique name updates and saves.
    /// </summary>
    [TestMethod]
    public void Update_WithExistingProductAndUniqueName_UpdatesAndSaves()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
            TestDataFactory.CreateProduct(2, "Bread"),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);
        var updated = TestDataFactory.CreateProduct(2, "Whole Bread", 250m, 8m, 42m, 3m, "slice", false);

        // Act
        var result = controller.Update(updated, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsTrue(result);
        var savedProduct = repositoryContext.LastSavedSnapshot!.Single(item => item.ProductId == 2);
        Assert.AreEqual("Whole Bread", savedProduct.Name);
        Assert.AreEqual(250m, savedProduct.Calories);
        Assert.IsFalse(savedProduct.IsActive);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Once);
    }

    /// <summary>
    /// Updates with unknown product returns false.
    /// </summary>
    [TestMethod]
    public void Update_WithUnknownProduct_ReturnsFalse()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);

        // Act
        var result = controller.Update(TestDataFactory.CreateProduct(2, "Banana"), TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsFalse(result);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Never);
    }

    /// <summary>
    /// Updates with duplicate name returns false.
    /// </summary>
    [TestMethod]
    public void Update_WithDuplicateName_ReturnsFalse()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
            TestDataFactory.CreateProduct(2, "Banana"),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);

        // Act
        var result = controller.Update(TestDataFactory.CreateProduct(2, " apple "), TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsFalse(result);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Never);
    }

    /// <summary>
    /// Updates with non-admin user throws <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    [TestMethod]
    public void Update_WithNonAdminUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        // Act
        void action() => controller.Update(TestDataFactory.CreateProduct(1, "Updated Apple"), TestDataFactory.CreateStandardUser());

        // Assert
        Assert.ThrowsException<UnauthorizedAccessException>(action);
    }

    /// <summary>
    /// Deactivates product with existing product updates status and saves.
    /// </summary>
    [TestMethod]
    public void DeactivateProduct_WithExistingProduct_UpdatesStatusAndSaves()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple", isActive: true),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);

        // Act
        var result = controller.DeactivateProduct(1, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsTrue(result);
        Assert.IsFalse(repositoryContext.LastSavedSnapshot!.Single().IsActive);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Once);
    }

    /// <summary>
    /// Activates product with existing product updates status and saves.
    /// </summary>
    [TestMethod]
    public void ActivateProduct_WithExistingProduct_UpdatesStatusAndSaves()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple", isActive: false),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);

        // Act
        var result = controller.ActivateProduct(1, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsTrue(result);
        Assert.IsTrue(repositoryContext.LastSavedSnapshot!.Single().IsActive);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Once);
    }

    /// <summary>
    /// Sets product status with unknown product returns false.
    /// </summary>
    [TestMethod]
    public void SetProductStatus_WithUnknownProduct_ReturnsFalse()
    {
        // Arrange
        var repositoryContext = new RepositoryMockContext<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
        ]);
        var controller = this.CreateController(productRepositoryContext: repositoryContext);

        // Act
        var result = controller.DeactivateProduct(99, TestDataFactory.CreateAdminUser());

        // Assert
        Assert.IsFalse(result);
        repositoryContext.Mock.Verify(repository => repository.SaveData(It.IsAny<List<Product>>()), Times.Never);
    }

    /// <summary>
    /// Activates product with non-admin user throws <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    [TestMethod]
    public void ActivateProduct_WithNonAdminUser_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        // Act
        void action() => controller.ActivateProduct(1, TestDataFactory.CreateStandardUser());

        // Assert
        Assert.ThrowsException<UnauthorizedAccessException>(action);
    }

    /// <summary>
    /// Gets most consumed product without menus returns null.
    /// </summary>
    [TestMethod]
    public void GetMostConsumedProduct_WithoutMenus_ReturnsNull()
    {
        // Arrange
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        // Act
        var result = controller.GetMostConsumedProduct(new DateTime(2026, 4, 1), new DateTime(2026, 4, 30));

        // Assert
        Assert.IsNull(result);
    }

    /// <summary>
    /// Gets most consumed product without menu products returns null.
    /// </summary>
    [TestMethod]
    public void GetMostConsumedProduct_WithoutMenuProducts_ReturnsNull()
    {
        // Arrange
        var controller = this.CreateController(
            products: [TestDataFactory.CreateProduct(1, "Apple")],
            menus: [TestDataFactory.CreateMenu(10, 1, new DateTime(2026, 4, 10))]);

        // Act
        var result = controller.GetMostConsumedProduct(new DateTime(2026, 4, 1), new DateTime(2026, 4, 30));

        // Assert
        Assert.IsNull(result);
    }

    /// <summary>
    /// Gets most consumed product with consumed products returns product with highest quantity.
    /// </summary>
    [TestMethod]
    public void GetMostConsumedProduct_WithConsumedProducts_ReturnsProductWithHighestQuantity()
    {
        // Arrange
        var controller = this.CreateController(
            products:
            [
                TestDataFactory.CreateProduct(1, "Apple"),
                TestDataFactory.CreateProduct(2, "Banana"),
            ],
            menus:
            [
                TestDataFactory.CreateMenu(10, 1, new DateTime(2026, 4, 10)),
                TestDataFactory.CreateMenu(11, 1, new DateTime(2026, 4, 11)),
            ],
            menuProducts:
            [
                TestDataFactory.CreateMenuProduct(1, 10, 1, 1m),
                TestDataFactory.CreateMenuProduct(2, 10, 2, 3m),
                TestDataFactory.CreateMenuProduct(3, 11, 2, 2m),
            ]);

        // Act
        var result = controller.GetMostConsumedProduct(new DateTime(2026, 4, 1), new DateTime(2026, 4, 30));

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ProductId);
    }

    private ProductController CreateController(
        IEnumerable<Product>? products = null,
        IEnumerable<Menu>? menus = null,
        IEnumerable<MenuProduct>? menuProducts = null,
        RepositoryMockContext<Product>? productRepositoryContext = null,
        RepositoryMockContext<Menu>? menuRepositoryContext = null,
        RepositoryMockContext<MenuProduct>? menuProductRepositoryContext = null)
    {
        return new ProductController(
            (productRepositoryContext ?? new RepositoryMockContext<Product>(products)).Mock.Object,
            (menuRepositoryContext ?? new RepositoryMockContext<Menu>(menus)).Mock.Object,
            (menuProductRepositoryContext ?? new RepositoryMockContext<MenuProduct>(menuProducts)).Mock.Object);
    }
}
