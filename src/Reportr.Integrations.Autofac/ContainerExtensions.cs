namespace Autofac
{
    using Reportr;
    using Reportr.Integrations;

    /// <summary>
    /// Various extension methods for Autofac containers
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Configures the ReportrEngine to be used with Autofac
        /// </summary>
        /// <param name="container">The Autofac container</param>
        public static void ConfigureReportrEngine
            (
                this IContainer container
            )
        {
            Validate.IsNotNull(container);
            
            ReportrEngine.Activator = container.Resolve<IDependencyResolver>();
        }
    }
}
