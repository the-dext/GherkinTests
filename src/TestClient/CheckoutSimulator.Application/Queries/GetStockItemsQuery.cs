// Checkout Simulator by Chris Dexter, file="GetStockItems.cs"

namespace CheckoutSimulator.Application.Queries
{
    using CheckoutSimulator.Domain;
    using MediatR;

    /// <summary>
    /// Defines the <see cref="GetStockItemsQuery"/>.
    /// </summary>
    public class GetStockItemsQuery : IRequest<IStockKeepingUnit[]>
    {
    }
}
