namespace GherkinTests.Gherkin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ScenarioContext{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ScenarioContext<T> : IDisposable
    {
        /// <summary>
        /// Defines the scenarioDescriptors.
        /// </summary>
        private readonly List<(string stage, List<string> steps)> scenarioDescriptors;

        /// <summary>
        /// Defines the isDisposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Defines the stepFunctions.
        /// </summary>
        private List<Func<T, Task>> stepFunctions;

        /// <summary>
        /// Defines the sut.
        /// </summary>
        private T sut;

        /// <summary>
        /// Defines the sutCreated.
        /// </summary>
        private bool sutCreated;

        /// <summary>
        /// Defines the sutCtorFunc.
        /// </summary>
        private Func<T> sutCtorFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioContext{T}"/> class.
        /// </summary>
        /// <param name="scenario">The scenario<see cref="string"/>.</param>
        internal ScenarioContext(string scenario)
        {
            this.scenarioDescriptors = new List<(string stage, List<string> steps)>
            {
                ("Scenario", new List<string> { scenario })
            };
        }

        /// <summary>
        /// Gets the Scenario.
        /// </summary>
        public string Scenario { get; private set; }

        /// <summary>
        /// The AddStage.
        /// </summary>
        /// <param name="stage">The stage<see cref="string"/>.</param>
        /// <param name="step">The step<see cref="string"/>.</param>
        public void AddStage(string stage, string step)
        {
            this.scenarioDescriptors.Add((stage, new List<string> { step }));
        }

        /// <summary>
        /// The AddStep.
        /// </summary>
        /// <param name="stepType">The stepType<see cref="StepType"/>.</param>
        /// <param name="step">The step<see cref="string"/>.</param>
        public void AddStep(StepType stepType, string step)
        {
            this.scenarioDescriptors.Last().steps.Add($"{stepType}: {step}");
        }

        /// <summary>
        /// The AddStepFunction.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        public void AddStepFunction(Func<T, Task> func)
        {
            if (this.stepFunctions == null)
            {
                this.stepFunctions = new List<Func<T, Task>>();
            }

            this.stepFunctions.Add(func);
        }

        /// <summary>
        /// The AddSutConstructor.
        /// </summary>
        /// <param name="sutCtorFunc">The sutCtorFunc<see cref="Func{T}"/>.</param>
        public void AddSutConstructor(Func<T> sutCtorFunc)
        {
            this.sutCtorFunc = sutCtorFunc;
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
        /// The GetLastStageDescriptor.
        /// </summary>
        /// <returns>The <see cref="(string stage, List{string} steps)"/>.</returns>
        [Obsolete]
        public (string stage, List<string> steps) GetLastStageDescriptor()
        {
            return this.scenarioDescriptors.Last();
        }

        /// <summary>
        /// The GetStageDescriptions.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{(string stage, IReadOnlyList{string} steps)}"/>.</returns>
        [Obsolete]
        public IEnumerable<(string stage, IReadOnlyList<string> steps)> GetStageDescriptions()
        {
            foreach ((string stage, List<string> steps) stage in this.scenarioDescriptors)
            {
                yield return (stage.stage, stage.steps.AsReadOnly());
            }
        }

        /// <summary>
        /// The GetSut.
        /// </summary>
        /// <returns>The <see cref="T"/>.</returns>
        public T GetSut()
        {
            if (!this.sutCreated)
            {
                this.sut = this.sutCtorFunc();
                this.sutCreated = true;
            }
            return this.sut;
        }

        /// <summary>
        /// The GetTestDescription.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTestDescription()
        {
            StringBuilder sb = new StringBuilder();
            this.scenarioDescriptors.ForEach(stg =>
            {
                sb.AppendLine($"{stg.stage}: {stg.steps[0]}");
                stg.steps.Skip(1).ToList().ForEach(step => sb.AppendLine($"\t{step}"));
            });
            return sb.ToString();
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ScenarioContext()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        /// <summary>
        /// The StepFunctions.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Func{T, Task}}"/>.</returns>
        public IEnumerable<Func<T, Task>> StepFunctions()
        {
            if (this.stepFunctions != null)
            {
                foreach (Func<T, Task> func in this.stepFunctions)
                {
                    yield return func;
                }
            }
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        /// <param name="disposing">The disposing<see cref="bool"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    // this.AssertionScope?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.isDisposed = true;
            }
        }
    }
}
