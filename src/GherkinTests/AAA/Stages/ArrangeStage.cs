namespace GherkinTests.AAA.Stages
{
    using System;

    /// <summary>
    /// Defines the <see cref="GivenStage"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ArrangeStage : IDisposable
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
        /// Defines the whenStage.
        /// </summary>
        private ActStage actStage;

        internal ArrangeStage(AAAScenarioContext scenarioContext, Action action)
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
        /// The When.
        /// </summary>
        /// <param name="action">The action <see cref="Action"/>.</param>
        /// <returns>The <see cref="WhenStage"/>.</returns>
        public ActStage Act(Action action)
        {
            this.actStage = new ActStage(this.scenarioContext, action);
            return this.actStage;
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
                    this.actStage?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
