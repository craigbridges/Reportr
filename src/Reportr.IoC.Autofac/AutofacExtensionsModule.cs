namespace Reportr.IoC.Autofac
{
    using global::Autofac;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a custom extensions module for an Autofac container builder
    /// </summary>
    public class AutofacExtensionsModule : Module
    {
        /// <summary>
        /// Overrides the modules base load with custom delegate registrations
        /// </summary>
        /// <param name="builder">The container builder</param>
        protected override void Load
            (
                ContainerBuilder builder
            )
        {
            // TODO: find all dependency registrations by scanning assemblies and register them here

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
        }
    }
}
