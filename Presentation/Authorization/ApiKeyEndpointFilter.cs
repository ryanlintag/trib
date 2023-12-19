using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Presentation.Authorization
{
    public class ApiKeyEndpointFilter : IEndpointFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyEndpointFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyConstants.AuthorizationHeaderName, out var extractedApiKey))
            {
                return new UnauthorizedHttpObjectResult("API Key is missing!");
            }
            var apiKey = _configuration.GetSection(ApiKeyConstants.AuthorizationSection);
            if (!apiKey.Value.Equals(extractedApiKey))
            {
                return new UnauthorizedHttpObjectResult("Invalid API Key!");
            }
            return await next(context);
        }
    }
}
