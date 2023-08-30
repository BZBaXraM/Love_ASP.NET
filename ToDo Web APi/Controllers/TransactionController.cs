using Microsoft.AspNetCore.Mvc;

namespace ToDo_Web_APi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ILogger _logger;

    public TransactionController(ILogger<TransactionController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreateTransactionAsync()
    {
        // await Task.Run(async () =>
        // {
        //     _logger.LogError("Transaction Begin");
        //     await Task.Delay(5555);
        //     _logger.LogError("Transaction End");
        // });
        return Ok();
    }
}