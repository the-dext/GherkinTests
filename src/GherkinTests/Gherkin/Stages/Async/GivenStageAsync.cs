namespace GherkinTests.Gherkin.Stages.Async
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.Gherkin.Factories;

    /// <summary>
    /// Defines the <see cref="GivenStage"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class GivenStageAsync<T> : IDisposable
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
        private WhenStageAsync<T> whenStage;

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStageAsync{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        internal GivenStageAsync(ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddStepFunction(func);
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public GivenStageAsync<T> AndAsync(Func<T, Task> func)
        {
            this.scenarioContext.AddStepFunction(func);
            return this;
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public GivenStageAsync<T> AndAsync(string stepDescription, Func<T, Task> func)
        {
            this.scenarioContext.AddStep(StepType.And, stepDescription);
            this.scenarioContext.AddStepFunction(func);
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
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStage"/>.</returns>
        public WhenStageAsync<T> WhenAsync(Func<T, Task> func)
        {
            this.whenStage = WhenStageFactory<T>.CreateAsyncStage(this.scenarioContext, func);
            return this.whenStage;
        }

        /// <summary>
        /// The When.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStage{T}"/>.</returns>
        public WhenStageAsync<T> WhenAsync(string stepDescription, Func<T, Task> func)
        {
            this.scenarioContext.AddStage("When", stepDescription);
            this.whenStage = WhenStageFactory<T>.CreateAsyncStage(this.scenarioContext, func);
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
