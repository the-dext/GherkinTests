namespace GherkinTests.Gherkin
{
    /// <summary>
    /// Defines the <see cref="GherkinScenario"/>.
    /// </summary>
    public static class GherkinScenario
    {
        /// <summary>
        /// Creates a new test scenario.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="scenario">The scenario <see cref="string"/>.</param>
        /// <returns>The <see cref="TestScenario"/>.</returns>
        public static GherkinTestScenario<T> Scenario<T>(string scenario)
        {
            return new GherkinTestScenario<T>(new ScenarioContext<T>(scenario));
        }
    }
}
