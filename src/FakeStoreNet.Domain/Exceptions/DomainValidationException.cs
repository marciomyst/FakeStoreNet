namespace FakeStoreNet.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a domain invariant is violated.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DomainValidationException"/> class with a specified error message.
    /// </remarks>
    /// <param name="message">The message that describes the error.</param>
    public class DomainValidationException(string message) : Exception(message)
    {
    }
}
