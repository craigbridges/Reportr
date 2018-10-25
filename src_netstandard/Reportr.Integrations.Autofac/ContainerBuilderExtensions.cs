namespace Autofac
{
    using Microsoft.Extensions.DependencyModel;
    using Reportr;
    using Reportr.Integrations.Autofac;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Various extension methods for Autofac container builders
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        private static Assembly[] _allAssemblies = null;

        /// <summary>
        /// Gets all assemblies that area referenced and in the current domain
        /// </summary>
        /// <param name="builder">The container builder</param>
        /// <returns>An array of assemblies</returns>
        /// <remarks>
        /// We merge the two results to ensure assemblies found in the plugins
        /// directory are also included, as they are not referenced.
        /// </remarks>
        internal static Assembly[] GetAllAssemblies
            (
                this ContainerBuilder builder
            )
        {
            if (_allAssemblies == null)
            {
                var assemblies = new List<Assembly>();
                var dependencies = DependencyContext.Default.RuntimeLibraries;

                foreach (var library in dependencies)
                {
                    var assembly = Assembly.Load
                    (
                        new AssemblyName(library.Name)
                    );

                    assemblies.Add(assembly);
                }

                _allAssemblies = assemblies.ToArray();
            }

            return _allAssemblies;
        }

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
