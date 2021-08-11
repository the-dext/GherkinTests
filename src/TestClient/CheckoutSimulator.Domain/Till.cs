// Checkout Simulator by Chris Dexter, file="Till.cs"

namespace CheckoutSimulator.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain.Exceptions;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="Till"/>.
    /// </summary>
    public class Till : ITill
    {
        private readonly IItemDiscount[] itemDiscounts;

        private readonly ISaleDiscount[] saleDiscounts;

        private readonly List<IScannedItem> scannedItems = new List<IScannedItem>();

        private readonly IStockKeepingUnit[] stockKeepingUnits;

        /// <summary>
        /// Initializes a new instance of the <see cref="Till"/> class.
        /// </summary>
        /// <param name="stockKeepingUnits">The stockKeepingUnits<see cref="IStockKeepingUnit[]"/>.</param>
        /// <param name="discounts">The discounts<see cref="IDiscount[]"/>.</param>
        public Till(IStockKeepingUnit[] stockKeepingUnits, IDiscount[] discounts)
        {
            this.ObjectId = Guid.NewGuid();
            System.Diagnostics.Debug.WriteLine($"Till created: {this.ObjectId}");

            this.stockKeepingUnits = Guard.Against.Null(stockKeepingUnits, nameof(stockKeepingUnits));
            _ = Guard.Against.Null(discounts, nameof(discounts));

            // Sort the discounts into the two supported types so that it's quicker to loop through later.
            this.itemDiscounts = discounts.Where(x => x is IItemDiscount).Cast<IItemDiscount>().ToArray();
            this.saleDiscounts = discounts.Where(x => x is ISaleDiscount).Cast<ISaleDiscount>().ToArray();
        }

        /// <summary>
        /// Gets the object identifier. Useful for debugging to check object references are different/equal....
        /// </summary>
        public Guid ObjectId { get; }

        /// <summary>
        /// The CompleteScanning.
        /// </summary>
        public void CompleteScanning()
        {
            this.scannedItems.Clear();
        }

        /// <summary>
        /// The ListScannedItems.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{IStockKeepingUnit}"/>.</returns>
        public IEnumerable<string> ListScannedItems()
        {
            return this.scannedItems.Select(x => x.Barcode);
        }

        /// <summary>
        /// The RequestTotalPrice.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public double RequestTotalPrice()
        {
            var result = this.scannedItems.Sum(x => x.IsDiscounted ? x.PriceAdjustment : x.UnitPrice);
            return Math.Round(result,2);
        }

        public Task<double> RequestTotalPriceAsync()
        {
            var result = this.scannedItems.Sum(x => x.IsDiscounted ? x.PriceAdjustment : x.UnitPrice);
            return Task.FromResult(Math.Round(result, 2));
        }

        /// <summary>
        /// The ScanItem.
        /// </summary>
        /// <param name="barcode">The barcode<see cref="string"/>.</param>
        /// <returns>The <see cref="IScanningResult"/>.</returns>
        public IScanningResult ScanItem(string barcode)
        {
            Guard.Against.Null(barcode, nameof(barcode));

            var sku = this.stockKeepingUnits.FirstOrDefault(x => x.Barcode.Equals(barcode))
                ?? throw new UnknownItemException($"Unrecognised barcode: {barcode}");

            var momento = new ScannedItemMomento(sku.Barcode, sku.UnitPrice);
            this.ApplyItemDiscounts(momento);

            this.scannedItems.Add(momento);
            return new ScanningResult(true, momento.Message);
        }

        public Task<IScanningResult> ScanItemAsync(string barcode)
        {
            Guard.Against.Null(barcode, nameof(barcode));

            Task<IScanningResult> Do()
            {

                var sku = this.stockKeepingUnits.FirstOrDefault(x => x.Barcode.Equals(barcode))
                    ?? throw new UnknownItemException($"Unrecognised barcode: {barcode}");

                var momento = new ScannedItemMomento(sku.Barcode, sku.UnitPrice);
                this.ApplyItemDiscounts(momento);

                this.scannedItems.Add(momento);
                var ret = new ScanningResult(true, momento.Message) as IScanningResult;
                return Task.FromResult(ret);
            }

            return Do();
        }

        /// <summary>
        /// The VoidItems.
        /// </summary>
        public void VoidItems()
        {
            this.scannedItems.Clear();
        }

        /// <summary>
        /// The ApplyItemDiscounts.
        /// </summary>
        /// <param name="momento">The momento<see cref="ScannedItemMomento"/>.</param>
        /// <returns>The <see cref="ScannedItemMomento"/>.</returns>
        private ScannedItemMomento ApplyItemDiscounts(ScannedItemMomento momento)
        {
            foreach (var itemDiscount in this.itemDiscounts)
            {
                itemDiscount.ApplyDiscount(momento, this.scannedItems.ToArray());
            }

            return momento;
        }
    }
}
