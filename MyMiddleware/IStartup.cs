namespace MyMiddleware;

public interface IStartup
{
    public void Configure(MiddlewareBuilder builder);
}