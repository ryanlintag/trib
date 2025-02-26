using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace common.web
{
    /// <summary>
    /// Handles exceptions of type <see cref="ProblemException"/> and generates problem details.
    /// </summary>
    public class ProblemExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemExceptionHandler"/> class with a specified problem details service.
        /// </summary>
        /// <param name="problemDetailsService">The service used to generate problem details.</param>
        public ProblemExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            _problemDetailsService = problemDetailsService;
        }

        /// <summary>
        /// Tries to handle the specified exception and generate problem details if it is a <see cref="ProblemException"/>.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the exception was handled.</returns>
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ProblemException problemException)
            {
                return true;
            }

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = problemException.Error,
                Detail = problemException.Message,
                Type = "Bad Request"
            };
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext { HttpContext = httpContext, ProblemDetails = problemDetails });
        }
    }
}
