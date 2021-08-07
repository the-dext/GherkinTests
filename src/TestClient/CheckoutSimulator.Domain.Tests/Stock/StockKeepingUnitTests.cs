// Checkout Simulator by Chris Dexter, file="StockKeepingUnitTests.cs"

namespace CheckoutSimulator.Domain.Tests.Stock
{
    using AutoFixture;
    using CheckoutSimulator.Domain;
    using FluentAssertions;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="StockKeepingUnitTests" />.
    /// </summary>
    public class StockKeepingUnitTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<StockKeepingUnit>();
        }

        /// <summary>
        /// The Create_Sets_Properties.
        /// </summary>
        [Fact]
        public void Create_Sets_Properties()
        {
            // Arrange
            var expectedBarcode = "A12";
            var expectedUnitPrice = 0.30;
            var expectedDescription = "Apple";

            // Act
            var sut = StockKeepingUnit.Create(expectedBarcode, expectedUnitPrice, expectedDescription);

            // Assert
            sut.Barcode.Should().Be(expectedBarcode);
            sut.UnitPrice.Should().Be(expectedUnitPrice);
            sut.Description.Should().Be(expectedDescription);
        }

        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<StockKeepingUnit>();
        }

        /// <summary>
        /// The Create_StateUnderTest_ExpectedBehavior.
        /// </summary>
        [Fact]
        public void Should_Implement_IStockKeepingUnit()
        {
            typeof(StockKeepingUnit).Should().BeAssignableTo<IStockKeepingUnit>();
        }

        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="StockKeepingUnit"/>.</returns>
            public StockKeepingUnit BuildSut()
            {
                return StockKeepingUnit.Create(
                    this.Fixture.Create<string>(),
                    this.Fixture.Create<double>(),
                    this.Fixture.Create<string>()
                );
            }
        }
    }
}
