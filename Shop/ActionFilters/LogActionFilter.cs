using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Application.ActionFilters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // لاگ قبل از اجرای اکشن
            Log.Information($"Starting action: {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // لاگ بعد از اجرای اکشن
            Log.Information($"Finished action: {context.ActionDescriptor.DisplayName}");
        }
    }
}
