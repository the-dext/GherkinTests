// Checkout Simulator by Chris Dexter, file="SaleDiscount.cs"
/// <summary>
/// This interface isn't really implemented, but is an example of how we would apply a discount
/// at the end of the sale, such as £5 off when you spend £50 or more.
/// </summary>

namespace CheckoutSimulator.Domain.Offers
{
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="SaleDiscount"/>.
    /// </summary>
    public class SaleDiscount : ISaleDiscount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleDiscount"/> class.
        /// </summary>
        /// <param name="description">The description <see cref="string"/>.</param>
        public SaleDiscount(string description)
        {
            this.Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The ApplyDiscount.
        /// </summary>
        /// <param name="totalSalePrice">The totalSalePrice<see cref="double"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public double ApplyDiscount(double totalSalePrice)
        {
            // Don't do anything
            return totalSalePrice;
        }
    }
}
