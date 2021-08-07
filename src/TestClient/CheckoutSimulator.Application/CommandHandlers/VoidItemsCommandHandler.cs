// Checkout Simulator by Chris Dexter, file="VoidItemsCommandHandler.cs"

namespace CheckoutSimulator.Application.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain;
    using MediatR;

    /// <summary>
    /// Defines the <see cref="VoidItemsCommandHandler"/>.
    /// </summary>
    public class VoidItemsCommandHandler : IRequestHandler<VoidItemsCommand, bool>
    {
        private readonly ITill till;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoidItemsCommandHandler"/> class.
        /// </summary>
        /// <param name="till">The till <see cref="ITill"/>.</param>
        public VoidItemsCommandHandler(ITill till)
        {
            this.till = Guard.Against.Null(till, nameof(till));
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="cmd">The cmd<see cref="VoidItemsCommand"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public Task<bool> Handle(VoidItemsCommand cmd, CancellationToken cancellationToken)
        {
            _ = Guard.Against.Null(cmd, nameof(cmd));

            this.till.VoidItems();

            return Task.FromResult(true);
        }
    }
}
