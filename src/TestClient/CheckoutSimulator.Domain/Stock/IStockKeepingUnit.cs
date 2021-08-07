// Checkout Simulator by Chris Dexter, file="IStockItem.cs"

namespace CheckoutSimulator.Domain
{
    /// <summary>
    /// Defines the <see cref="IStockKeepingUnit" />.
    /// </summary>
    public interface IStockKeepingUnit
    {
        /// <summary>
        /// Gets the Description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the SKU.
        /// </summary>
        string Barcode { get; }

        /// <summary>
        /// Gets the UnitPrice.
        /// </summary>
        double UnitPrice { get; }
    }
}
