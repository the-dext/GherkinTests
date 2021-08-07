// Checkout Simulator by Chris Dexter, file="TillFactoryTests.cs"

namespace CheckoutSimulator.Domain.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Repositories;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="TillFactoryTests" />.
    /// </summary>
    public class TillFactoryTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<TillFactory>();
        }

        /// <summary>
        /// The CreateTillAsync_Calls_Repository.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        [Fact]
        public async Task CreateTillAsync_Calls_Repository()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture.BuildSut();

            // Act
            var result = await sut.CreateTillAsync();

            // Assert
            testFixture.MockStockRepository.Verify(x => x.GetStockItemsAsync(), Times.Once);
        }

        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<TillFactory>();
        }

        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Mock<IStockRepository> MockStockRepository;
            public Mock<IDiscountRepository> MockDiscountsRepository;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.MockStockRepository = new Mock<IStockRepository>();
                this.MockStockRepository.Setup(x => x.GetStockItemsAsync())
                    .Returns(Task.FromResult(Array.Empty<IStockKeepingUnit>().AsEnumerable()));

                this.MockDiscountsRepository = new Mock<IDiscountRepository>();
                this.MockDiscountsRepository.Setup(x => x.GetDiscountsAsync())
                    .Returns(Task.FromResult(Array.Empty<IDiscount>().AsEnumerable()));
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="TillFactory"/>.</returns>
            public TillFactory BuildSut()
            {
                return new TillFactory(this.MockStockRepository.Object, this.MockDiscountsRepository.Object);
            }
        }
    }
}
