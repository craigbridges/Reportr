namespace Reportr.Registration.Entity
{
    using Nito.AsyncEx.Synchronous;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an Entity Framework unit of work implementation
    /// </summary>
    public sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the unit of work with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfUnitOfWork
            (
                ReportrDbContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Saves all changes made to the underlying database
        /// </summary>
        /// <returns>The number of objects written to the database</returns>
        public int SaveChanges()
        {
            return SaveChangesAsync().WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously saves all changes made to the underlying database
        /// </summary>
        /// <returns>The number of objects written to the database</returns>
        public async Task<int> SaveChangesAsync()
        {
            var rows = default(int);
            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var saveTask = _context.SaveChangesAsync();

                    rows = await saveTask.ConfigureAwait
                    (
                        false
                    );

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new DataException
                    (
                        "The transaction failed. See inner exception for details.",
                        ex
                    );
                }
            }
            
            return rows;
        }

        /// <summary>
        /// Forces the database context dispose method to run
        /// </summary>
        public void Dispose()
        {
            // NOTE:
            // This was disabled after having issues with the DbContext being disposed early.
            // See this Stack Overflow answer for more: http://stackoverflow.com/a/35587389

            // In a nutshell, Autofac would create an instance of a repository and DbContext
            // per lifetime scope but a new instance of IUnitOfWork for different threads 
            // in the same lifetime scope. At some point the DbContext gets disposed but when 
            // a new instance of IUnitOfWork is instantiated it uses the old instance of the
            // repository and DbContext that was created for the current lifetime scope 
            // (which has already been disposed).

            //this.Context.Dispose();
        }
    }
}
