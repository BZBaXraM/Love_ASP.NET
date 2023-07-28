using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Intro.Data;
using MVC_Intro.Models;

namespace MVC_Intro.Controllers;

public class ProductsController : Controller
{
    private readonly MvcAppContext _context;

    public ProductsController(MvcAppContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return _context?.Products != null ? View(await _context.Products.ToListAsync()) : Problem("Product not found!");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null || _context.Products is null) return NotFound();
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == x.Id!);
        if (product is null) return NotFound();
        return View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null || _context.Products is null) return NotFound();
        var product = await _context.Products.FindAsync(id);
        if (product is null) return NotFound();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, Product product)
    {
        if (id != product.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task<IActionResult> Delete(int? id, Product product)
    {
        if (id != product.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    
        return View(product);
    }
}