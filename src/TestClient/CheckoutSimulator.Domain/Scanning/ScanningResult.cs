// Checkout Simulator by Chris Dexter, file="ScanningResult.cs"

namespace CheckoutSimulator.Domain.Scanning
{
    /// <summary>
    /// Defines the <see cref="ScanningResult"/>.
    /// </summary>
    public class ScanningResult : IScanningResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanningResult"/> class.
        /// </summary>
        /// <param name="success">The success<see cref="bool"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ScanningResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets a value indicating whether Success.
        /// </summary>
        public bool Success { get; }
    }
}
