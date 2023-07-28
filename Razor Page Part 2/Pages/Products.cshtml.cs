using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Page_part_2.Models;
using Razor_Page_part_2.Services;

namespace Razor_Page_part_2.Pages;

public class ProductsModel : PageModel
{
    private readonly ProductService _productService;
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    public ProductsModel(ProductService productService)
    {
        _productService = productService;
    }

    public async Task OnGet()
    {
        Products = await _productService.GetProductsAsync();
    }
}