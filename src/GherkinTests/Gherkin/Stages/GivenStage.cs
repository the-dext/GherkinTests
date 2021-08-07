namespace GherkinTests.Gherkin.Stages
{
    using System;

    using GherkinTests.Gherkin.Factories;

    /// <summary>
    /// Defines the <see cref="GivenStage"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class GivenStage<T> : IDisposable
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
        /// Defines the whenStage.
        /// </summary>
        private WhenStage<T> whenStage;

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStage{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        internal GivenStage(ScenarioContext<T> scenarioContext, Action<T> action)
        {
            this.scenarioContext = scenarioContext;
            action(scenarioContext.GetSut());
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public GivenStage<T> And(Action<T> action)
        {
            action(this.scenarioContext.GetSut());
            return this;
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public GivenStage<T> And(string stepDescription, Action<T> action)
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
        /// The When.
        /// </summary>
        /// <param name="action">The action <see cref="Action"/>.</param>
        /// <returns>The <see cref="WhenStage"/>.</returns>
        public WhenStage<T> When(Action<T> action)
        {
            this.whenStage = WhenStageFactory<T>.CreateStage(this.scenarioContext, action);
            return this.whenStage;
        }

        /// <summary>
        /// The When.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="WhenStage{T}"/>.</returns>
        public WhenStage<T> When(string stepDescription, Action<T> action)
        {
            this.scenarioContext.AddStage("When", stepDescription);
            this.whenStage = WhenStageFactory<T>.CreateStage(this.scenarioContext, action);
            return this.whenStage;
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
                    this.whenStage?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
