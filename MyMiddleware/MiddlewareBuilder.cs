namespace MyMiddleware;

public class MiddlewareBuilder
{
    private readonly Stack<Type> _middlewares = new();

    public void Use<T>() where T : IMiddleware
    {
        _middlewares.Push(typeof(T));
    }

    public HttpHandler Build()
    {
        HttpHandler handler = context => context.Response.Close();
        while (_middlewares.Count != 0)
        {
            var mw = _middlewares.Pop();
            IMiddleware? middleware = Activator.CreateInstance(mw) as IMiddleware;
            middleware!.Next = handler;
            handler = middleware.Handle;
        }

        return handler;
    }
}