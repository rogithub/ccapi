using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Api.Filters
{

    public class ErrorHandlerAttribute : IExceptionFilter, IAsyncExceptionFilter
    {
        private ILogger Logger { get; }
        public ErrorHandlerAttribute(ILogger logger)
        {
            Logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Logger.Error(context.Exception, "Uncaught async exception");
            }

            return Task.CompletedTask;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Logger.Error(context.Exception, "Uncaught exception");
            }
        }
    }
}