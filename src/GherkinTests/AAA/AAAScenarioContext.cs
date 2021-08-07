namespace GherkinTests.AAA
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions.Execution;

    /// <summary>
    /// Defines the <see cref="AAAScenarioContext{T}" />.
    /// </summary>
    public class AAAScenarioContext : IDisposable
    {
        /// <summary>
        /// Defines the isDisposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Defines the stepFunctions.
        /// </summary>
        private List<Func<Task>> stepFunctions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AAAScenarioContext"/> class.
        /// </summary>
        /// <param name="_">The _<see cref="string"/>.</param>
        internal AAAScenarioContext(string _)
        {
            this.AssertionScope = new AssertionScope();
        }

        /// <summary>
        /// Gets the AssertionScope.
        /// </summary>
        public AssertionScope AssertionScope { get; }

        /// <summary>
        /// Gets the Scenario.
        /// </summary>
        public string Scenario { get; private set; }

        /// <summary>
        /// The AddStepFunction.
        /// </summary>
        /// <param name="func">The func<see cref="Func{T, Task}"/>.</param>
        public void AddStepFunction(Func<Task> func)
        {
            if (this.stepFunctions == null)
            {
                this.stepFunctions = new List<Func<Task>>();
            }

            this.stepFunctions.Add(func);
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
        /// The StepFunctions.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Func{T, Task}}"/>.</returns>
        public IEnumerable<Func<Task>> StepFunctions()
        {
            if (this.stepFunctions != null)
            {
                foreach (Func<Task> func in this.stepFunctions)
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
                    this.AssertionScope?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.isDisposed = true;
            }
        }
    }
}
