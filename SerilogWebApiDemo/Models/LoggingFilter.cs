using Microsoft.AspNetCore.Mvc.Filters;

namespace SerilogWebApiDemo.Models
{
    public class LoggingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // BEFORE controller action
            Console.WriteLine("Before action");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // AFTER controller action
            Console.WriteLine("After action");
        }
    }
}
