// Checkout Simulator by Chris Dexter, file="GetStockItemsQueryHandlerTests.cs"

namespace CheckoutSimulator.Application.Tests.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoFixture;
    using CheckoutSimulator.Application.Queries;
    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Repositories;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="GetStockItemsQueryHandlerTests" />.
    /// </summary>
    public class GetStockItemsQueryHandlerTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<GetStockItemsQueryHandler>();
        }

        /// <summary>
        /// The Handle_Calls_Repository.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_Calls_Repository()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture.BuildSut();

            var cancellationToken = default(CancellationToken);

            // Act
            var result = await sut.Handle(new GetStockItemsQuery(), cancellationToken);

            // Assert
            testFixture.MockStockRepository.Verify(x => x.GetStockItemsAsync(), Times.Once);
        }

        /// <summary>
        /// The Is_Assignable_To_IRequestHandler.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        [Fact]
        public async Task Is_Assignable_To_IRequestHandler()
        {
            typeof(GetStockItemsQueryHandler).Should().BeAssignableTo<IRequestHandler<GetStockItemsQuery, IStockKeepingUnit[]>>();
        }

        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<GetStockItemsQueryHandler>();
        }

        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public Mock<IStockRepository> MockStockRepository;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.MockStockRepository = this.Fixture.Freeze<Mock<IStockRepository>>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="GetStockItemsQueryHandler"/>.</returns>
            public GetStockItemsQueryHandler BuildSut()
            {
                return new GetStockItemsQueryHandler(this.MockStockRepository.Object);
            }
        }
    }
}
