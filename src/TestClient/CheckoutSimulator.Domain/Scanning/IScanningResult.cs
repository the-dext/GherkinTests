// Checkout Simulator by Chris Dexter, file="Till.cs"

namespace CheckoutSimulator.Domain.Scanning
{
    /// <summary>
    /// Defines the <see cref="IScanningResult"/>.
    /// </summary>
    public interface IScanningResult
    {
        /// <summary>
        /// Gets the Message.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the Success
        /// Gets a value indicating whether IsSuccess..
        /// </summary>
        bool Success { get; }
    }
}
