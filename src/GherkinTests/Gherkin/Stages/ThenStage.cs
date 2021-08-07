namespace GherkinTests.Gherkin.Stages
{
    using System;

    /// <summary>
    /// Defines the <see cref="ThenStage" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ThenStage<T> : IDisposable
    {
        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Defines the scenarioContext.
        /// </summary>
        private readonly ScenarioContext<T> scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThenStage{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="GherkinTests.ScenarioContext"/>.</param>
        /// <param name="action">The action<see cref="Action"/>.</param>
        public ThenStage(ScenarioContext<T> scenarioContext, Action<T> action)
        {
            this.scenarioContext = scenarioContext;
            action(this.scenarioContext.GetSut());
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="action">The action<see cref="Action"/>.</param>
        /// <returns>The <see cref="ThenStage"/>.</returns>
        public ThenStage<T> And(Action<T> action)
        {
            action(this.scenarioContext.GetSut());
            return this;
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> And(string stepDescription, Action<T> action)
        {
            this.scenarioContext.AddStep(StepType.And, stepDescription);
            action(this.scenarioContext.GetSut());
            return this;
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T, string}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> And(string stepDescription, Action<T, string> action)
        {
            this.scenarioContext.AddStep(StepType.And, stepDescription);
            action(this.scenarioContext.GetSut(), stepDescription);
            return this;
        }

        /// <summary>
        /// The But.
        /// </summary>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> But(Action<T> action)
        {
            action(this.scenarioContext.GetSut());
            return this;
        }

        /// <summary>
        /// The But.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> But(string stepDescription, Action<T> action)
        {
            this.scenarioContext.AddStep(StepType.But, stepDescription);
            action(this.scenarioContext.GetSut());
            return this;
        }

        /// <summary>
        /// The But.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T, string}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public ThenStage<T> But(string stepDescription, Action<T, string> action)
        {
            this.scenarioContext.AddStep(StepType.But, stepDescription);
            action(this.scenarioContext.GetSut(), stepDescription);
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
