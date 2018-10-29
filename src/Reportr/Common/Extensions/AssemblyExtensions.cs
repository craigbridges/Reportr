namespace System.Reflection
{
    using Reportr;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Various extension methods for assemblies
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets all loadable types for the assembly specified
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <returns>A collection of loadable types</returns>
        /// <remarks>
        /// This is an ugly workaround to avoid getting the exception
        /// ReflectionTypeLoadException while loading all types from
        /// an assembly.
        /// 
        /// For example, if the assembly contains types referencing 
        /// an assembly which is currently not available.
        /// 
        /// See this article for more https://tinyurl.com/ycloaxgg
        /// </remarks>
        public static IEnumerable<Type> GetLoadableTypes
            (
                this Assembly assembly
            )
        {
            Validate.IsNotNull(assembly);

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where
                (
                    t => t != null
                );
            }
        }
    }
}
