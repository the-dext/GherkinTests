namespace GherkinTests.AAA.Stages
{
    using System;

    /// <summary>
    /// Defines the <see cref="WhenStage"/>.
    /// </summary>
    public class ActStage : IDisposable
    {
        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly AAAScenarioContext scenarioContext;
        private bool disposedValue;
        private AssertStage assertStage;

        internal ActStage(AAAScenarioContext scenarioContext, Action action)
        {
            this.scenarioContext = scenarioContext;
            action();
        }

        /// <summary>
        /// The Then.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        /// <returns>The <see cref="ThenStage"/>.</returns>
        public AssertStage Assert(Action action)
        {
            this.assertStage = new AssertStage(this.scenarioContext, action);
            return this.assertStage;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    this.assertStage?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~WhenStage()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
