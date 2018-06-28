namespace Reportr.Registration.Entity
{
    using Reportr.IoC;
    using Reportr.Registration.Categorization;
    using Reportr.Registration.Entity.Repositories;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// Represents an entity type register implementation
    /// </summary>
    public sealed class EntityTypeRegister : ITypeRegister
    {
        /// <summary>
        /// Gets a collection of all registered types
        /// </summary>
        /// <returns>A collection of registered types</returns>
        public IEnumerable<RegisteredType> GetRegisteredTypes()
        {
            // TODO: correct DbContext

            return new List<RegisteredType>()
            {
                new RegisteredType
                (
                    typeof(DbContext),
                    typeof(DbContext)
                ),
                new RegisteredType
                (
                    typeof(IUnitOfWork),
                    typeof(EfUnitOfWork)
                ),
                new RegisteredType
                (
                    typeof(IRegisteredReportRepository),
                    typeof(EfRegisteredReportRepository)
                ),
                new RegisteredType
                (
                    typeof(IReportCategoryRepository),
                    typeof(EfReportCategoryRepository)
                )
            };
        }
    }
}
