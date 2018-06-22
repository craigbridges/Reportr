namespace Reportr.Registration
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a Unit of Work contract that can be used with repositories
    /// </summary>
    /// <remarks>
    /// The unit of work represents a transaction when used in data layers. 
    /// Typically the unit of work will roll back the transaction if SaveChanges() 
    /// has not been invoked before being disposed.
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all changes made to the underlying database
        /// </summary>
        /// <returns>The number of objects written to the database</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made to the underlying database
        /// </summary>
        /// <returns>The number of objects written to the database</returns>
        Task<int> SaveChangesAsync();
    }
}
