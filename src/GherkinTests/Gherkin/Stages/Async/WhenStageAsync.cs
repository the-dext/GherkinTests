namespace GherkinTests.Gherkin.Stages.Async
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.Gherkin.Factories;

    /// <summary>
    /// Defines the <see cref="WhenStage"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class WhenStageAsync<T> : IDisposable
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
        private ThenStageAsync<T> thenStage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhenStageAsync{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        internal WhenStageAsync(ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddStepFunction(func);
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStage"/>.</returns>
        public WhenStageAsync<T> AndAsync(Func<T, Task> func)
        {
            this.scenarioContext.AddStepFunction(func);
            return this;
        }

        /// <summary>
        /// The AndAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStageAsync{T}"/>.</returns>
        public WhenStageAsync<T> AndAsync(string stepDescription, Func<T, Task> func)
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
        /// The Then.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStage"/>.</returns>
        public ThenStageAsync<T> ThenAsync(Func<T, Task> func)
        {
            this.thenStage = new ThenStageAsync<T>(this.scenarioContext, func);
            return this.thenStage;
        }

        /// <summary>
        /// The ThenAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ThenAsync(string stepDescription, Func<T, Task> func)
        {
            this.thenStage = ThenStageFactory<T>.CreateAsyncStage(stepDescription, this.scenarioContext, func);
            return this.thenStage;
        }

        /// <summary>
        /// The ThenAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, string, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ThenAsync(string stepDescription, Func<T, string, Task> func)
        {
            this.thenStage = ThenStageFactory<T>.CreateAsyncStage(stepDescription, this.scenarioContext, func);
            return this.thenStage;
        }

        /// <summary>
        /// Takes an action which will automatically be wrapped in a task so that it can be called as part of the step sequence returned from the Go() method.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>.</returns>
        public ThenStageAsync<T> ThenAsync(Action<T> action)
        {
            Func<T, Task> stepFunc = new Func<T, Task>(sut => Task.Run(() => action(sut)));
            this.thenStage = new ThenStageAsync<T>(this.scenarioContext, stepFunc);
            return this.thenStage;
        }

        /// <summary>
        /// The ThenAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ThenAsync(string stepDescription, Action<T> action)
        {
            Func<T, Task> stepFunc = new Func<T, Task>(sut => Task.Run(() => action(sut)));
            this.thenStage = ThenStageFactory<T>.CreateAsyncStage(stepDescription, this.scenarioContext, stepFunc);
            return this.thenStage;
        }

        /// <summary>
        /// The ThenAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="action">The action<see cref="Action{T, string}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ThenAsync(string stepDescription, Action<T, string> action)
        {
            Func<T, Task> stepFunc = new Func<T, Task>(sut => Task.Run(() => action(sut, stepDescription)));
            this.thenStage = ThenStageFactory<T>.CreateAsyncStage(stepDescription, this.scenarioContext, stepFunc);
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
