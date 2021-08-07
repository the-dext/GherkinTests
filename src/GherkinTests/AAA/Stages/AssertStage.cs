namespace GherkinTests.AAA.Stages
{
    using System;

    /// <summary>
    /// Defines the <see cref="ThenStage" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class AssertStage : IDisposable
    {
        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private AAAScenarioContext scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertStage{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="GherkinTests.ScenarioContext"/>.</param>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public AssertStage(AAAScenarioContext scenarioContext, Action action)
        {
            this.scenarioContext = scenarioContext;
            action();
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
                    this.scenarioContext?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
