// Checkout Simulator by Chris Dexter, file="ScannedItemMomento.cs"

namespace CheckoutSimulator.Domain.Scanning
{
    /// <summary>
    /// Defines the <see cref="IScannedItem"/>.
    /// </summary>
    public interface IScannedItem
    {
        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        string Barcode { get; }

        /// <summary>
        /// Gets the IsDiscounted Gets a value indicating whether IsDiscounted....
        /// </summary>
        bool IsDiscounted { get; }

        bool IsIncludedInADiscountOffer { get; }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the PriceAdjustment.
        /// </summary>
        double PriceAdjustment { get; }

        /// <summary>
        /// Gets the UnitPrice.
        /// </summary>
        double UnitPrice { get; }

        /// <summary>
        /// Adjusts the price.
        /// </summary>
        /// <param name="reason">The reason<see cref="string"/>.</param>
        /// <param name="priceAdjustment">The price adjustment.</param>
        void ApplyDiscount(string reason, double priceAdjustment);

        void SetIncludedInDiscountOffer(bool value);
    }
}
