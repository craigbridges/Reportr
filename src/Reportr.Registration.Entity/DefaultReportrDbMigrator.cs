namespace Reportr.Registration.Entity
{
    using Microsoft.EntityFrameworkCore;
    using System;

    /// <summary>
    /// Represents the default Reportr database migrator
    /// </summary>
    public sealed class DefaultReportrDbMigrator : IReportrDbMigrator
    {
        private readonly IDbContextOptionsGenerator _optionsGenerator;

        /// <summary>
        /// Constructs the service with required dependencies
        /// </summary>
        /// <param name="optionsGenerator">The context options generator</param>
        public DefaultReportrDbMigrator
            (
                IDbContextOptionsGenerator optionsGenerator
            )
        {
            Validate.IsNotNull(optionsGenerator);

            _optionsGenerator = optionsGenerator;
        }

        /// <summary>
        /// Migrates the database to the latest version
        /// </summary>
        public void Migrate()
        {
            using (var context = new ReportrDbContext(_optionsGenerator))
            {
                context.Database.Migrate();
            }
        }
    }
}
