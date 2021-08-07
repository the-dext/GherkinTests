// Checkout Simulator by Chris Dexter, file="Till.cs"

namespace CheckoutSimulator.Domain
{
    using System.Collections.Generic;

    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="ITill" />.
    /// </summary>
    public interface ITill
    {
        /// <summary>
        /// The CompleteScanning.
        /// </summary>
        void CompleteScanning();

        /// <summary>
        /// The ListScannedItems.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{string}"/>.</returns>
        IEnumerable<string> ListScannedItems();

        /// <summary>
        /// The RequestTotalPrice.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        double RequestTotalPrice();

        /// <summary>
        /// The ScanItem.
        /// </summary>
        /// <param name="barcode">The barcode<see cref="string"/>.</param>
        /// <returns>The <see cref="IScanningResult"/>.</returns>
        IScanningResult ScanItem(string barcode);

        /// <summary>
        /// The VoidItems.
        /// </summary>
        void VoidItems();
    }
}
