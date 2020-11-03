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

        public EfRegisteredReportRepository(ReportrDbContext context)
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        public void AddReport(RegisteredReport report)
        {
            Validate.IsNotNull(report);

            var set = _context.Set<RegisteredReport>();

            var nameUsed = set.Any
            (
                _ => _.Name.Equals(report.Name, StringComparison.OrdinalIgnoreCase)
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

        public bool IsRegistered(string name)
        {
            Validate.IsNotEmpty(name);

            var set = _context.Set<RegisteredReport>();

            return set.Any(_ => _.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public RegisteredReport GetReport(string name)
        {
            Validate.IsNotEmpty(name);

            var set = _context.Set<RegisteredReport>();

            var report = set.FirstOrDefault
            (
                _ => _.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
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

        public IEnumerable<RegisteredReport> GetAllReports()
        {
            return _context.Set<RegisteredReport>().OrderBy(_ => _.Name);
        }

        public void UpdateReport(RegisteredReport report)
        {
            Validate.IsNotNull(report);

            var entry = _context.Entry<RegisteredReport>(report);

            entry.State = EntityState.Modified;
        }

        public void RemoveReport(string name)
        {
            Validate.IsNotEmpty(name);

            var report = GetReport(name);
            var entry = _context.Entry<RegisteredReport>(report);

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<RegisteredReport>().Attach(report);
            }

            _context.Set<RegisteredReport>().Remove(report);
        }
    }
}
