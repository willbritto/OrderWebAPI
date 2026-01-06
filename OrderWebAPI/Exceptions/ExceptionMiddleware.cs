using System.Net;
using System.Text.Json;

namespace OrderWebAPI.Execptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;

        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
               
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";

            response.StatusCode = ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException =>(int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var errorResponse = new
            {
                statusCode = response.StatusCode,
                message = ex.Message,
                details = ex.InnerException?.Message
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(json);

        }
    }
}
