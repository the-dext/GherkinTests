namespace GherkinTests.Gherkin.Factories
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.Gherkin.Stages;
    using GherkinTests.Gherkin.Stages.Async;

    /// <summary>
    /// Defines the <see cref="GivenStageFactory{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public static class GivenStageFactory<T>
    {
        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="GivenStageAsync{T}"/>.</returns>
        public static GivenStageAsync<T> CreateAsyncStage(ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            return new GivenStageAsync<T>(scenarioContext, func);
        }

        /// <summary>
        /// The CreateAsyncStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="GivenStageAsync{T}"/>.</returns>
        public static GivenStageAsync<T> CreateAsyncStage(string stepDescription, ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            scenarioContext.AddStage("Given", stepDescription);
            return new GivenStageAsync<T>(scenarioContext, func);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public static GivenStage<T> CreateStage(ScenarioContext<T> scenarioContext, Action<T> action)
        {
            GivenStage<T> stage = new GivenStage<T>(scenarioContext, action);
            return stage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GivenStage{T}"/> class.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="GivenStage{T}"/>.</returns>
        public static GivenStage<T> CreateStage(string stepDescription, ScenarioContext<T> scenarioContext, Action<T> action)
        {
            scenarioContext.AddStage("Given", stepDescription);
            return new GivenStage<T>(scenarioContext, action);
        }
    }
}
