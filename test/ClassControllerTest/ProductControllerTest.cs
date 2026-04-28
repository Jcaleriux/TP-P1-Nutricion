using ClassController;
using ClassModels;

namespace ClassControllerTest;

[TestClass]
public class ProductControllerTest
{
    [TestMethod]
    public void GetAllProducts_ReturnsProductsOrderedById()
    {
        var controller = this.CreateController(
            products:
            [
                TestDataFactory.CreateProduct(3, "Yogurt"),
                TestDataFactory.CreateProduct(1, "Apple"),
                TestDataFactory.CreateProduct(2, "Bread"),
            ]);

        var result = controller.GetAllProducts();

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result.Select(item => item.ProductId).ToArray());
    }

    [TestMethod]
    public void GetActiveProducts_ReturnsOnlyActiveProducts()
    {
        var controller = this.CreateController(
            products:
            [
                TestDataFactory.CreateProduct(1, "Apple", isActive: true),
                TestDataFactory.CreateProduct(2, "Bread", isActive: false),
                TestDataFactory.CreateProduct(3, "Milk", isActive: true),
            ]);

        var result = controller.GetActiveProducts();

        CollectionAssert.AreEqual(new[] { 1, 3 }, result.Select(item => item.ProductId).ToArray());
    }

    [TestMethod]
    public void Register_WithAdminAndUniqueName_SavesProductWithNextId()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(5, "Apple"),
        ]);
        var controller = this.CreateController(productRepository: repository);
        var admin = TestDataFactory.CreateAdminUser();
        var product = TestDataFactory.CreateProduct(0, "Banana");

        var result = controller.Register(product, admin);

        Assert.IsTrue(result);
        Assert.AreEqual(6, product.ProductId);
        Assert.AreEqual(1, repository.SaveCallCount);
        Assert.HasCount(2, repository.LastSavedSnapshot!);
    }

    [TestMethod]
    public void Register_WithDuplicateName_ReturnsFalseWithoutSaving()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
        ]);
        var controller = this.CreateController(productRepository: repository);

        var result = controller.Register(TestDataFactory.CreateProduct(0, " apple "), TestDataFactory.CreateAdminUser());

        Assert.IsFalse(result);
        Assert.AreEqual(0, repository.SaveCallCount);
    }

    [TestMethod]
    public void Register_WithNonAdminUser_ThrowsUnauthorizedAccessException()
    {
        var controller = this.CreateController();

        Assert.Throws<UnauthorizedAccessException>(
            () => controller.Register(TestDataFactory.CreateProduct(0, "Banana"), TestDataFactory.CreateStandardUser()));
    }

    [TestMethod]
    public void Update_WithExistingProductAndUniqueName_UpdatesAndSaves()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
            TestDataFactory.CreateProduct(2, "Bread"),
        ]);
        var controller = this.CreateController(productRepository: repository);
        var updated = TestDataFactory.CreateProduct(2, "Whole Bread", 250m, 8m, 42m, 3m, "slice", false);

        var result = controller.Update(updated, TestDataFactory.CreateAdminUser());

        Assert.IsTrue(result);
        var savedProduct = repository.LastSavedSnapshot!.Single(item => item.ProductId == 2);
        Assert.AreEqual("Whole Bread", savedProduct.Name);
        Assert.AreEqual(250m, savedProduct.Calories);
        Assert.IsFalse(savedProduct.IsActive);
    }

    [TestMethod]
    public void Update_WithUnknownProduct_ReturnsFalse()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
        ]);
        var controller = this.CreateController(productRepository: repository);

        var result = controller.Update(TestDataFactory.CreateProduct(2, "Banana"), TestDataFactory.CreateAdminUser());

        Assert.IsFalse(result);
        Assert.AreEqual(0, repository.SaveCallCount);
    }

    [TestMethod]
    public void Update_WithDuplicateName_ReturnsFalse()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple"),
            TestDataFactory.CreateProduct(2, "Banana"),
        ]);
        var controller = this.CreateController(productRepository: repository);

        var result = controller.Update(TestDataFactory.CreateProduct(2, " apple "), TestDataFactory.CreateAdminUser());

        Assert.IsFalse(result);
        Assert.AreEqual(0, repository.SaveCallCount);
    }

    [TestMethod]
    public void Update_WithNonAdminUser_ThrowsUnauthorizedAccessException()
    {
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        Assert.Throws<UnauthorizedAccessException>(
            () => controller.Update(TestDataFactory.CreateProduct(1, "Updated Apple"), TestDataFactory.CreateStandardUser()));
    }

    [TestMethod]
    public void DeactivateProduct_WithExistingProduct_UpdatesStatusAndSaves()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple", isActive: true),
        ]);
        var controller = this.CreateController(productRepository: repository);

        var result = controller.DeactivateProduct(1, TestDataFactory.CreateAdminUser());

        Assert.IsTrue(result);
        Assert.IsFalse(repository.LastSavedSnapshot!.Single().IsActive);
    }

    [TestMethod]
    public void ActivateProduct_WithExistingProduct_UpdatesStatusAndSaves()
    {
        var repository = new InMemoryRepository<Product>(
        [
            TestDataFactory.CreateProduct(1, "Apple", isActive: false),
        ]);
        var controller = this.CreateController(productRepository: repository);

        var result = controller.ActivateProduct(1, TestDataFactory.CreateAdminUser());

        Assert.IsTrue(result);
        Assert.IsTrue(repository.LastSavedSnapshot!.Single().IsActive);
    }

    [TestMethod]
    public void SetProductStatus_WithUnknownProduct_ReturnsFalse()
    {
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        var result = controller.DeactivateProduct(99, TestDataFactory.CreateAdminUser());

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ActivateProduct_WithNonAdminUser_ThrowsUnauthorizedAccessException()
    {
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        Assert.Throws<UnauthorizedAccessException>(
            () => controller.ActivateProduct(1, TestDataFactory.CreateStandardUser()));
    }

    [TestMethod]
    public void GetMostConsumedProduct_WithoutMenus_ReturnsNull()
    {
        var controller = this.CreateController(products: [TestDataFactory.CreateProduct(1, "Apple")]);

        var result = controller.GetMostConsumedProduct(new DateTime(2026, 4, 1), new DateTime(2026, 4, 30));

        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetMostConsumedProduct_WithoutMenuProducts_ReturnsNull()
    {
        var controller = this.CreateController(
            products: [TestDataFactory.CreateProduct(1, "Apple")],
            menus: [TestDataFactory.CreateMenu(10, 1, new DateTime(2026, 4, 10))]);

        var result = controller.GetMostConsumedProduct(new DateTime(2026, 4, 1), new DateTime(2026, 4, 30));

        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetMostConsumedProduct_WithConsumedProducts_ReturnsProductWithHighestQuantity()
    {
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

        var result = controller.GetMostConsumedProduct(new DateTime(2026, 4, 1), new DateTime(2026, 4, 30));

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ProductId);
    }

    private ProductController CreateController(
        IEnumerable<Product>? products = null,
        IEnumerable<Menu>? menus = null,
        IEnumerable<MenuProduct>? menuProducts = null,
        InMemoryRepository<Product>? productRepository = null,
        InMemoryRepository<Menu>? menuRepository = null,
        InMemoryRepository<MenuProduct>? menuProductRepository = null)
    {
        return new ProductController(
            productRepository ?? new InMemoryRepository<Product>(products),
            menuRepository ?? new InMemoryRepository<Menu>(menus),
            menuProductRepository ?? new InMemoryRepository<MenuProduct>(menuProducts));
    }
}
