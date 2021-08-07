namespace GherkinTests.AAA.Stages.Async
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ArrangeStageAsync{T}"/>.
    /// </summary>
    public class ArrangeStageAsync : IDisposable
    {
        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly AAAScenarioContext scenarioContext;

        /// <summary>
        /// Defines the actStage.
        /// </summary>
        private ActStageAsync actStage;

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrangeStageAsync"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext <see cref="AAAScenarioContext"/>.</param>
        /// <param name="func">The func <see cref="Func{Task}"/>.</param>
        internal ArrangeStageAsync(AAAScenarioContext scenarioContext, Func<Task> func)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddStepFunction(func);
        }

        /// <summary>
        /// The ActAsync.
        /// </summary>
        /// <param name="func">The func<see cref="Func{Task}"/>.</param>
        /// <returns>The <see cref="ActStageAsync"/>.</returns>
        public ActStageAsync ActAsync(Func<Task> func)
        {
            this.actStage = new ActStageAsync(this.scenarioContext, func);
            return this.actStage;
        }

        /// <summary>
        /// The Act step. Takes an action but will wrap it in a Func that returns a task to make it async.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public ActStageAsync ActAsync(Action action)
        {
            var stepFunc = new Func<Task>(() => Task.Run(() => action()));
            this.actStage = new ActStageAsync(this.scenarioContext, stepFunc);
            return this.actStage;
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
        /// <param name="disposing">The disposing <see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    this.actStage?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
