// Checkout Simulator by Chris Dexter, file="VoidItemsCommandTests.cs"

namespace CheckoutSimulator.Application.Tests.Commands
{
    using CheckoutSimulator.Application.Commands;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="VoidItemsCommandTests" />.
    /// </summary>
    public class VoidItemsCommandTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<VoidItemsCommand>();
        }

        /// <summary>
        /// Methods the guard against null arguments.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<VoidItemsCommand>();
        }
    }
}
