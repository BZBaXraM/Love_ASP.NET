using Microsoft.EntityFrameworkCore;
using ToDo_Web_APi.Data;

namespace ToDo_Web_APi.Hosted_Services;

public class DatabaseClearJob : IHostedService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _provider;

    public DatabaseClearJob(ILogger<DatabaseClearJob> logger, IServiceProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    private bool _run;

    private async Task RunAsync()
    {
        while (_run)
        {
            var scope = _provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
            await Task.Delay(TimeSpan.FromSeconds(5));
            _logger.LogError("Transaction Service is running…");
            _logger.LogCritical($"Todos count: {context.ToDoItems.Count()}");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _run = true;
        RunAsync();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _run = false;
        return Task.CompletedTask;
    }
}