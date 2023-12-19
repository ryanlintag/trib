namespace Domain.Abstractions
{
    public record DomainError(string Code, string? Description = null)
    {
        public static readonly DomainError None = new(string.Empty);
        public static implicit operator DomainResult(DomainError error) => DomainResult.Failure(error);
    }
}
