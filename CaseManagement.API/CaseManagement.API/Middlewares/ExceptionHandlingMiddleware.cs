using CaseManagement.API.Common;

namespace CaseManagement.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) {
                var messageParts = ex.Message.Split(':').Skip(1);
                var message = messageParts.Any() ? String.Join(':',messageParts) : $"An error occurred: {ex.Message}";
                await SendResponse.ResponseWithError(context, 500,message);
            }
        }
    }
}
