namespace Reportr.IoC.Autofac
{
    using global::Autofac;

    /// <summary>
    /// Various extension methods for Autofac container builders
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Registers all Reportr services in the Autofac container
        /// </summary>
        /// <param name="builder">The container builder</param>
        public static void RegisterReportrServices
            (
                this ContainerBuilder builder
            )
        {
            Validate.IsNotNull(builder);

            builder.RegisterModule<AutofacCoreModule>();
            builder.RegisterModule<AutofacExtensionsModule>();
        }
    }
}
