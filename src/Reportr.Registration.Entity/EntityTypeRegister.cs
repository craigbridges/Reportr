namespace Reportr.Registration.Entity
{
    using Reportr.IoC;
    using Reportr.Registration.Authorization;
    using Reportr.Registration.Categorization;
    using Reportr.Registration.Entity.Repositories;
    using System.Collections.Generic;

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
            return new List<RegisteredType>()
            {
                new RegisteredType
                (
                    typeof(ReportrDbContext),
                    typeof(ReportrDbContext)
                ),
                new RegisteredType
                (
                    typeof(IDbContextOptionsGenerator),
                    typeof(DefaultDbContextOptionsGenerator)
                ),
                new RegisteredType
                (
                    typeof(IReportrDbMigrator),
                    typeof(DefaultReportrDbMigrator)
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
                ),
                new RegisteredType
                (
                    typeof(IReportRoleRepository),
                    typeof(EfReportRoleRepository)
                ),
                new RegisteredType
                (
                    typeof(IReportRoleAssignmentRepository),
                    typeof(EfReportRoleAssignmentRepository)
                )
            };
        }
    }
}
