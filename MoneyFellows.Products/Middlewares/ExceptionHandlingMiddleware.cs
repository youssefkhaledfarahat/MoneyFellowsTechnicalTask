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

                Log.Information("Request {Method} {Path} from {RemoteIp} was handled successfully",
                    context.Request.Method,
                    context.Request.Path,
                    context.Connection.RemoteIpAddress?.ToString());
            }
            catch (Exception ex)
            {
                LogError(ex, context);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void LogError(Exception ex, HttpContext context)
        {
            if (ex is ArgumentException || ex is InvalidOperationException)
            {
                Log.Warning(ex, "A warning occurred processing the request.");
            }
            else if (ex is UnauthorizedAccessException)
            {
                Log.Information(ex, "An authorization issue occurred.");
            }
            else
            {
                Log.Error(ex, "Unhandled exception occurred.");
            }

            //Log.Debug("Request Path: {RequestPath}", context.Request.Path);
            //Log.Debug("Request Query: {RequestQuery}", context.Request.QueryString);
            //Log.Debug("Request Headers: {RequestHeaders}", context.Request.Headers);
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
