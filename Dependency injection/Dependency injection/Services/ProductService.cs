using Dependency_injection.Data;
using Dependency_injection.Models;

namespace Dependency_injection.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Product AddProduct(Product product) => _repository.AddProduct(product);

    public IEnumerable<Product> GetProducts() => _repository.GetProducts();
}