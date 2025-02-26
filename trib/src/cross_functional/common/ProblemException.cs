namespace common
{
    /// <summary>
    /// Represents an exception that contains an error and a message.
    /// </summary>
    public class ProblemException : Exception
    {
        /// <summary>
        /// Gets the error associated with the exception.
        /// </summary>
        public string Error { get; } = string.Empty;

        /// <summary>
        /// Gets the message associated with the exception.
        /// </summary>
        public string Message { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemException"/> class with a specified error and message.
        /// </summary>
        /// <param name="error">The error associated with the exception.</param>
        /// <param name="message">The message associated with the exception.</param>
        public ProblemException(string error, string message) : base(message)
        {
            Error = error;
            Message = message;
        }
    }
}
