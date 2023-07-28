using Bogus;
using Razor_Page_part_2.Models;

namespace Razor_Page_part_2.Services;

public class ProductService
{
    private readonly List<Product> _products = new();

    public ProductService()
    {
        var faker = new Faker<Product>()
            .RuleFor(x => x.Id, f => f.Random.Int(1))
            .RuleFor(x => x.Name, f => f.Commerce.Product())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
            .RuleFor(x => x.Count, f => f.Random.UInt(0))
            .RuleFor(x => x.Price, f => f.Random.Decimal(1));
        _products.AddRange(faker.GenerateBetween(30, 30));
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
        => Task.FromResult(_products.AsEnumerable());

    public Task<Product?> GetProductById(int id)
        => Task.FromResult(_products.FirstOrDefault(x => x.Id == id));

    public Task AddProductAsync(Product product)
    {
        var faker = new Faker<Product>().RuleFor(x => x.Id, f => f.Random.Int(1));
        product.Id = faker.Generate().Id;
        if (product.Count > 0) product.Available = true;
        _products.Add(product);

        return Task.CompletedTask;
    }
}