// Checkout Simulator by Chris Dexter, file="GetTotalPriceQuery.cs"

namespace CheckoutSimulator.Application.Queries
{
    using MediatR;

    /// <summary>
    /// Defines the <see cref="GetTotalPriceQuery"/>.
    /// </summary>
    public class GetTotalPriceQuery : IRequest<double>
    {
    }
}
