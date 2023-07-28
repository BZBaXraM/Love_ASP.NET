using Dependency_injection.Models;

namespace Dependency_injection.Data;

public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<Guid, Product> _products
        = new Dictionary<Guid, Product>();

    public InMemoryProductRepository(bool data)
    {
        if (data)
        {
            AddProduct(new Product
            {
                Name = "Coca Cola",
                Description = "Сделано из жуков"
            });
            AddProduct(new Product
            {
                Name = "Nestle Nescafe 3 in 1",
                Description = "Покой для Бахрама…"
            });
            AddProduct(new Product
            {
                Name = "Efe's Zero",
                Description = "Кайф для Бахрама…"
            });
        }
    }

    public Product AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        _products.Add(product.Id, product);
        return product;
    }

    public IEnumerable<Product> GetProducts() => _products.Values;
}