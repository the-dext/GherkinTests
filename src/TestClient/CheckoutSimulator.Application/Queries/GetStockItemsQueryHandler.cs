// Checkout Simulator by Chris Dexter, file="GetStockItemsQueryHandler.cs"

namespace CheckoutSimulator.Application.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Ardalis.GuardClauses;

    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Repositories;

    using MediatR;

    /// <summary>
    /// Defines the <see cref="GetStockItemsQueryHandler" />.
    /// </summary>
    public class GetStockItemsQueryHandler : IRequestHandler<GetStockItemsQuery, IStockKeepingUnit[]>
    {
        private readonly IStockRepository stockRepository;

        public GetStockItemsQueryHandler(IStockRepository stockRepository)
        {
            this.stockRepository = Guard.Against.Null(stockRepository, nameof(stockRepository));
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="request">The request<see cref="GetStockItemsQuery"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{IStockItem[]}"/>.</returns>
        public Task<IStockKeepingUnit[]> Handle(GetStockItemsQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));

            async Task<IStockKeepingUnit[]> DoWork()
            {
                return (await stockRepository.GetStockItemsAsync().ConfigureAwait(false)).ToArray();
            }

            return DoWork();
        }
    }
}
