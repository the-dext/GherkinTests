// Checkout Simulator by Chris Dexter, file="GetTotalPriceQueryHandler.cs"

namespace CheckoutSimulator.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain;
    using MediatR;

    /// <summary>
    /// Defines the <see cref="GetTotalPriceQueryHandler" />.
    /// </summary>
    public class GetTotalPriceQueryHandler : IRequestHandler<GetTotalPriceQuery, double>
    {
        private readonly ITill till;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTotalPriceQueryHandler"/> class.
        /// </summary>
        /// <param name="till">The till <see cref="ITill"/>.</param>
        public GetTotalPriceQueryHandler(ITill till)
        {
            this.till = Guard.Against.Null(till, nameof(till));
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="request">The request<see cref="GetTotalPriceQuery"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{double}"/>.</returns>
        public Task<double> Handle(GetTotalPriceQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            async Task<double> DoWork()
            {
                return this.till.RequestTotalPrice();
            }
            return DoWork();
        }
    }
}
