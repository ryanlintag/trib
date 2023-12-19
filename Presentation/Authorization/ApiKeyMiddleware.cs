using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Presentation.Authorization
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (!context.Request.Headers.TryGetValue(ApiKeyConstants.AuthorizationHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing!");  
                return;
            }
            var apiKey = _configuration.GetSection(ApiKeyConstants.AuthorizationSection);
            if (!apiKey.Value.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key!");
                return;
            }
            await _next(context);
        }
    }
}
