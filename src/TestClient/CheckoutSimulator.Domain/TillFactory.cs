// Checkout Simulator by Chris Dexter, file="TillFactory.cs"

namespace CheckoutSimulator.Domain
{
    using System.Linq;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain.Repositories;

    /// <summary>
    /// Defines the <see cref="TillFactory" />.
    /// </summary>
    public class TillFactory
    {
        private readonly IStockRepository stockRepository;
        private readonly IDiscountRepository discountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TillFactory"/> class.
        /// </summary>
        /// <param name="stockRepository">The stockRepository<see cref="IStockRepository"/>.</param>
        public TillFactory(IStockRepository stockRepository, IDiscountRepository discountRepository)
        {
            this.stockRepository = Guard.Against.Null(stockRepository, nameof(stockRepository));
            this.discountRepository = Guard.Against.Null(discountRepository, nameof(discountRepository));
        }

        /// <summary>
        /// The CreateTillAsync.
        /// </summary>
        /// <returns>The <see cref="Task{Till}"/>.</returns>
        public async Task<Till> CreateTillAsync()
        {
            var stockItems = await this.stockRepository.GetStockItemsAsync().ConfigureAwait(false);
            var discounts = await this.discountRepository.GetDiscountsAsync().ConfigureAwait(false);
            return new Till(stockItems.ToArray(), discounts.ToArray());
        }
    }
}
