namespace GherkinTests.Gherkin
{
    using System;

    using Factories;

    using GherkinTests.Gherkin.Stages;

    /// <summary>
    /// Defines the <see cref="TestScenario"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class GherkinTestScenario<T> : IDisposable
    {
        /// <summary>
        /// Defines the ScenarioContext.
        /// </summary>
        public ScenarioContext<T> ScenarioContext;

        /// <summary>
        /// Defines the constructorStage.
        /// </summary>
        private ConstructorStage<T> constructorStage;

        /// <summary>
        /// Defines the disposedValue.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="GherkinTestScenario{T}"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        internal GherkinTestScenario(ScenarioContext<T> scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
        }

        /// <summary>
        /// The Against.
        /// </summary>
        /// <param name="sutCtorFunc">The sutCtorFunc<see cref="Func{T}"/>.</param>
        /// <returns>The <see cref="TestScenario"/>.</returns>
        public ConstructorStage<T> Ctor(Func<T> sutCtorFunc)
        {
            this.constructorStage = ConstructorStageFactory<T>.CreateStage(this.ScenarioContext, sutCtorFunc);
            return this.constructorStage;
        }

        /// <summary>
        /// The Ctor.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="sutCtorFunc">The sutCtorFunc<see cref="Func{T}"/>.</param>
        /// <returns>The <see cref="ConstructorStage{T}"/>.</returns>
        public ConstructorStage<T> Ctor(string stepDescription, Func<T> sutCtorFunc)
        {
            this.constructorStage = ConstructorStageFactory<T>.CreateStage(this.ScenarioContext, sutCtorFunc, stepDescription);
            return this.constructorStage;
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
                    this.constructorStage?.Dispose();
                    this.ScenarioContext?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }
    }
}
