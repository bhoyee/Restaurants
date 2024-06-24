
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWtch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopWtch.Stop();

            // chk if more than 4sec
            if (stopWtch.ElapsedMilliseconds / 1000 > 4) 
            { 
                logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopWtch.ElapsedMilliseconds);
            }
        }
    }
}
