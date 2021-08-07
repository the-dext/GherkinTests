// Checkout Simulator by Chris Dexter, file="IRunningTotalDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using System.Collections.Generic;
    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="IItemDiscount"/>. This type of discount is applied as a till is
    /// scanning items.
    /// </summary>
    public interface IItemDiscount : IDiscount
    {
        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        string Barcode { get; }

        /// <summary>
        /// Gets the DiscountPrice.
        /// </summary>
        double DiscountPrice { get; }

        /// <summary>
        /// Gets the ItemsRequired.
        /// </summary>
        int ItemsRequired { get; }

        /// <summary>
        /// The ApplyDiscount.
        /// </summary>
        /// <param name="itemBeingScanned">The scannedItem <see cref="IScannedItem"/>.</param>
        /// <param name="previouslyScannedItems">The previouslyScannedItems <see cref="List{IScannedItem}"/>.</param>
        void ApplyDiscount(IScannedItem itemBeingScanned, IScannedItem[] previouslyScannedItems);
    }
}
