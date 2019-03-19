namespace Reportr.Registration.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Represents a configuration for migrating a Reportr context
    /// </summary>
    internal sealed class Configuration
        : DbMigrationsConfiguration<ReportrDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}
