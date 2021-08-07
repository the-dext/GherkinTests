namespace GherkinTests.Gherkin.Stages
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.Gherkin.Factories;
    using GherkinTests.Gherkin.Stages.Async;

    /// <summary>
    /// Defines the <see cref="ConstructorStage{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ConstructorStage<T> : IDisposable
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
        /// Defines the givenStage.
        /// </summary>
        private GivenStage<T> givenStage;

        /// <summary>
        /// Defines the givenStageAsync.
        /// </summary>
        private GivenStageAsync<T> givenStageAsync;

        /// <summary>
        /// Defines the whenStage.
        /// </summary>
        private WhenStage<T> whenStage;

        /// <summary>
        /// Defines the whenStageAsyc.
        /// </summary>
        private WhenStageAsync<T> whenStageAsyc;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorStage{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="sutCtorFunc">The sutCtorFunc<see cref="Func{T}"/>.</param>
        internal ConstructorStage(ScenarioContext<T> scenarioContext, Func<T> sutCtorFunc)
        {
            this.scenarioContext = scenarioContext;
            this.scenarioContext.AddSutConstructor(sutCtorFunc);
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
        /// The given step is used to set preconditions of the gherkin test.
        /// </summary>
        /// <returns>The <see cref="GivenStage"/>.</returns>
        public GivenStage<T> Given()
        {
            this.givenStage = GivenStageFactory<T>.CreateStage(this.scenarioContext, new Action<T>((sut) => { }));
            return this.givenStage;
        }

        /// <summary>
        /// The given step is used to set preconditions of the gherkin test.
        /// </summary>
        /// <param name="action">The action to run, with the sut as an argument.</param>
        /// <returns>The <see cref="GivenStage"/>.</returns>
        public GivenStage<T> Given(Action<T> action)
        {
            this.givenStage = GivenStageFactory<T>.CreateStage(this.scenarioContext, action);
            return this.givenStage;
        }

        /// <summary>
        /// The given step is used to set preconditions of the gherkin test.
        /// </summary>
        /// <param name="stepDescription">The step description.</param>
        /// <param name="action">The action to run, with the sut as an argument.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public GivenStage<T> Given(string stepDescription, Action<T> action)
        {
            this.givenStage = GivenStageFactory<T>.CreateStage(stepDescription, this.scenarioContext, action);
            return this.givenStage;
        }

        /// <summary>
        /// The GivenAsync.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="GivenStageAsync{T}"/>.</returns>
        public GivenStageAsync<T> GivenAsync(Func<T, Task> func)
        {
            this.givenStageAsync = GivenStageFactory<T>.CreateAsyncStage(this.scenarioContext, func);
            return this.givenStageAsync;
        }

        /// <summary>
        /// The GivenAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="GivenStageAsync{T}"/>.</returns>
        public GivenStageAsync<T> GivenAsync(string stepDescription, Func<T, Task> func)
        {
            this.givenStageAsync = GivenStageFactory<T>.CreateAsyncStage(stepDescription, this.scenarioContext, func);
            return this.givenStageAsync;
        }

        /// <summary>
        /// Whens a action takes place against the subject under test (sut). Bypassing the Given step.
        /// </summary>
        /// <param name="action">The action to run against the sut.</param>
        /// <returns>When stage.</returns>
        public WhenStage<T> When(Action<T> action)
        {
            this.whenStage = WhenStageFactory<T>.CreateStage(this.scenarioContext, action);
            return this.whenStage;
        }

        /// <summary>
        /// Whens a action takes place against the subject under test (sut). Bypassing the Given step.
        /// </summary>
        /// <param name="stepDescription">The step description.</param>
        /// <param name="action">The action to run against the sut.</param>
        /// <returns>When staege.</returns>
        public WhenStage<T> When(string stepDescription, Action<T> action)
        {
            this.whenStage = WhenStageFactory<T>.CreateStage(stepDescription, this.scenarioContext, action);
            return this.whenStage;
        }

        /// <summary>
        /// The WhenAsync.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStageAsync{T}"/>.</returns>
        public WhenStageAsync<T> WhenAsync(Func<T, Task> func)
        {
            this.whenStageAsyc = WhenStageFactory<T>.CreateAsyncStage(this.scenarioContext, func);
            return this.whenStageAsyc;
        }

        /// <summary>
        /// The WhenAsync.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStageAsync{T}"/>.</returns>
        public WhenStageAsync<T> WhenAsync(string stepDescription, Func<T, Task> func)
        {
            this.whenStageAsyc = WhenStageFactory<T>.CreateAsyncStage(stepDescription, this.scenarioContext, func);
            return this.whenStageAsyc;
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
                    this.givenStage?.Dispose();
                    this.whenStage?.Dispose();
                    this.givenStageAsync?.Dispose();
                    this.whenStageAsyc?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
