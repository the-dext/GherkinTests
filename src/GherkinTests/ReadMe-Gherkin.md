# Test Structures
Gherkin Tests adds structure to your tests by allowing you to break them into clearly defined steps within a test method.
The test steps can be either Gherkin style, or Arrange-Act-Assert style if that is your preference.
Gherkin Tests does not replace your unit test runner or any assertion framework you may be using. It shoudl work regardless of whether you use MSTest, NUnit or xUnit.
Internally Gherkin Tests uses the FluentAssertion framework and I recommend you try this.

## Using Gherkin Tests

Gherkin tests follow a "given, when, then" structure preceded by a scenario constructor function which is where you tell your test now to construct the SUT (the subject under test - ie the thing you are trying to test. Or if you are writing an integration test then this is the class that you will invoke actions against).

In each stage of the test you can optionally provide a description of the stage, and a you must supply either a function or an Action (depending on the step) that is invoked when the step runs.

### Import the Namespace
Using a Gherkin structure in your unit tests starts by importing the static Scenario function using the following syntax
`using static GherkinTests.Gherkin.GherkinScenario;`

### The Scenario
To apply a gherkin structure to your unit tests start by creating a test scenario. Do this by creating a new instance of `Scenario<T>` where T is the type of class being tested (or if you are creating an integration test T would be the class you are invoking methods against).
The constructor for the scenario will accept a string where you can describe what your test scenario is.
`Scenario<T>` is disposable so wrap this in a using statement, **this is important to make sure your assertions work correctly.**
You may also define any variables your test will need within the using scope.
```
using (var scenario = Scenario<Till>("Complete scanning resets scanned items"))
{
  int originalItemCount = 0;
}
```

### The Scenario Constructor
The scenario constructor is always the first step of a test. It is here that you tell the test how you are going to build an instance of your Subject Under Test (referred to as the SUT from here on in). You can do this anyway you choose to, my preference is to define a builder class that has methods with names that describe what is being built, and I reuse this builder class in each of my tests by calling different combinations of the methods it exposes.
You are free to use simpler techniques if you prefer, the only constraint is that your constructor must return an instance of the type being tested, as defined by the T argument you supplied when you defined your scenario.

```
scenario.Ctor(() => new TestFixtureBuilder()
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .WithPreviouslyScannedItem("B15")
                .WithPreviouslyScannedItem("B15")
                .BuildSut())
```
### Given…
The given step is where you define what has to happen to put your test subject into a known state before you carry out actions and assertions.
As you can see in my example because of the way I write tests I have a bit of an overlap with set up steps performed in the scenario constructor, but here I am using the Given step to read the number of scanned items from the till before I invoke the method I am trying to test.
You may optionally provide a description of the step and I encourage you do so that your tests become self documenting.

The expression that you provide to the given stage may take an instance of the Sut so that you can act upon it and do whatever your test setup requires.
```
.Given("Scanning count is read before completing scanning", sut => originalItemCount = sut.ListScannedItems().Count())
```

### When…
The When step is where you perform the action that you are trying to test. Just like the Given step the expression you use should return a description to help your test be clear, and return a function that accepts an instance of the SUT and invoked the action you want.
```
.When("Complete Scanning is called", sut => sut.CompleteScanning())
```

### …And…
The Then step can be followed by an And step if there are multiple actions you need to perform. Whilst you could invoke multiple actions in the Given step if that is your preference, the And step allows you to improve readability of your tests by clearly separating the invocations.

The full example below does not use this step but it’s simple to use (the method signature is the same as the When step) and you can call it as many times as needed before moving into the Then step.
For example

```
.Given(sut => sut.ScanItem("B15"))
    .And(sut => sut.ScanItem("B15"))
    .And(sut => sut.ScanItem("B15"))
```

### Then…
The Then step is the stage of a Gherkin test where the expectations are defined. In your test this is where you should perform your assertions using whatever assertions methods your test framework provides. In my examples I use Fluent Assertions which provides a set of Should() extension methods.
Just like previous steps you may supply a description for the step and you should provide an Action that will accept an instance of your SUT and does whatever you need it to.
This Action however can also accept a Because argument, which is the description of the step that you provided. The Because argument is supplied so that you can use it in any messages should the assertion fail (for example Fluent Assertions allows you to pass a message to be should if the assertion does not return true).

```
.Then("Item count should be two before completing scanning", (sut, because) => originalItemCount.Should().Be(2, because))
```


### …And…
Some tests require more than a single assertion and while you could do all of these in the Then step it makes more sense to separate them out so that your test is more readable. The And step allows you to do this and you can have as many as you need. The And step, just like the Then step takes a description where you can provide a clear explanation of the rule you are asserting, and an Action which accepts an instance of the SUT and a Because argument so that you can perform the assertions you need.
The Because argument is supplied so that you can use it in any messages should the assertion fail (for example Fluent Assertions allows you to pass a message to be should if the assertion does not return true).

```
.And("Item count should be zero after completing scanning", (sut, because) => sut.ListScannedItems().Count().Should().Be(0, because));
```

### Complete Test Example
```
using static GherkinTests.Gherkin.GherkinScenario;

    [Fact]
    public void ExampleGherkinTest()
    {
        using (var scenario = Scenario<Till>("Complete scanning resets scanned items"))
        {
            int originalItemCount = 0;

            scenario.Ctor(() => new TestFixtureBuilder()
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .WithPreviouslyScannedItem("B15")
                .WithPreviouslyScannedItem("B15")
                .BuildSut())
            .Given("Scanning count is read before completing scanning", sut => originalItemCount = sut.ListScannedItems().Count())
            .When("Complete Scanning is called", sut => sut.CompleteScanning())
            .Then("Item count should be two before completing scanning", (sut, because) => originalItemCount.Should().Be(2, because))
            .And("Item count should be zero after completing scanning", (sut, because) => sut.ListScannedItems().Count().Should().Be(0, because));
        }
    }
```


## ASync Tests
If you need to use async/await in your tests you can do this using the asynchronous counterpart of each test step, which have an `Async` suffix. The method signatures of each step are the same as the non asynchronous version except that the actions and functions have been tweaked to work with Tasks.
There is one important difference when using asynchronous steps however.
In order to invoke the test you must call an additional **Go** step, as the final step before the Scenario scope is closed.
This will invoke the functions you have setup and perform the test.

Below is a complete example of a test that makes use of asynchronous steps.

```
using static GherkinTests.Gherkin.GherkinScenario;

    [Fact]
    public async Task ExampleGherkinTest()
    {

            using (var scenario = Scenario<Till>("Item discount can be applied during scanning"))
            {

                const string Barcode = "B15";
                const double ExpectedPrice = 0.45;

                await scenario
                    .Ctor(() => new TestFixtureBuilder()
                        .WithStockKeepingUnit(Barcode, 0.30, "Biscuits")
                        .WithMockDiscount(Barcode)
                        .BuildSut())
                    .GivenAsync("an item on discount is scanned", async sut => await sut.ScanItemAsync(Barcode))
                    .WhenAsync("a second item is scanned that triggers discount", async (sut) => await Task.Run(() => sut.ScanItemAsync(Barcode)))
                    .ThenAsync("the price should include the discount", async (sut, because) => (await sut.RequestTotalPriceAsync()).Should().Be(ExpectedPrice, because))
                    .Go();
            }
    }
```