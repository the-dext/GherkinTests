// Checkout Simulator by Chris Dexter, file="CompositionRoot.cs"

namespace CheckoutSimulator.Console.Setup
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Autofac;

    /// <summary>
    /// Defines the <see cref="CompositionRoot"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CompositionRoot
    {
        /// <summary>
        /// The ConfigureIoc.
        /// </summary>
        /// <returns>The <see cref="IContainer"/>.</returns>
        public IContainer ConfigureIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(CheckoutSimulator.Application.Setup.AutofacModule)));
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(CheckoutSimulator.Persistence.Setup.AutofacModule)));
            return builder.Build();
        }
    }
}
