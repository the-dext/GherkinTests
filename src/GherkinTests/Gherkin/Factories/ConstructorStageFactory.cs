namespace GherkinTests.Gherkin.Factories
{
    using System;

    using GherkinTests.Gherkin.Stages;

    /// <summary>
    /// Defines the <see cref="ConstructorStageFactory{T}"/>.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ConstructorStageFactory<T>
    {
        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext <see cref="ScenarioContext{T}"/>.</param>
        /// <param name="sutCtorFunc">The sutCtorFunc <see cref="Func{T}"/>.</param>
        /// <returns>The <see cref="ConstructorStage{T}"/>.</returns>
        public static ConstructorStage<T> CreateStage(ScenarioContext<T> scenarioContext, Func<T> sutCtorFunc)
        {
            return new ConstructorStage<T>(scenarioContext, sutCtorFunc);
        }

        /// <summary>
        /// The CreateStage.
        /// </summary>
        /// <param name="scenarioContext">The scenarioContext <see cref="ScenarioContext{T}"/>.</param>
        /// <param name="sutCtorFunc">The sutCtorFunc <see cref="Func{T}"/>.</param>
        /// <param name="stepDescription">The stepDescription <see cref="string"/>.</param>
        /// <returns>The <see cref="ConstructorStage{T}"/>.</returns>
        public static ConstructorStage<T> CreateStage(ScenarioContext<T> scenarioContext, Func<T> sutCtorFunc, string stepDescription)
        {
            ConstructorStage<T> stage = new ConstructorStage<T>(scenarioContext, sutCtorFunc);
            scenarioContext.AddStage("Setup", stepDescription);
            return stage;
        }
    }
}
