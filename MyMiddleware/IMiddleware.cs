using System.Net;

namespace MyMiddleware;

public delegate void HttpHandler(HttpListenerContext context);

public interface IMiddleware
{
    public HttpHandler Next { get; set; }
    public void Handle(HttpListenerContext context);
}