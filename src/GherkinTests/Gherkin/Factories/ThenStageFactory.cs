namespace GherkinTests.Gherkin.Factories
{
    using System;
    using System.Threading.Tasks;

    using GherkinTests.Gherkin.Stages;
    using GherkinTests.Gherkin.Stages.Async;

    /// <summary>
    /// Defines the <see cref="ThenStageFactory{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public static class ThenStageFactory<T>
    {
        /// <summary>
        /// The CreateAsyncStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public static ThenStageAsync<T> CreateAsyncStage(string stepDescription, ScenarioContext<T> scenarioContext, Func<T, Task> func)
        {
            scenarioContext.AddStage("Then", stepDescription);
            return new ThenStageAsync<T>(scenarioContext, func);
        }

        /// <summary>
        /// The CreateAsyncStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="func">The func<see cref="Func{T, string, Task}"/>.</param>
        /// <returns>The <see cref="ThenStageAsync{T}"/>.</returns>
        public static ThenStageAsync<T> CreateAsyncStage(string stepDescription, ScenarioContext<T> scenarioContext, Func<T, string, Task> func)
        {
            scenarioContext.AddStage("Then", stepDescription);
            Func<T, Task> stepFunc = new Func<T, Task>((sut) => func(sut, stepDescription));
            return new ThenStageAsync<T>(scenarioContext, stepFunc);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public static ThenStage<T> CreateStage(ScenarioContext<T> scenarioContext, Action<T> action)
        {
            return new ThenStage<T>(scenarioContext, action);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public static ThenStage<T> CreateStage(string stepDescription, ScenarioContext<T> scenarioContext, Action<T> action)
        {
            scenarioContext.AddStage("Then", stepDescription);
            return new ThenStage<T>(scenarioContext, action);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="stepDescription">The stepDescription<see cref="string"/>.</param>
        /// <param name="scenarioContext">The scenarioContext<see cref="AAAScenarioContext{T}"/>.</param>
        /// <param name="action">The action<see cref="Action{T, string}"/>.</param>
        /// <returns>The <see cref="ThenStage{T}"/>.</returns>
        public static ThenStage<T> CreateStage(string stepDescription, ScenarioContext<T> scenarioContext, Action<T, string> action)
        {
            scenarioContext.AddStage("Then", stepDescription);
            Action<T> stepAction = new Action<T>((sut) => action(sut, stepDescription));
            return new ThenStage<T>(scenarioContext, stepAction);
        }
    }
}
