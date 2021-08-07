// Checkout Simulator by Chris Dexter, file="DiscountRepository.cs"

namespace CheckoutSimulator.Persistence
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Repositories;

    /// <summary>
    /// Defines the <see cref="DiscountRepository"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DiscountRepository : IDiscountRepository
    {
        /// <summary>
        /// The GetDiscountsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{IDiscount}}"/>.</returns>
        public Task<IEnumerable<IDiscount>> GetDiscountsAsync()
        {
            var discounts = new List<IDiscount>()
            {
                new BuyOneGetOneFree("Buy One-Get One Free on Eggs", "C40", 2, 0),
                new MultiBuy("2 for 45p on Biscuits", "B15", itemsRequired: 2, discountPrice: 0.15),
                new MultiBuy("3 Apples for £1.30 ", "A99", itemsRequired: 3,  discountPrice: .30d),
            };

            return Task.FromResult(discounts.AsEnumerable());
        }
    }
}
