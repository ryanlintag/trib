namespace Domain.Abstractions
{
    public class DomainResult
    {
        private DomainResult(bool isSuccess, DomainError error)
        {
            if ((isSuccess && error != DomainError.None) ||
                (!isSuccess && error == DomainError.None))
            {
                throw new ArgumentException("Invalid error!", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public DomainError Error { get; }

        public static DomainResult Success() => new(true, DomainError.None);
        public static DomainResult Failure(DomainError error) => new(false, error);
    }
}
