using Serilog;
using System.Net;

namespace MoneyFellows.Products.Middlewares
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
            catch (Exception ex)
            {
                LogError(ex, context);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void LogError(Exception ex, HttpContext context)
        {
            Log.Error(ex, "Unhandled exception occurred");
            Log.Information("Request Path: {RequestPath}", context.Request.Path);
            Log.Warning("Request Query: {RequestQuery}", context.Request.QueryString);
            Log.Debug("Request Headers: {RequestHeaders}", context.Request.Headers);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new
            {
                error = "An unexpected error occurred.",
                message = ex.Message,
                exceptionType = ex.GetType().ToString()
            };

            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
