// Checkout Simulator by Chris Dexter, file="TillTests.cs"

namespace CheckoutSimulator.Domain.Tests
{
    using AutoFixture;
    using AutoFixture.Idioms;

    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Exceptions;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Scanning;

    using FluentAssertions;

    using Moq;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Xunit;

    using static TestUtils.TestIdioms;
    using static GherkinTests.AAA.AAAScenario;
    using FluentAssertions.Execution;

    /// <summary>
    /// Defines the <see cref="TillTests" />.
    /// </summary>
    public class TillTestsAAA
    {
        /// <summary>
        /// The Can_Scan_Item.
        /// </summary>
        [Fact]
        public void Can_Scan_Item()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Can scan an item"))
            {
                string expectedBarcode = "B15";
                Till sut = default;

                scenario
                .Arrange(() =>
                    {
                        sut = new TestFixtureBuilder()
                        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                        .BuildSut();
                    })
                .Act(() => sut.ScanItem(expectedBarcode))
                .Assert(() => sut.ListScannedItems().Should().Contain(expectedBarcode));
            }
        }

        /// <summary>
        /// The Can_Scan_MultipleItems.
        /// </summary>
        [Fact]
        public void Can_Scan_MultipleItems()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Can scan multiple items"))
            {
                Till sut = default;
                List<string> expectedBarcodes = default;

                scenario.Arrange(() =>
                {
                    expectedBarcodes = new List<string> { "B15", "A12", "B15", "B15" };

                    sut = new TestFixtureBuilder()
                    .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                    .WithStockKeepingUnit("A12", 0.30, "Apple")
                    .BuildSut();
                })
                .Act(() => expectedBarcodes.ForEach(x => sut.ScanItem(x)))
                .Assert(() => {
                    sut.ListScannedItems().First().Should().Be("B15", "first item should be B15");
                    sut.ListScannedItems().Last().Should().Be("B15", "last item should be A12");
                });
            }
        }

        /// <summary>
        /// The Can_Total_Scanned_Items.
        /// </summary>
        [Fact]
        public void Can_Total_Scanned_Items()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Can scan multiple items"))
            {
                double totalPriceResult = 0;
                Till sut = default;

                scenario
                    .Arrange(() =>
                    {
                        sut = new TestFixtureBuilder()
                            .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                            .BuildSut();
                        sut.ScanItem("B15");
                        sut.ScanItem("B15");
                        sut.ScanItem("B15");
                    })
                    .Act(() =>
                    {
                        totalPriceResult = sut.RequestTotalPrice();
                    })
                    .Assert(() =>
                    {
                        totalPriceResult.Should().Be(0.45 * 3, "Total price should be 3 times 45p");
                    });
            }
        }

        /// <summary>
        /// The ItemDiscount_CanBeApplied_DuringScanning.
        /// </summary>
        [Fact]
        public async Task ItemDiscount_CanBeApplied_DuringScanning()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Item discount can be applied during scanning"))
            {
                const string Barcode = "B15";
                const double ExpectedPrice = 0.45;
                Till sut = default;

                await scenario.ArrangeAsync(async () =>
                {
                    await Task.Run(() =>
                        sut = new TestFixtureBuilder()
                            .WithStockKeepingUnit(Barcode, 0.30, "Biscuits")
                            .WithMockDiscount(Barcode)
                            .BuildSut());
                    sut.ScanItem(Barcode);
                })
                .ActAsync(async () => await sut.ScanItemAsync(Barcode))
                .AssertAsync(async () => (await sut.RequestTotalPriceAsync()).Should().Be(ExpectedPrice, "the price should include the discount"))
                .Go();
            }
        }

        [Fact]
        public async Task ItemDiscount_CanBeApplied_DuringScanning_WithActions()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Item discount can be applied during scanning"))
            {
                const string Barcode = "B15";
                const double ExpectedPrice = 0.45;
                Till sut = default;

                await scenario.ArrangeAsync(() =>
                {
                    sut = new TestFixtureBuilder()
                        .WithStockKeepingUnit(Barcode, 0.30, "Biscuits")
                        .WithMockDiscount(Barcode)
                        .BuildSut();
                    sut.ScanItem(Barcode);
                })
                .ActAsync(() => sut.ScanItem(Barcode))
                .AssertAsync(() => sut.RequestTotalPrice().Should().Be(ExpectedPrice, "the price should include the discount"))
                .Go();
            }
        }

        [Fact]
        public async Task Scanning_Unknown_Item_Throws_Exception_Alternative_Implementation_Async()
        {
            // Arrange
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Scanning unknown item throws exception"))
            {
                string unexpectedBarcode = "A12";
                Action action = default;

                scenario.Arrange(() =>
                {
                    var till = new TestFixtureBuilder()
                        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                        .BuildSut();
                    action = new Action(() => till.ScanItem(unexpectedBarcode));
                })
                .Act(() => { })
                .Assert(() =>  action
                        .Should()
                        .Throw<UnknownItemException>()
                        .WithMessage("Unrecognised barcode: A12"));
            }
        }

        /// <summary>
        /// The Till_Implements_ITill.
        /// </summary>
        [Fact]
        public void Till_Implements_ITill()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario("Till should be assignable to ITill"))
            {
                typeof(Till).Should().BeAssignableTo<ITill>();
            }
        }

        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        public class TestFixtureBuilder
        {
            /// <summary>
            /// Defines the Fixture.
            /// </summary>
            public Fixture Fixture;

            /// <summary>
            /// Defines the StockKeepingUnits.
            /// </summary>
            public List<IStockKeepingUnit> StockKeepingUnits;

            /// <summary>
            /// Defines the Discounts.
            /// </summary>
            private readonly List<IDiscount> Discounts;

            /// <summary>
            /// Defines the postBuildActions.
            /// </summary>
            private readonly List<Action<Till>> postBuildActions;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.postBuildActions = new List<Action<Till>>();
                this.StockKeepingUnits = new List<IStockKeepingUnit>();
                this.Discounts = new List<IDiscount>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="Till"/>.</returns>
            public Till BuildSut()
            {
                Till ret = new Till(this.StockKeepingUnits.ToArray(), this.Discounts.ToArray());

                // apply post creation actions to set up test fixture state.
                foreach (Action<Till> action in this.postBuildActions)
                {
                    action(ret);
                }

                return ret;
            }

            /// <summary>
            /// The WithMockDiscount.
            /// </summary>
            /// <param name="barcode">The barcode<see cref="string"/>.</param>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithMockDiscount(string barcode)
            {
                Queue<Action<IScannedItem, IScannedItem[]>> callBacks = new Queue<Action<IScannedItem, IScannedItem[]>>();
                callBacks.Enqueue((scannedItem, previouslyScannedItems) => Debug.WriteLine("first callback, no discount"));
                callBacks.Enqueue((scannedItem, previouslyScannedItems) => scannedItem.ApplyDiscount("mock discount", 0.15));

                Mock<IItemDiscount> discount = new Mock<IItemDiscount>();
                discount.Setup(x => x.ApplyDiscount(It.Is<IScannedItem>(item => item.Barcode == barcode), It.IsAny<IScannedItem[]>()))
                    .Callback<IScannedItem, IScannedItem[]>((scannedItem, previouslyScannedItems) => callBacks.Dequeue()(scannedItem, previouslyScannedItems));

                this.Discounts.Add(discount.Object);

                return this;
            }

            /// <summary>
            /// The WithExistingScannedItem.
            /// </summary>
            /// <param name="barcode">The barcode<see cref="string"/>.</param>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithPreviouslyScannedItem(string barcode)
            {
                this.postBuildActions.Add((Till x) =>
                {
                    x.ScanItem(barcode);
                });
                return this;
            }

            /// <summary>
            /// The WithStockKeepingUnit.
            /// </summary>
            /// <param name="barcode">The barcode<see cref="string"/>.</param>
            /// <param name="price">The price<see cref="double"/>.</param>
            /// <param name="description">The description<see cref="string"/>.</param>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithStockKeepingUnit(string barcode, double price, string description)
            {
                IStockKeepingUnit sku = Mock.Of<IStockKeepingUnit>(x => x.Barcode == barcode
                    && x.UnitPrice == price
                    && x.Description == description);

                this.StockKeepingUnits.Add(sku);

                return this;
            }
        }
    }
}
