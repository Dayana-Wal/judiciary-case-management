using CaseManagement.Business.Common;

namespace CaseManagement.API.Common
{
    public class SendResponse
    {
        public static async Task ResponseWithError(HttpContext context, int statusCode, string message)
        {
            OperationResult opResult = OperationResult.Failed(message);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(opResult);
        }
    }
}
