namespace Reportr.IoC.Autofac
{
    using global::Autofac;

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
            var assemblies = builder.GetAssemblies();
            var resolver = new TypeResolver();

            var resolvedTypes = resolver.FindRegisteredTypes
            (
                assemblies
            );

            foreach (var registeredType in resolvedTypes)
            {
                var sourceType = registeredType.SourceType;
                var asType = registeredType.ImplementationType;

                builder.RegisterType(sourceType)
                       .As(asType)
                       .InstancePerLifetimeScope();
            }
        }
    }
}
