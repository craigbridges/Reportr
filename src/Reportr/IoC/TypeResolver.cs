namespace Reportr.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents a service that resolves registered types
    /// </summary>
    public class TypeResolver
    {
        /// <summary>
        /// Finds all registered types in the assemblies specified
        /// </summary>
        /// <param name="assemblies">The assemblies</param>
        /// <returns>A collection of registered types</returns>
        public IEnumerable<RegisteredType> FindRegisteredTypes
            (
                params Assembly[] assemblies
            )
        {
            Validate.IsNotNull(assemblies);

            var allTypes = new List<Type>();
            var allRegisteredTypes = new List<RegisteredType>();

            foreach (var assembly in assemblies)
            {
                var assemblyTypes = assembly.GetLoadableTypes();

                allTypes.AddRange(assemblyTypes);
            }
            
            var registries = allTypes.Where
            (
                type => type != null 
                    && typeof(ITypeRegister).IsAssignableFrom(type) 
                    && false == type.IsInterface 
                    && false == type.IsAbstract 
                    && type.HasDefaultConstructor()
            );

            foreach (var type in registries)
            {
                var registry = Activator.CreateInstance(type) as ITypeRegister;
                var registeredTypes = registry.GetRegisteredTypes();

                foreach (var registration in registeredTypes)
                {
                    if (false == allRegisteredTypes.Contains(registration))
                    {
                        allRegisteredTypes.Add(registration);
                    }
                }
            }

            return allRegisteredTypes;
        }
    }
}
