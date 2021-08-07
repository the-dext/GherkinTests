namespace GherkinTests.AAA
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.AAA.Stages;
    using GherkinTests.AAA.Stages.Async;
    using GherkinTests.Gherkin;

    /// <summary>
    /// Defines the <see cref="AAATestScenario" />.
    /// </summary>
    public class AAATestScenario : IDisposable
    {
        /// <summary>
        /// Defines the arrangeStage.
        /// </summary>
        private ArrangeStage arrangeStage;

        /// <summary>
        /// Defines the arrangeStageAsync.
        /// </summary>
        private ArrangeStageAsync arrangeStageAsync;

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly AAAScenarioContext scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AAATestScenario"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="ScenarioContext{T}"/>.</param>
        internal AAATestScenario(AAAScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        /// <summary>
        /// The Arrange.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        /// <returns>The <see cref="ArrangeStage"/>.</returns>
        public ArrangeStage Arrange(Action action)
        {
            this.arrangeStage = new ArrangeStage(this.scenarioContext, action);
            return this.arrangeStage;
        }

        /// <summary>
        /// The Arrange.
        /// </summary>
        /// <param name="func">The func<see cref="Func{Task}"/>.</param>
        /// <returns>The <see cref="ArrangeStageAsync"/>.</returns>
        public ArrangeStageAsync ArrangeAsync(Func<Task> func)
        {
            this.arrangeStageAsync = new ArrangeStageAsync(this.scenarioContext, func);
            return this.arrangeStageAsync;
        }

        /// <summary>
        /// The arrage, if an action is passed then this will be wrapped in a Fun that returns a task automatically so that it becomes asynchronous
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public ArrangeStageAsync ArrangeAsync(Action action)
        {
            var stepFunc = new Func<Task>(() => Task.Run(()=>action()));
            this.arrangeStageAsync = new ArrangeStageAsync(this.scenarioContext, stepFunc);
            return this.arrangeStageAsync;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    this.arrangeStage?.Dispose();
                    this.arrangeStageAsync?.Dispose();
                    this.scenarioContext?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
