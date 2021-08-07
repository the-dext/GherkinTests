// Checkout Simulator by Chris Dexter, file="ScanItemCommandTests.cs"

namespace CheckoutSimulator.Application.Tests.Commands
{
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain.Scanning;

    using FluentAssertions;

    using MediatR;

    using Xunit;

    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="ScanItemCommandTests" />.
    /// </summary>
    public class ScanItemCommandTests_NoRefactorNeeded_Done
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<ScanItemCommand>();
        }

        /// <summary>
        /// Methods the guard against null arguments.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<ScanItemCommand>();
        }

        /// <summary>
        /// The ScanItemCommand_Is_Typeof_Request.
        /// </summary>
        [Fact]
        public void ScanItemCommand_Is_Typeof_Request()
        {
            typeof(ScanItemCommand).Should().BeAssignableTo<IRequest<IScanningResult>>();
        }

        /// <summary>
        /// Writable the properties behave as expected.
        /// </summary>
        [Fact]
        public void WritableProperties_Behave()
        {
            AssertWritablePropertiesBehaveAsExpected<ScanItemCommand>();
        }
    }
}
