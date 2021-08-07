// Checkout Simulator by Chris Dexter, file="AutofacModule.cs"

namespace CheckoutSimulator.Persistence.Setup
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Autofac;

    /// <summary>
    /// Defines the <see cref="AutofacModule" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
