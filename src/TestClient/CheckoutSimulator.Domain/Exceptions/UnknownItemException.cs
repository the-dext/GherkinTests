// Checkout Simulator by Chris Dexter, file="UnknownItemException.cs"

namespace CheckoutSimulator.Domain.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the <see cref="UnknownItemException" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UnknownItemException : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownItemException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public UnknownItemException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownItemException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public UnknownItemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
