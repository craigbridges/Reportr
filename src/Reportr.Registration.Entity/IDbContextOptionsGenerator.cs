namespace Reportr.Registration.Entity
{
    using Microsoft.EntityFrameworkCore;
    
    /// <summary>
    /// Defines a contract for a database context options generator
    /// </summary>
    public interface IDbContextOptionsGenerator
    {
        /// <summary>
        /// Generates the context options
        /// </summary>
        /// <returns>The database context options</returns>
        DbContextOptions Generate();
    }
}
