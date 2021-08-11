// Checkout Simulator by Chris Dexter, file="TillTests.cs"

namespace CheckoutSimulator.Domain.Tests
{
    using AutoFixture;

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
    using static GherkinTests.Gherkin.GherkinScenario;
    using FluentAssertions.Execution;

    /// <summary>
    /// Defines the <see cref="TillTests" />.
    /// </summary>
    public class TillTestsGherkin
    {
        /// <summary>
        /// The Can_Scan_Item.
        /// </summary>
        [Fact]
        public void Can_Scan_Item()
        {
            string expectedBarcode = "B15";

            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Till>("Can scan an item"))
            {
                scenario.Ctor(() =>
                {
                    return new TestFixtureBuilder()
                        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                        .BuildSut();
                })
                .Given()
                .When((sut) => sut.ScanItem(expectedBarcode))
                .Then(sut => sut.ListScannedItems().Should().Contain(expectedBarcode));
            }
        }

        /// <summary>
        /// The Can_Scan_MultipleItems.
        /// </summary>
        [Fact]
        public void Can_Scan_MultipleItems()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Till>("Can scan multiple items"))
            {
                scenario
                .Ctor("Till with two SKUs", () => new TestFixtureBuilder()
                    .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                    .WithStockKeepingUnit("A12", 0.30, "Apple")
                    .BuildSut())
                .Given()
                .When("each item is scanned", sut =>
                {
                    List<string> expectedBarcodes = new List<string> { "B15", "A12", "B15", "B15" };
                    expectedBarcodes.ForEach(x => sut.ScanItem(x));
                })
                .Then("first item should be B15", (sut, because) =>
                {
                    sut.ListScannedItems().First().Should().Be("B15", because);
                })
                .And("last item should be A12", (sut, because) =>
                {
                    sut.ListScannedItems().First().Should().Be("B15", because);
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
            using (var scenario = Scenario<Till>("Can scan multiple items"))
            {
                double totalPrice = 0;

                scenario.Ctor("Till that can scan biscuits", () =>
                    new TestFixtureBuilder()
                        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                        .BuildSut())
                .Given(sut => sut.ScanItem("B15"))
                    .And(sut => sut.ScanItem("B15"))
                    .And(sut => sut.ScanItem("B15"))
                .When(sut =>
                {
                    totalPrice = sut.RequestTotalPrice();
                })
                .Then("Total price should be 3 times 45p", (sut, because) => totalPrice.Should().Be(0.45 * 3, because));
            }
        }

        /// <summary>
        /// The CompleteScanning_StateUnderTest_ExpectedBehavior.
        /// </summary>
        [Fact]
        public void CompleteScanning_Resets_ScannedItems()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Till>("Complete scanning resets scanned items"))
            {
                int originalItemCount = 0;

                scenario.Ctor(() => new TestFixtureBuilder()
                    .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                    .WithPreviouslyScannedItem("B15")
                    .WithPreviouslyScannedItem("B15")
                    .BuildSut())
                .Given("Scanning count is read before completing scanning", sut => originalItemCount = sut.ListScannedItems().Count())
                .When("Complete Scanning is called", sut => sut.CompleteScanning())
                .Then("Item count should be two before completing scanning", (sut, because) => originalItemCount.Should().Be(2, because))
                .And("Item count should be zero after completing scanning", (sut, because) => sut.ListScannedItems().Count().Should().Be(0, because));
            }
        }

        /// <summary>
        /// The ItemDiscount_CanBeApplied_DuringScanning.
        /// </summary>
        [Fact]
        public async Task ItemDiscount_CanBeApplied_DuringScanning()
        {
            const string Barcode = "B15";
            const double ExpectedPrice = 0.45;

            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Till>("Item discount can be applied during scanning"))
            {
                await scenario
                    .Ctor(() => new TestFixtureBuilder()
                        .WithStockKeepingUnit(Barcode, 0.30, "Biscuits")
                        .WithMockDiscount(Barcode)
                        .BuildSut())
                    .GivenAsync("an item on discount is scanned", async sut => await sut.ScanItemAsync(Barcode))
                    .WhenAsync("a second item is scanned that triggers discount", async (sut) => await Task.Run(() => sut.ScanItemAsync(Barcode)))
                    .ThenAsync("the price should include the discount", async (sut, because) => (await sut.RequestTotalPriceAsync()).Should().Be(ExpectedPrice, because))
                    .Go();
            }
        }

        /// <summary>
        /// The Scanning_Unknown_Item_Throws_Exception.
        /// </summary>
        [Fact]
        public void Scanning_Unknown_Item_Throws_Exception()
        {
            // Arrange
            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Till>("Scanning unknown item throws exception"))
            {
                string unexpectedBarcode = "A12";

                scenario.Ctor(() =>
                    new TestFixtureBuilder()
                    .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                    .BuildSut()
                )
                .Given()
                .When("when the scan item method is invoked", sut => { })
                .Then("an exception should be thrown", (sut, because) =>
                    new Action(() => sut.ScanItem(unexpectedBarcode))
                        .Should()
                        .Throw<UnknownItemException>()
                        .WithMessage("Unrecognised barcode: A12"));
            }
        }

        [Fact]
        public void Scanning_Unknown_Item_Throws_Exception_Alternative_Implementation()
        {
            // Arrange
            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Action>("Scanning unknown item throws exception"))
            {
                string unexpectedBarcode = "A12";

                scenario.Ctor(() => {
                    var till = new TestFixtureBuilder()
                        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                        .BuildSut();
                    return new Action(() => till.ScanItem(unexpectedBarcode));
                })
                .Given()
                .When("when the scan item method is invoked", sut => { })
                .Then("an exception should be thrown", (sutAction, because) =>
                    sutAction
                        .Should()
                        .Throw<UnknownItemException>()
                        .WithMessage("Unrecognised barcode: A12"));
            }
        }

        [Fact]
        public async Task Scanning_Unknown_Item_Throws_Exception_Alternative_Implementation_Async()
        {
            // Arrange
            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Action>("Scanning unknown item throws exception"))
            {
                string unexpectedBarcode = "A12";

                await scenario.Ctor(() => {
                    var till = new TestFixtureBuilder()
                        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                        .BuildSut();
                    return new Action(() => till.ScanItem(unexpectedBarcode));
                })
                .GivenAsync()
                .WhenAsync("scanning an item")
                .AndAsync("and the item has an unrecognised barcode")
                .ThenAsync("an 'Unrecognised barcode: A12' exception should be thrown", (sutAction, because) =>
                    sutAction
                        .Should()
                        .Throw<UnknownItemException>(because)
                        .WithMessage("Unrecognised barcode: A12", because))
                .Go();
            }
        }

        /// <summary>
        /// The Till_Implements_ITill.
        /// </summary>
        [Fact]
        public void Till_Implements_ITill()
        {
            using (var scope = new AssertionScope())
            using (var scenario = Scenario<Till>("Till should be assignable to ITill"))
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
