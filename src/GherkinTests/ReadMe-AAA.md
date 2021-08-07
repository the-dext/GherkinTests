
## Using Arrange-Act-Assert Tests

Arrange-Act-Assert (AAA) Tests follow a simple three step structure which is less.
The Arrange step is used to set up pre-conditions for the test. The Act step is used to invoke the action that you're trying to test, and the Assert step is used to check post-conditions have been met.

In each stage of the test you can optionally provide a description of the stage, and a you must supply either a function or an Action (depending on the step) that is invoked when the step runs.

### Import the Namespace
Using an AAA structure in your unit tests starts by importing the static Scenario function using the following syntax
`using static GherkinTests.AAA.AAAScenario;`

### The Scenario
Your AAA unit tests start by creating a test scenario. Do this by calling the static Scenario function, passing a string where you describe what your test scenario is.
The `Scenario` instance that is returned is disposable so wrap this in a using statement, **this is important to make sure your assertions work correctly.**
You may also define any variables your test will need within the using scope, such as the variable to hold the instace of the class you are testing.
```
using (var scenario = Scenario("Can scan multiple items"))
{
    Till sut = default;
    List<string> expectedBarcodes = default;
}
```

### Arrange...
The Arrange step is used to setup the pre-conditions for your test including building the instance of your subject under test

```
scenario.Arrange(() => {
    expectedBarcodes = new List<string> { "B15", "A12", "B15", "B15" };

    sut = new TestFixtureBuilder()
        .WithStockKeepingUnit("B15", 0.45, "Biscuits")
        .WithStockKeepingUnit("A12", 0.30, "Apple")
        .BuildSut();
})
```
### Act...
The Act stage is where you invoke the action(s) you are trying to prove work.

```
.Act(() => expectedBarcodes.ForEach(x => sut.ScanItem(x)))
```

### Assert...
The Assert stage is where you assert that post conditions have been met

```
.Assert(() => {
    sut.ListScannedItems().First().Should().Be("B15", "first item should be B15");
    sut.ListScannedItems().Last().Should().Be("B15", "last item should be A12");
});
```

### Complete Test Example
```
using static GherkinTests.Gherkin.GherkinScenario;

    [Fact]
    public void Can_Scan_MultipleItems()
    {
        using (var scenario = Scenario("Can scan multiple items"))
        {
            Till sut = default;
            List<string> expectedBarcodes = default;

            scenario
            .Arrange(() => {
                expectedBarcodes = new List<string> { "B15", "A12", "B15", "B15" };

                sut = new TestFixtureBuilder()
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .WithStockKeepingUnit("A12", 0.30, "Apple")
                .BuildSut();
            })
            .Act(() => expectedBarcodes.ForEach(x => sut.ScanItem(x)))
            .Assert(() => {
                sut.ListScannedItems().First().Should().Be("B15", "first item should be B15");
                sut.ListScannedItems().Last().Should().Be("B15", "last item should be A12");
            });
        }
    }
```


## ASync Tests
If you need to use async/await in your tests you can do this using the asynchronous counterpart of each test step, which have an `Async` suffix.
There is one important difference when using asynchronous steps however. In order to invoke the test you must call an additional **Go** step as the final step before the Scenario scope is closed.
This will invoke the functions you have setup and perform the test.

Below is a complete example of a test that makes use of asynchronous steps.

```
[Fact]
public async Task ItemDiscount_CanBeApplied_DuringScanning()
{
    using (var scenario = Scenario("Item discount can be applied during scanning"))
    {
        const string Barcode = "B15";
        const double ExpectedPrice = 0.45;
        Till sut = default;

        await scenario.ArrangeAsync(async () =>
        {
            await Task.Run(() =>
                sut = new TestFixtureBuilder()
                    .WithStockKeepingUnit(Barcode, 0.30, "Biscuits")
                    .WithMockDiscount(Barcode)
                    .BuildSut());
            sut.ScanItem(Barcode);
        })
        .ActAsync(async () => await sut.ScanItemAsync(Barcode))
        .AssertAsync(async () => (await sut.RequestTotalPriceAsync()).Should().Be(ExpectedPrice, "the price should include the discount"))
        .Go();
    }
}
```