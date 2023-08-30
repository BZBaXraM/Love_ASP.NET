using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace ToDo_Web_APi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IMemoryCache _cache;

    public TestController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpGet("test")]
    // [ResponseCache(Duration = 60)]
    public async Task<ActionResult> GetAsync()
    {
        if (!_cache.TryGetValue("Test", out var data))
        {
            return Ok(data);
        }

        await Task.Delay(3000);
        _cache.Set("Test", "It's works -> 200 OK",
            new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(3)
            });
        return Ok("It's works -> 200 OK");
        // await Task.Delay(3000);
        // Log.Information("It's works -> 200 OK");
        // // _logger.LogInformation("It's works -> 200 OK");
        // return Ok("It's Works!");
    }
}