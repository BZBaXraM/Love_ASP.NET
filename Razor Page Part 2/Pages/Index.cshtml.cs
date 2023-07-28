using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor_Page_part_2.Models;
using Razor_Page_part_2.Services;
using System.Threading.Tasks;

namespace Razor_Page_part_2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductService _service;

        public IndexModel(ILogger<IndexModel> logger, ProductService service)
        {
            _logger = logger;
            _service = service;

            Product = new Product();
        }

        [BindProperty]
        public Product Product { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var name = Product.Name;
            var description = Product.Description;
            var count = Product.Count;
            var price = Product.Price;

            var newProduct = new Product
            {
                Name = name,
                Description = description,
                Count = count,
                Price = price,
                Available = true
            };

            await _service.AddProductAsync(newProduct);

            return Page();
        }
    }
}