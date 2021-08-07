namespace GherkinTests.Gherkin.Stages.Async
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ThenStageAsync" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ThenStageAsync<T> : IDisposable
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
        /// Initializes a new instance of the <see cref="ThenStageAsync{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="GherkinTests.ScenarioContext"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        public ThenStageAsync(ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddStepFunction(func);
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync"/>.</returns>
        public ThenStageAsync<T> AndAsync(Func<T, Task> func)
        {
            this.scenarioContext.AddStepFunction(func);
            return this;
        }

        /// <summary>
        /// The And.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> AndAsync(string stepDescription, Func<T, Task> func)
        {
            this.scenarioContext.AddStep(StepType.And, stepDescription);
            this.scenarioContext.AddStepFunction(func);
            return this;
        }

        /// <summary>
        /// The ButAsync.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ButAsync(Func<T, Task> func)
        {
            this.scenarioContext.AddStepFunction(func);
            return this;
        }

        /// <summary>
        /// The ButAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ButAsync(string stepDescription, Func<T, Task> func)
        {
            this.scenarioContext.AddStep(StepType.But, stepDescription);
            this.scenarioContext.AddStepFunction(func);
            return this;
        }

        /// <summary>
        /// The ButAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, string, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public ThenStageAsync<T> ButAsync(string stepDescription, Func<T, string, Task> func)
        {
            this.scenarioContext.AddStep(StepType.But, stepDescription);
            Func<T, Task> stepFunc = new Func<T, Task>((sut) => func(sut, stepDescription));
            this.scenarioContext.AddStepFunction(stepFunc);
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
        /// The Go.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task Go()
        {
            foreach (Func<T, Task> t in this.scenarioContext.StepFunctions())
            {
                await t(this.scenarioContext.GetSut());
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
