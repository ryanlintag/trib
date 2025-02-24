using ErrorOr;

namespace Domain
{
    public static class ErrorOrExtensions
    {
        public static ErrorOr<T> UnexpectedError<T>(this ErrorOr<T> thisErrorOr, string errorCode, string message) =>  
            Error.Failure(code: $"UnexpectedError.{errorCode}", description: message);
    }
}
