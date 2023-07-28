using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Page_Intro.Data;

namespace Razor_Page_Intro.Pages;

public class IndexModel : PageModel
{
    // private readonly ILogger<IndexModel> _logger;

    // public IndexModel(ILogger<IndexModel> logger)
    // {
    //     _logger = logger;
    // }

    // public string Foo(string name)
    // {
    //     return $"{name} is foo";
    // }

    public long Factorial(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;
        return n * Factorial(n - 1);
    }

    public long Fib(int n)
    {
        if (n <= 0) return 0;
        if (n == 1) return 1;
        return Fib(n - 1) + Fib(n - 2);
    }

    public string FizzBuzz(params int[] arr)
    {
        foreach (var i in arr)
        {
            return i.ToString();
        }

        return string.Empty;
    }

    // public void OnGet(string name, int age)
    // {
    //     ViewData["Name"] = name;
    //     ViewData["Age"] = age;
    // }

    // public void OnGet(Person person)
    // {
    //     ViewData["Name"] = person.FirstName;
    //     ViewData["Age"] = person.Age;
    // }

    // public void OnGet(string[] names)
    // {
    //     StringBuilder messageBuilder = new();
    //     foreach (var item in names)
    //     {
    //         messageBuilder.Append($"{item} ");
    //     }
    //
    //     ViewData["Name"] = messageBuilder.ToString();
    //     ViewData["Age"] = 0;
    // }

    // public ContentResult OnGet()
    // {
    //     return Content("My content");
    // }

    /*private readonly IWebHostEnvironment _webHost;

    public IndexModel(IWebHostEnvironment webHost)
    {
        _webHost = webHost;
    }

    public FileResult OnGet()
    {
        var path = Path.Combine(_webHost.ContentRootPath, "Data/text.txt");
        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes, "Application/txt", "temp.txt");
    }*/
}