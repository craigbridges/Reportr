namespace Reportr.Integrations.Autofac
{
    using System;
    using System.IO;
    using System.Reflection;

    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the path to the directory where the assembly resides
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <returns>The assembly path</returns>
        public static string GetDirectoryPath(this Assembly assembly)
        {
            var filePath = new Uri(assembly.CodeBase).LocalPath;

            return Path.GetDirectoryName(filePath);
        }
    }
}
