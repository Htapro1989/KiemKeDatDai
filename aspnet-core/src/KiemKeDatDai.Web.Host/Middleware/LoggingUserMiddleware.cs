#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class LoggingUserMiddleware

{
    private readonly RequestDelegate _next;

    public LoggingUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userName = context.User?.Identity?.Name ?? "Anonymous";
        log4net.LogicalThreadContext.Properties["username"] = userName;

        await _next(context);
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member