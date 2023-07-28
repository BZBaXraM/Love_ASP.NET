using System.Net;

await new WebHost(27001).Run();

class WebHost
{
    private const string Path = @"/Users/baxram/WebstormProjects/First/HW/JS_HW/Final Project/";
    private readonly int _port;
    private HttpListener? _listener;

    public WebHost(int port)
    {
        _port = port;
    }

    public async Task Run()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://localhost:{_port}/");
        _listener.Start();
        Console.WriteLine($"Http server started on {_port}.");
        while (true)
        {
            var context = await _listener.GetContextAsync();
            await Task.Run(() => HandleRequest(context));
        }
    }

    private static async Task HandleRequest(HttpListenerContext context)
    {
        var url = context.Request.RawUrl;
        var path = $@"{Path}{url?.Split('/').Last()}";

        var response = context.Response;
        await using StreamWriter streamWriter = new(response.OutputStream);
        try
        {
            var src = await File.ReadAllTextAsync(path);
            await streamWriter.WriteAsync(src);
        }
        catch (Exception e)
        {
            await streamWriter.WriteAsync(e.Message);
        }

        streamWriter.Close();
    }
}