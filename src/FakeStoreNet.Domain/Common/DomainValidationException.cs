namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Exception thrown when a domain invariant is violated.
    /// </summary>
    public class DomainValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainValidationException(string message)
            : base(message)
        {
        }
    }
}
