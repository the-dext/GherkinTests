namespace GherkinTests.AAA.Stages.Async
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="AssertStageAsync" />.
    /// </summary>
    public class AssertStageAsync : IDisposable
    {
        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly AAAScenarioContext scenarioContext;

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertStageAsync"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="GherkinTests.ScenarioContext"/>.</param>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public AssertStageAsync(AAAScenarioContext scenarioContext, Func<Task> func)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddStepFunction(func);
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
        /// The Go.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task Go()
        {
            foreach (var t in this.scenarioContext.StepFunctions())
            {
                await t();
            }
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
                    this.scenarioContext?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
