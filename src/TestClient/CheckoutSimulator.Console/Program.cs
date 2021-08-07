// Checkout Simulator by Chris Dexter, file="Program.cs"

namespace CheckoutSimulator.Console
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the <see cref="Program"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args <see cref="string[]"/>.</param>
        public static void Main()
        {
            new Application().Run();
        }
    }
}