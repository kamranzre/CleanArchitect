using Serilog;

namespace Shop.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            Log.Information($"Starting Request:{httpContext.Request.Method} {httpContext.Request.Path}");
            try
            {
                await _next(httpContext);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "An unhandled exception occurred.");
                throw;
            }
            finally
            {
                Log.Information($"Finished request: {httpContext.Request.Method} {httpContext.Request.Path} with status code {httpContext.Response.StatusCode}");
            }
        }
    }
}
