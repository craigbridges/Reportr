namespace Autofac
{
    using Reportr.Integrations.Autofac;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
        /// We scan the bin folder for all assemblies and load them into memory,
        /// this ensures all assemblies are discovered and returned.
        /// </remarks>
        internal static Assembly[] GetAllAssemblies(this ContainerBuilder builder)
        {
            if (_allAssemblies == null)
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                var currentDirectory = currentAssembly.GetDirectoryPath();

                var fileNames = new List<string>(FindAssemblyFileNames(currentDirectory));
                var loadedAssemblies = new List<Assembly>();

                foreach (var assemblyFile in fileNames)
                {
                    loadedAssemblies.Add(Assembly.LoadFrom(assemblyFile));
                }

                _allAssemblies = loadedAssemblies.ToArray();
            }

            IEnumerable<string> FindAssemblyFileNames(string path)
            {
                path = path.Replace("\\\\", "\\");

                if (Directory.Exists(path))
                {
                    var fileNames = Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);

                    return fileNames.Where(x => false == x.Contains(@"\runtimes\"));
                }
                else
                {
                    return new string[] { };
                }
            }

            return _allAssemblies;
        }

        /// <summary>
        /// Registers all Reportr services in the Autofac container
        /// </summary>
        /// <param name="builder">The container builder</param>
        public static void RegisterReportrServices(this ContainerBuilder builder)
        {
            Validate.IsNotNull(builder);

            builder.RegisterModule<AutofacCoreModule>();
            builder.RegisterModule<AutofacExtensionsModule>();
        }
    }
}
