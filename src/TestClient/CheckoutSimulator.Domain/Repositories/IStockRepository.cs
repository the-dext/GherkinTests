// Checkout Simulator by Chris Dexter, file="IStockRepository.cs"

namespace CheckoutSimulator.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IStockRepository"/>.
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// The GetStockItems.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{IStockItem}"/>.</returns>
        Task<IEnumerable<IStockKeepingUnit>> GetStockItemsAsync();
    }
}
