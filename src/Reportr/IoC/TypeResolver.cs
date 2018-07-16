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

            var allRegisteredTypes = new List<RegisteredType>();

            var allTypes = assemblies.SelectMany
            (
                x => x.GetTypes()
            );

            var registries = allTypes.Where
            (
                x => typeof(ITypeRegister).IsAssignableFrom(x) 
                    && false == x.IsInterface 
                    && false == x.IsAbstract
                    && x.HasDefaultConstructor()
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
