using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Page_part_2.Models;
using Razor_Page_part_2.Services;

namespace Razor_Page_part_2.Pages;

public class ProductModel : PageModel
{
    private readonly ProductService _productService;
    public Product? Product { get; set; }

    public ProductModel(ProductService productService)
    {
        _productService = productService;
    }

    public async Task OnGetAsync(int id)
    {
        Product = await _productService.GetProductById(id);
    }
}