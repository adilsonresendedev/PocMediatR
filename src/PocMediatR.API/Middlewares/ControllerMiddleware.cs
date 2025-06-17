using PocMediatR.Common.Extensions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace PocMediatR.API.Middlewares
{
    public class ControllerMiddleware(ILogger<ControllerMiddleware> logger, RequestDelegate next)
    {
        private static readonly JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
                logger.LogError($"Error with description: {ex}");
            }
        }

        private static Task HandleException(HttpContext httpContext, Exception exception)
        {
            var httpResponse = httpContext.Response;
            httpResponse.StatusCode = exception.GetStatusCodeFromException();
            httpResponse.ContentType = "application/json";
            var responseError = exception.GenerateErrorResponse(httpContext);
            var responseBody = JsonSerializer.Serialize(responseError, options);
            return httpResponse.WriteAsJsonAsync(responseBody);
        }
    }
}
