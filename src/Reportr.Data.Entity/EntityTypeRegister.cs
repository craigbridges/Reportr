namespace Reportr.Data.Entity
{
    using Reportr.IoC;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents the data entity type register implementation
    /// </summary>
    public sealed class EntityTypeRegister : ITypeRegister
    {
        /// <summary>
        /// Gets a collection of all registered types
        /// </summary>
        /// <returns>A collection of registered types</returns>
        public IEnumerable<RegisteredType> GetRegisteredTypes()
        {
            return new List<RegisteredType>()
            {
                new RegisteredType
                (
                    typeof(IDataSource),
                    typeof(EfDataSource)
                ),
                new RegisteredType
                (
                    typeof(EfDataSource),
                    typeof(EfDataSource)
                )
            };
        }
    }
}
