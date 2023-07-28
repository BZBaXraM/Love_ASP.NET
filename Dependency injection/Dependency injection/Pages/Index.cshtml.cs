using Dependency_injection.Models;
using Dependency_injection.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dependency_injection.Pages;

public class IndexModel : PageModel
{
    private readonly ProductService _service;

    public IndexModel(ProductService service)
    {
        _service = service;
    }

    public void OnGet()
    {
        var products = _service.GetProducts();
        ViewData["Products"] = products;
    }

    public void OnPost(Product product)
    {
        _service.AddProduct(product);
        // var products = _service.GetProducts();
        // ViewData["products"] = products;
        OnGet();
    }
}