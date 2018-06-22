namespace Reportr.Registration.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    
    /// <summary>
    /// Represents an Entity Framework registered report repository
    /// </summary>
    public sealed class EfRegisteredReportRepository : IRegisteredReportRepository
    {
        private readonly DbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfRegisteredReportRepository
            (
                DbContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Adds a single registered report to the repository
        /// </summary>
        /// <param name="report">The registered report</param>
        public void AddReport
            (
                RegisteredReport report
            )
        {
            var nameUsed = _context.Set<RegisteredReport>().Any
            (
                r => r.Name.ToLower() == report.Name.ToLower()
            );

            if (nameUsed)
            {
                var message = "A report named '{0}' has already been registered.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        report.Name
                    )
                );
            }

            _context.Set<RegisteredReport>().Add
            (
                report
            );
        }

        /// <summary>
        /// Determines if a report has been registered
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        public bool IsRegistered
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return _context.Set<RegisteredReport>().Any
            (
                r => r.Name.ToLower() == name.ToLower()
            );
        }

        /// <summary>
        /// Gets a single registered report from the repository
        /// </summary>
        /// <param name="name">The name of the report</param>
        /// <returns>The matching registered report</returns>
        public RegisteredReport GetReport
            (
                string name
            )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all registered reports in the repository
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetAllReports()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a single registered report in the repository
        /// </summary>
        /// <param name="report">The registered report to update</param>
        public void UpdateReport
            (
                RegisteredReport report
            )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a single registered report from the repository
        /// </summary>
        /// <param name="name">The name of the report</param>
        public void RemoveReport
            (
                string name
            )
        {
            throw new NotImplementedException();
        }
    }
}
