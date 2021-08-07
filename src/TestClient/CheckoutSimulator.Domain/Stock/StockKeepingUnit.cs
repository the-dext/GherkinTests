// Checkout Simulator by Chris Dexter, file="StockItem.cs"

namespace CheckoutSimulator.Domain
{
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="StockKeepingUnit"/>.
    /// </summary>
    public class StockKeepingUnit : IStockKeepingUnit
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="StockKeepingUnit"/> class from being created.
        /// </summary>
        private StockKeepingUnit()
        {
        }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        public string Barcode { get; private set; }

        /// <summary>
        /// Gets the UnitPrice.
        /// </summary>
        public double UnitPrice { get; private set; }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="sku">The sku<see cref="string"/>.</param>
        /// <param name="unitPrice">The unitPrice<see cref="double"/>.</param>
        /// <param name="description">The description<see cref="string"/>.</param>
        /// <returns>The <see cref="StockKeepingUnit"/>.</returns>
        public static StockKeepingUnit Create(string sku, double unitPrice, string description)
        {
            return new StockKeepingUnit
            {
                Barcode = Guard.Against.NullOrWhiteSpace(sku, nameof(sku)),
                UnitPrice = Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice)),
                Description = Guard.Against.NullOrWhiteSpace(description, nameof(description))
            };
        }
    }
}
