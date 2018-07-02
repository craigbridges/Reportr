namespace Reportr.Registration.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Represents a configuration for migrating a Reportr context
    /// </summary>
    internal sealed class Configuration
        : DbMigrationsConfiguration<ReportrDbContext>
    {
        /// <summary>
        /// Constructs the configuration with default values
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Automatically seeds the database with any seed data
        /// </summary>
        /// <param name="context">The database context</param>
        protected override void Seed
            (
                ReportrDbContext context
            )
        {

            context.SaveChanges();
        }
    }
}
