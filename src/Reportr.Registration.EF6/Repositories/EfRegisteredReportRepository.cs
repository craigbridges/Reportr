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
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfRegisteredReportRepository
            (
                ReportrDbContext context
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
            Validate.IsNotNull(report);

            var set = _context.Set<RegisteredReport>();

            var nameUsed = set.Any
            (
                r => r.Name.Equals(report.Name, StringComparison.OrdinalIgnoreCase)
            );

            if (nameUsed)
            {
                throw new InvalidOperationException
                (
                    $"A report named '{report.Name}' has already been registered."
                );
            }

            set.Add(report);
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

            var set = _context.Set<RegisteredReport>();

            return set.Any
            (
                r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
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
            Validate.IsNotEmpty(name);

            var set = _context.Set<RegisteredReport>();

            var report = set.FirstOrDefault
            (
                r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );

            if (report == null)
            {
                throw new KeyNotFoundException
                (
                    $"No report was found with the name '{name}'."
                );
            }

            return report;
        }

        /// <summary>
        /// Gets all registered reports in the repository
        /// </summary>
        /// <returns>A collection of registered reports</returns>
        public IEnumerable<RegisteredReport> GetAllReports()
        {
            var reports = _context.Set<RegisteredReport>();

            return reports.OrderBy
            (
                a => a.Name
            );
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
            Validate.IsNotNull(report);

            var entry = _context.Entry<RegisteredReport>
            (
                report
            );

            entry.State = EntityState.Modified;
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
            Validate.IsNotEmpty(name);

            var report = GetReport(name);

            var entry = _context.Entry<RegisteredReport>
            (
                report
            );

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<RegisteredReport>().Attach
                (
                    report
                );
            }

            _context.Set<RegisteredReport>().Remove
            (
                report
            );
        }
    }
}
