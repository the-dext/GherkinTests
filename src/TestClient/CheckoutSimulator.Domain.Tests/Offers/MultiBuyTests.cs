// Checkout Simulator by Chris Dexter, file="MultiBuyTests.cs"

namespace CheckoutSimulator.Domain.Tests.Offers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Scanning;
    using FluentAssertions;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="MultiBuyTests" />.
    /// </summary>
    public class MultiBuyTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<MultiBuy>();
        }


        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<MultiBuy>();
        }

        [Fact]
        public void Writable_Properties_Behave()
        {
            AssertWritablePropertiesBehaveAsExpected<MultiBuy>();
        }

        [Fact]
        public void Implements_IItemDiscount_And_IDiscount()
        {
            typeof(MultiBuy).Should().BeAssignableTo<IItemDiscount>();
            typeof(MultiBuy).Should().BeAssignableTo<IDiscount>();
        }

        [Fact]
        public void Should_Apply_Discount_When_Valid_Correct_Number_Of_Items_Scanned()
        {
            // Arrange
            const string Barcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(Barcode)
                .BuildSut("My Test Discount", Barcode, 2, 0);

            // Act
            var itemBeingScanned = new ScannedItemMomento(Barcode, 0.45);
            sut.ApplyDiscount(itemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeTrue();
            itemBeingScanned.IsDiscounted.Should().BeTrue();
        }

        [Fact]
        public void Should_Apply_Discount_Once_If_Additional_Items_Scanned_After_Discount()
        {
            // Arrange
            const string Barcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(Barcode)
                .BuildSut("My Test Discount", Barcode, 2, 0);

            var firstItemBeingScanned = new ScannedItemMomento(Barcode, 0.45);
            var secondItemBeingScanned = new ScannedItemMomento(Barcode, 0.45);

            // Act
            sut.ApplyDiscount(firstItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());
            sut.ApplyDiscount(secondItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeTrue();
            testFixture.PreviouslyScannedItems.First().IsDiscounted.Should().BeFalse();

            firstItemBeingScanned.IsDiscounted.Should().BeTrue();
            firstItemBeingScanned.IsIncludedInADiscountOffer.Should().BeTrue();

            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();
            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Apply_Discount_If_Two_Items_Differ()
        {
            // Arrange
            const string Barcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(Barcode)
                .BuildSut("My Test Discount", Barcode, 2, 0);

            var firstItemBeingScanned = new ScannedItemMomento("A99", 0.45);

            // Act
            sut.ApplyDiscount(firstItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeFalse();
            testFixture.PreviouslyScannedItems.First().IsDiscounted.Should().BeFalse();

            firstItemBeingScanned.IsDiscounted.Should().BeFalse();
            firstItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();
        }

        [Fact]
        public void Should_Apply_Discount_Second_Item_Is_Not_Consecutively_Scanned()
        {
            // Arrange
            const string DiscountedBarcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(DiscountedBarcode)
                .BuildSut("My Test Discount", DiscountedBarcode, 2, 0);

            var firstItemBeingScanned = new ScannedItemMomento("A99", 0.45);
            var secondItemBeingScanned = new ScannedItemMomento(DiscountedBarcode, 0.45);

            // Act
            sut.ApplyDiscount(firstItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());
            sut.ApplyDiscount(secondItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeTrue();
            testFixture.PreviouslyScannedItems.First().IsDiscounted.Should().BeFalse();

            firstItemBeingScanned.IsDiscounted.Should().BeFalse();
            firstItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();

            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeTrue();
            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeTrue();
        }

        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public List<IScannedItem> PreviouslyScannedItems { get; }

            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.PreviouslyScannedItems = new List<IScannedItem>();
            }

            public MultiBuy BuildSut(string message, string barcode, int itemsRequired, double discountPrice)
            {
                return new MultiBuy(message, barcode, itemsRequired, discountPrice);
            }

            public TestFixtureBuilder WithPreviouslyScannedItem(string barcode)
            {
                // Real object, not a mock. Because we are testing the domain layer so testing using state instead of behaviour is more helpful.
                this.PreviouslyScannedItems.Add(new ScannedItemMomento(barcode, .45));
                return this;
            }
        }
    }
}
