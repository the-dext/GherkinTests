namespace GherkinTests.Gherkin.Stages
{
    using System;

    using Factories;

    /// <summary>
    /// Defines the <see cref="WhenStage"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class WhenStage<T> : IDisposable
    {
        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly ScenarioContext<T> scenarioContext;

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Defines the thenStage.
        /// </summary>
        private ThenStage<T> thenStage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenStage{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        internal WhenStage(ScenarioContext<T> scenarioContext, Action<T> action)
        {
            this.scenarioContext = scenarioContext;
            action(this.scenarioContext.GetSut());
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="action">The action <see cref="Action"/>.</param>
        /// <returns>The <see cref="WhenStage"/>.</returns>
        public WhenStage<T> And(Action<T> action)
        {
            action(this.scenarioContext.GetSut());
            return this;
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="WhenStage{T}"/>.</returns>
        public WhenStage<T> And(string stepDescription, Action<T> action)
        {
            this.scenarioContext.AddStep(StepType.And, stepDescription);
            action(this.scenarioContext.GetSut());
            return this;
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
        /// The Then.
        /// </summary>
        /// <param name="action">The action <see cref="Action"/>.</param>
        /// <returns>The <see cref="ThenStage"/>.</returns>
        public ThenStage<T> Then(Action<T> action)
        {
            this.thenStage = new ThenStage<T>(this.scenarioContext, action);
            return this.thenStage;
        }

        /// <summary>
        /// The Then.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> Then(string stepDescription, Action<T> action)
        {
            this.thenStage = ThenStageFactory<T>.CreateStage(stepDescription, this.scenarioContext, action);
            return this.thenStage;
        }

        /// <summary>
        /// The Then.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T, string}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> Then(string stepDescription, Action<T, string> action)
        {
            this.thenStage = ThenStageFactory<T>.CreateStage(stepDescription, this.scenarioContext, action);
            return this.thenStage;
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
                    this.thenStage?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
