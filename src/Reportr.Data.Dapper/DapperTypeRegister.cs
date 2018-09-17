namespace Reportr.Data.Dapper
{
    using Reportr.IoC;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents the data Dapper type register implementation
    /// </summary>
    public sealed class DapperTypeRegister : ITypeRegister
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
                    typeof(DapperDataSource),
                    typeof(DapperDataSource)
                )
            };
        }
    }
}
