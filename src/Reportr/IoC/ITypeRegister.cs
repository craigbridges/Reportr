namespace Reportr.Integrations
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a container that gets registered types
    /// </summary>
    public interface ITypeRegister
    {
        /// <summary>
        /// Gets a collection of all registered types
        /// </summary>
        /// <returns>A collection of registered types</returns>
        IEnumerable<RegisteredType> GetRegisteredTypes();
    }
}
