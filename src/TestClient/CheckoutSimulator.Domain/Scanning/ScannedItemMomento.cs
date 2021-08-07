// Checkout Simulator by Chris Dexter, file="ScannedItemMomento.cs"
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CheckoutSimulator.Domain.Tests")]
namespace CheckoutSimulator.Domain.Scanning
{
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="ScannedItemMomento" />.
    /// </summary>
    ///
    internal class ScannedItemMomento : IScannedItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScannedItemMomento"/> class.
        /// </summary>
        /// <param name="barcode">The barcode<see cref="string"/>.</param>
        /// <param name="unitPrice">The unitPrice<see cref="double"/>.</param>
        public ScannedItemMomento(string barcode, double unitPrice)
        {
            this.Barcode = barcode;
            this.UnitPrice = unitPrice;
        }

        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        public string Barcode { get; }

        /// <summary>
        /// Gets a value indicating whether IsDiscounted.
        /// </summary>
        public bool IsDiscounted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether IsIncludedInADiscountOffer.
        /// </summary>
        public bool IsIncludedInADiscountOffer { get; private set; }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the PriceAdjustment.
        /// </summary>
        public double PriceAdjustment { get; private set; }

        /// <summary>
        /// Gets the UnitPrice.
        /// </summary>
        public double UnitPrice { get; }

        /// <summary>
        /// The ApplyDiscount.
        /// </summary>
        /// <param name="reason">The reason<see cref="string"/>.</param>
        /// <param name="priceAdjustment">The priceAdjustment<see cref="double"/>.</param>
        public void ApplyDiscount(string reason, double priceAdjustment)
        {
            _ = Guard.Against.NullOrWhiteSpace(reason, nameof(reason));

            this.Message = reason;
            this.IsDiscounted = true;
            this.PriceAdjustment = priceAdjustment;
            this.IsIncludedInADiscountOffer = true;
        }

        /// <summary>
        /// The SetIncludedInDiscountOffer.
        /// </summary>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public void SetIncludedInDiscountOffer(bool value)
        {
            this.IsIncludedInADiscountOffer = value;
        }
    }
}
