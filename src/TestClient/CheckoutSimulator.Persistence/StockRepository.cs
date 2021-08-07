// Checkout Simulator by Chris Dexter, file="StockRepository.cs"

namespace CheckoutSimulator.Persistence
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Repositories;

    /// <summary>
    /// Defines the <see cref="StockRepository"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StockRepository : IStockRepository
    {
        /// <summary>
        /// Gets stock items. Really these would be from a DB, but for now they're hard coded.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IStockItem}" />.
        /// </returns>
        public Task<IEnumerable<IStockKeepingUnit>> GetStockItemsAsync()
        {
            return Task.FromResult(new IStockKeepingUnit[]
            {
                StockKeepingUnit.Create("A99", 0.50, "Apple"),
                StockKeepingUnit.Create("B15", 0.30, "Biscuits"),
                StockKeepingUnit.Create("C40", 0.60, "Eggs"),
            }
            .AsEnumerable());
        }
    }
}
