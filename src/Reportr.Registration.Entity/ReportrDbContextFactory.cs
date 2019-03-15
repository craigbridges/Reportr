namespace Reportr.Registration.Entity
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// Creates default DBContext for Add-Migration only
    /// </summary>
    public class ReportrDbContextFactory : IDesignTimeDbContextFactory<ReportrDbContext>
    {
        /// <summary>
        /// Create default DBContext
        /// </summary>
        /// <param name="args">Input parameters</param>
        /// <returns>New DBContext</returns>
        public ReportrDbContext CreateDbContext
            (
                string[] args
            )
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReportrDbContext>();

            optionsBuilder.UseSqlServer
            (
                @"Server=(localdb)\mssqllocaldb;Database=Reportr;Trusted_Connection=True;"
            );

            return new ReportrDbContext
            (
                optionsBuilder.Options
            );
        }
    }
}
