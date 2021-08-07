// Checkout Simulator by Chris Dexter, file="ISaleDiscount.cs"
/// <summary>
/// This interface isn't implemented by anything, but is an example of how we would apply a discount
/// at the end of the sale, such as £5 off when you spend £50 or more.
/// </summary>

namespace CheckoutSimulator.Domain.Offers
{
    /// <summary>
    /// Defines the <see cref="ISaleDiscount"/>. This type of discount is applied when a total sale
    /// is being completed.
    /// </summary>
    public interface ISaleDiscount : IDiscount
    {
        /// <summary>
        /// The Applies the discount.
        /// </summary>
        /// <param name="totalSalePrice">The totalSalePrice<see cref="double"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        double ApplyDiscount(double totalSalePrice);
    }
}
