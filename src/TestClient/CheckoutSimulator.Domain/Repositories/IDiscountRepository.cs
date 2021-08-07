// Checkout Simulator by Chris Dexter, file="IDiscountRepository.cs"

namespace CheckoutSimulator.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CheckoutSimulator.Domain.Offers;

    /// <summary>
    /// Defines the <see cref="IDiscountRepository" />.
    /// </summary>
    public interface IDiscountRepository
    {
        /// <summary>
        /// The GetDiscountsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{IDiscount}}"/>.</returns>
        Task<IEnumerable<IDiscount>> GetDiscountsAsync();
    }
}
