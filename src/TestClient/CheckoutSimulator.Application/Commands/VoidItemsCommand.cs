// Checkout Simulator by Chris Dexter, file="VoidItemsCommand.cs"

namespace CheckoutSimulator.Application.Commands
{
    using MediatR;

    /// <summary>
    /// Defines the <see cref="VoidItemsCommand" />.
    /// </summary>
    public class VoidItemsCommand : IRequest<bool>
    {
    }
}
