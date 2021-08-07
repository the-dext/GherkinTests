namespace CheckoutSimulator.Domain.Tests.Offers
{
    using AutoFixture;

    using CheckoutSimulator.Domain.Offers;

    using FluentAssertions;

    using Xunit;

    using static TestUtils.TestIdioms;

    public class SaleDiscountTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<SaleDiscount>();
        }


        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<SaleDiscount>();
        }

        [Fact]
        public void SaleDiscount_IsTypeOf_ISaleDiscount_And_IDiscount()
        {
            typeof(SaleDiscount).Should().BeAssignableTo<ISaleDiscount>();
            typeof(SaleDiscount).Should().BeAssignableTo<IDiscount>();
        }

        private class TestFixtureBuilder
        {
            public Fixture Fixture;


            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();

            }

            public SaleDiscount BuildSut()
            {
                return new SaleDiscount(this.Fixture.Create<string>());
            }
        }
    }
}
