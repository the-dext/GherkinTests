// Checkout Simulator by Chris Dexter, file="ItemDiscount.cs"
/// <summary>
/// This discount just proves the concept of injecting different item discount types into the offers.
/// </summary>
namespace CheckoutSimulator.Domain.Offers
{
    using System.Linq;
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="BuyOneGetOneFree"/>.
    /// </summary>
    public class BuyOneGetOneFree : IItemDiscount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyOneGetOneFree"/> class.
        /// </summary>
        /// <param name="description">The description <see cref="string"/>.</param>
        /// <param name="barcode">The barcode <see cref="string"/>.</param>
        public BuyOneGetOneFree(string description, string barcode, int itemsRequired, double discountPrice)
        {
            this.Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
            this.Barcode = Guard.Against.NullOrWhiteSpace(barcode, nameof(barcode));
            this.ItemsRequired = Guard.Against.NegativeOrZero(itemsRequired, nameof(itemsRequired));
            this.DiscountPrice = Guard.Against.Negative(discountPrice, nameof(discountPrice));
        }

        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        public string Barcode { get; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }
        public int ItemsRequired { get; }
        public double DiscountPrice { get; }

        /// <summary>
        /// The ApplyDiscount.
        /// </summary>
        /// <param name="itemBeingScanned">The scannedItem <see cref="IScannedItem"/>.</param>
        /// <param name="previouslyScannedItems">The previouslyScannedItems<see cref="IScannedItem[]"/>.</param>
        public void ApplyDiscount(IScannedItem itemBeingScanned, IScannedItem[] previouslyScannedItems)
        {
            _ = Guard.Against.Null(itemBeingScanned, nameof(itemBeingScanned));
            _ = Guard.Against.Null(previouslyScannedItems, nameof(previouslyScannedItems));

            if (itemBeingScanned.Barcode == this.Barcode)
            {
                // if there is one other item, that hasn't been included in a discount already then this discount can apply
                var previousApplicableItems = previouslyScannedItems.Where(x =>
                    x.Barcode == itemBeingScanned.Barcode && x.IsIncludedInADiscountOffer == false)
                .Take(this.ItemsRequired - 1);

                if (previousApplicableItems.Count() == this.ItemsRequired - 1)
                {
                    foreach (var item in previousApplicableItems)
                    {
                        item.SetIncludedInDiscountOffer(true);
                    }
                    itemBeingScanned.ApplyDiscount(this.Description, this.DiscountPrice);
                }
            }
        }
    }
}
