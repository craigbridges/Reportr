namespace Reportr.Registration.Entity
{
    /// <summary>
    /// Defines a contract for a service that migrates Reportr databases
    /// </summary>
    public interface IReportrDbMigrator
    {
        /// <summary>
        /// Migrates the database to the latest version
        /// </summary>
        void Migrate();
    }
}
