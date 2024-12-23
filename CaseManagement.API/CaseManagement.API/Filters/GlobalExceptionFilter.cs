using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CaseManagement.Business.Common;

namespace CaseManagement.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception details
            _logger.LogError(context.Exception, "An unexpected error occurred");

            var errorResponse = OperationResult.Failed(context.Exception.Message);

            // Set the result
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };

            // Mark the exception as handled
            context.ExceptionHandled = true;
        }
    }
}
