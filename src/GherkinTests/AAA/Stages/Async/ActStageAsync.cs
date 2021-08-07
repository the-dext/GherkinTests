namespace GherkinTests.AAA.Stages.Async
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ActStageAsync" />.
    /// </summary>
    public class ActStageAsync : IDisposable
    {
        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly AAAScenarioContext scenarioContext;

        /// <summary>
        /// Defines the assertStage.
        /// </summary>
        private AssertStageAsync assertStage;

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActStageAsync"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext"/>.</param>
        /// <param name="func">The func<see cref="Func{Task}"/>.</param>
        internal ActStageAsync(AAAScenarioContext scenarioContext, Func<Task> func)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddStepFunction(func);
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="func">The func<see cref="Func{Task}"/>.</param>
        /// <returns>The <see cref="WhenStage"/>.</returns>
        public AssertStageAsync AssertAsync(Func<Task> func)
        {
            this.assertStage = new AssertStageAsync(this.scenarioContext, func);
            return this.assertStage;
        }

        /// <summary>
        /// The assert stage, accepts an action and wraps it in a func that will make it async
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public AssertStageAsync AssertAsync(Action action)
        {
            var stepFunc = new Func<Task>(() => Task.Run(() => action()));
            this.assertStage = new AssertStageAsync(this.scenarioContext, stepFunc);
            return this.assertStage;
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
                    this.assertStage?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
