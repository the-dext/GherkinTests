namespace gherkinTests.Tests
{

    using FluentAssertions;

    using GherkinTests.Gherkin;

    using Xunit;


    using static GherkinTests.Gherkin.GherkinScenario;

    public class ScenarioContextTests
    {
        [Fact]
        public void Given_Stage_And_Steps_Added_When_ToString_Then_TestDescription_Returned()
        {
            var sut = Scenario<string>("Tests can be printed").ScenarioContext;
            sut.AddStage("Given", "A scenario context has been created");
            sut.AddStep(StepType.And, "The user wants to print our the test description");
            sut.AddStage("When", "Each stage is added to the scenario");
            sut.AddStep(StepType.And, "steps are added to the stage");
            sut.AddStep(StepType.And, "the call to print the test description is invoked");
            sut.AddStage("Then", "the test print out is returned");
            sut.AddStep(StepType.And, "each stage and step is included");
            sut.AddStep(StepType.But, "nothing else");

            var result = sut.GetTestDescription();
            result.Should().Be(
                "Scenario: Tests can be printed\r\n"
                + "Given: A scenario context has been created\r\n"
                + "\tAnd: The user wants to print our the test description\r\n"
                + "When: Each stage is added to the scenario\r\n"
                + "\tAnd: steps are added to the stage\r\n"
                + "\tAnd: the call to print the test description is invoked\r\n"
                + "Then: the test print out is returned\r\n"
                + "\tAnd: each stage and step is included\r\n"
                + "\tBut: nothing else\r\n");
        }
    }
}
