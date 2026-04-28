using ClassController.Abstractions;
using ClassModels;

namespace ClassControllerTest;

internal sealed class ProductControllerDouble : IProductController
{
    public List<Product> Products { get; } = new List<Product>();

    public List<Product> GetAllProducts()
    {
        return this.Products.ToList();
    }

    public List<Product> GetActiveProducts()
    {
        return this.Products.Where(product => product.IsActive).ToList();
    }

    public bool Register(Product product, User user)
    {
        throw new NotSupportedException();
    }

    public bool Update(Product product, User user)
    {
        throw new NotSupportedException();
    }

    public bool DeactivateProduct(int productId, User user)
    {
        throw new NotSupportedException();
    }

    public bool ActivateProduct(int productId, User user)
    {
        throw new NotSupportedException();
    }
}
