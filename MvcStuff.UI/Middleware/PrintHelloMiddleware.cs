
namespace MvcStuff.Middleware;
public class PrintHelloMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Console.WriteLine("Hello From PrintHello Middleware on the way to the end!");

        await next(context);

        Console.WriteLine("Hello From PrintHello Middleware on the way back!");
    }
}
