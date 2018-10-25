namespace Reportr.Integrations.Autofac
{
    using global::Autofac;
    using Reportr.IoC;

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
            var assemblies = builder.GetAllAssemblies();
            var resolver = new TypeResolver();

            var resolvedTypes = resolver.FindRegisteredTypes
            (
                assemblies
            );

            foreach (var registeredType in resolvedTypes)
            {
                var sourceType = registeredType.SourceType;
                var implementationType = registeredType.ImplementationType;

                builder.RegisterType(implementationType)
                       .As(sourceType)
                       .InstancePerLifetimeScope();
            }
        }
    }
}
