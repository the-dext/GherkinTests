namespace GherkinTests.AAA
{
    /// <summary>
    /// Defines the <see cref="AAAScenario"/>.
    /// </summary>
    public static class AAAScenario
    {
        /// <summary>
        /// Creates a new test scenario.
        /// </summary>
        /// <param name="scenario">The scenario <see cref="string"/>.</param>
        /// <returns>The <see cref="TestScenario"/>.</returns>
        public static AAATestScenario Scenario(string scenario)
        {
            return new AAATestScenario(new AAAScenarioContext(scenario));
        }
    }
}
