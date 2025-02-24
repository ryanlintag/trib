using ErrorOr;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Application
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>>
        where TRequest : IRequest<ErrorOr<TResponse>>
        where TResponse : class
    {
        protected ILogger _logger { get; set; }
        protected BaseRequestHandler(ILogger logger)
        {
            _logger = logger;
        }
        public abstract Task<ErrorOr<TResponse>> HandleRequest(TRequest request, CancellationToken cancellationToken);
        public async ValueTask<ErrorOr<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleRequest(request, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing {Request} and {Response}",
                    typeof(TRequest).FullName,
                    typeof(ErrorOr<TResponse>).FullName);
                return ErrorOr<TResponse>.From([ApplicationError.UnhandledException(ex.Message)]);
            }
        }
    }
    public static class ApplicationError
    {
        public static Error UnhandledException(string exceptionMessage) => new Error()
        {
           
        };
    }

}
