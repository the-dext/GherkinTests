// Checkout Simulator by Chris Dexter, file="VoidItemsCommandHandlerTests.cs"

namespace CheckoutSimulator.Application.Tests.CommandHandlers
{
    using AutoFixture;

    using CheckoutSimulator.Application.CommandHandlers;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain;

    using Moq;

    using Xunit;

    using static TestUtils.TestIdioms;
    using static GherkinTests.Gherkin.GherkinScenario;

    /// <summary>
    /// Defines the <see cref="VoidItemsCommandHandlerTests" />.
    /// </summary>
    public class VoidItemsCommandHandlerTests_GTW
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<VoidItemsCommandHandler>();
        }

        /// <summary>
        /// The Handle_Calls_Till_ScanItem.
        /// </summary>
        [Fact]
        public void Handle_Calls_Till_ScanItem()
        {
            TestFixtureBuilder testFixtureBuilder = new TestFixtureBuilder();
            using (var scenario = Scenario<VoidItemsCommandHandler>("Handle calls Till Scan Item"))
            {
                scenario.Ctor(() => testFixtureBuilder.BuildSut())
                .When(sut => sut.Handle(new VoidItemsCommand(), default))
                .Then(sut => testFixtureBuilder.MockTill.Verify(x => x.VoidItems()));
            }
        }

        /// <summary>
        /// Methods the guard against null arguments.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<VoidItemsCommandHandler>();
        }

        /// Defines the <see cref="TestFixture"/>.
        /// </summary>
        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public Mock<ITill> MockTill;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.MockTill = this.Fixture.Freeze<Mock<ITill>>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="ScanItemCommandHandler"/>.</returns>
            public VoidItemsCommandHandler BuildSut()
            {
                return new VoidItemsCommandHandler(this.MockTill.Object);
            }
        }
    }
}
