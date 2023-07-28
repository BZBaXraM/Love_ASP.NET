using System.Net;

namespace MyMiddleware;

public class StaticFilesMiddleware : IMiddleware
{
    public HttpHandler? Next { get; set; }

    public void Handle(HttpListenerContext context)
    {
        if (Path.HasExtension(context.Request.RawUrl))
        {
            try
            {
                var fileName = context.Request.RawUrl.Substring(1);
                var path = $@"/Users/baxram/RiderProjects/Love_ASP.NET/MyMiddleware/wwwroot/{fileName}";
                var bytes = File.ReadAllBytes(path);
                if (Path.GetExtension(path) == "html")
                {
                    context.Response.AddHeader("Context-Type", "text/html");
                }
                else if (Path.GetExtension(path) == "html")
                {
                    context.Response.AddHeader("Context-Type", "image/png");
                }

                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = $"File not found {e.Message}";
            }
        }
        else
        {
            Next?.Invoke(context);
        }

        context.Response.Close();
    }
}