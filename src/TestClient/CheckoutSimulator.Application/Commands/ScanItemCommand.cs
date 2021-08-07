// Checkout Simulator by Chris Dexter, file="ScanItemCommand.cs"

namespace CheckoutSimulator.Application.Commands
{
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain.Scanning;
    using MediatR;

    /// <summary>
    /// Defines the <see cref="ScanItemCommand"/>.
    /// </summary>
    public class ScanItemCommand : IRequest<IScanningResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanItemCommand"/> class.
        /// </summary>
        /// <param name="barcode">The barcode <see cref="string"/>.</param>
        public ScanItemCommand(string barcode)
        {
            this.Barcode = Guard.Against.NullOrWhiteSpace(barcode, nameof(barcode));
        }

        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        public string Barcode { get; }
    }
}
