namespace GherkinTests.Gherkin.Factories
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.Gherkin.Stages;
    using GherkinTests.Gherkin.Stages.Async;

    /// <summary>
    /// Defines the <see cref="WhenStageFactory" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public static class WhenStageFactory<T>
    {
        /// <summary>
        /// The CreateAsyncStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStageAsync{T}"/>.</returns>
        public static WhenStageAsync<T> CreateAsyncStage(ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            return new WhenStageAsync<T>(scenarioContext, func);
        }

        /// <summary>
        /// The CreateAsyncStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="WhenStageAsync{T}"/>.</returns>
        public static WhenStageAsync<T> CreateAsyncStage(string stepDescription, ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            scenarioContext.AddStage("When", stepDescription);
            return new WhenStageAsync<T>(scenarioContext, func);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="WhenStage{T}"/>.</returns>
        public static WhenStage<T> CreateStage(ScenarioContext<T> scenarioContext, Action<T> action)
        {
            return new WhenStage<T>(scenarioContext, action);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="WhenStage{T}"/>.</returns>
        public static WhenStage<T> CreateStage(string stepDescription, ScenarioContext<T> scenarioContext, Action<T> action)
        {
            scenarioContext.AddStage("When", stepDescription);
            return new WhenStage<T>(scenarioContext, action);
        }
    }
}
