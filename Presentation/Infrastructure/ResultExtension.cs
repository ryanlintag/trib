using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Presentation.Infrastructure
{
    public static class ResultExtension
    {
        public static IResult ToProblemDetails(this DomainResult result)
        {
            ArgumentNullException.ThrowIfNull(result, nameof(result));
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Unable to convert a success result to a problem");
            }
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                extensions: new Dictionary<string, object> {
                    { "errors", new[]{ result.Error } }
                });

        }
    }
}
