using Dependency_injection.Models;

namespace Dependency_injection.Data;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public IEnumerable<Product> GetProducts();
}