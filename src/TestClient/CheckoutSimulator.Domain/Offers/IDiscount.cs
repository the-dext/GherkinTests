// Checkout Simulator by Chris Dexter, file="IDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    /// <summary>
    /// Defines the <see cref="IDiscount"/>. A base interface all discount types will implement.
    /// </summary>
    public interface IDiscount
    {
        /// <summary>
        /// Gets the Description.
        /// </summary>
        string Description { get; }
    }
}
