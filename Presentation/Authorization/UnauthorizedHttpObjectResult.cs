using Microsoft.AspNetCore.Http;

namespace Presentation.Authorization
{
    public class UnauthorizedHttpObjectResult : IResult, IStatusCodeHttpResult
    {
        private readonly object _body;
        public UnauthorizedHttpObjectResult(object body)
        {
            this._body = body;
        }
        public int StatusCode => StatusCodes.Status401Unauthorized;

        int? IStatusCodeHttpResult.StatusCode => StatusCode;

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            httpContext.Response.StatusCode = this.StatusCode;
            if(this._body is string s)
            {
                await httpContext.Response.WriteAsync(s);
                return;
            }

            await httpContext.Response.WriteAsJsonAsync(this._body);
        }
    }
}
