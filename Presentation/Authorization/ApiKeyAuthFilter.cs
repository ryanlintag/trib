using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Presentation.Authorization
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyConstants.AuthorizationHeaderName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key is missing!");
                return;
            }
            var apiKey = _configuration.GetSection(ApiKeyConstants.AuthorizationSection);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key!");
                return;
            }
        }
    }
}
