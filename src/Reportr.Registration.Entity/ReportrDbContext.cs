namespace Reportr.Registration.Entity
{
    using Microsoft.EntityFrameworkCore;
    using Reportr.Registration.Authorization;
    using Reportr.Registration.Categorization;
    using Reportr.Registration.Entity.Configurations;
    using System;

    /// <summary>
    /// Represents a Reportr database context implementation
    /// </summary>
    public class ReportrDbContext : DbContext
    {
        /// <summary>
        /// Constructs the context with default options
        /// </summary>
        /// <param name="optionsGenerator">The options generator</param>
        public ReportrDbContext
            (
                IDbContextOptionsGenerator optionsGenerator
            )
            : this(optionsGenerator.Generate())
        { }

        /// <summary>
        /// Constructs the context with the options specified
        /// </summary>
        /// <param name="options">The context options</param>
        public ReportrDbContext
            (
                DbContextOptions options
            )
            : base(options)
        {
            Validate.IsNotNull(options);

            ReportrDbContext.CurrentContextOptions = options;
        }

        /// <summary>
        /// Gets the last used options instance
        /// </summary>
        public static DbContextOptions CurrentContextOptions { get; private set; }

        /// <summary>
        /// Gets or sets the registered reports set
        /// </summary>
        public DbSet<RegisteredReport> RegisteredReports { get; set; }

        /// <summary>
        /// Gets or sets the registered report categories set
        /// </summary>
        public DbSet<ReportCategory> ReportCategories { get; set; }

        /// <summary>
        /// Gets or sets the registered report roles set
        /// </summary>
        public DbSet<ReportRole> ReportRoles { get; set; }

        /// <summary>
        /// Gets or sets the registered report role assignments set
        /// </summary>
        public DbSet<ReportRoleAssignment> ReportRoleAssignments { get; set; }

        /// <summary>
        /// Handles the model creation process by injecting entity configurations into the model builder
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating
            (
                ModelBuilder modelBuilder
            )
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.SpecifyDateKind<RegisteredReport>();
            modelBuilder.SpecifyDateKind<RegisteredReportSourceRevision>();
            modelBuilder.SpecifyDateKind<ReportCategory>();
            modelBuilder.SpecifyDateKind<ReportCategoryAssignment>();
            modelBuilder.SpecifyDateKind<ReportRole>();
            modelBuilder.SpecifyDateKind<ReportRoleAssignment>();

            modelBuilder.ApplyConfiguration
            (
                new RegisteredReportEntityConfiguration()
            );

            modelBuilder.ApplyConfiguration
            (
                new RegisteredReportSourceRevisionEntityConfiguration()
            );

            modelBuilder.ApplyConfiguration
            (
                new ReportCategoryEntityConfiguration()
            );

            modelBuilder.ApplyConfiguration
            (
                new ReportCategoryAssignmentEntityConfiguration()
            );

            modelBuilder.ApplyConfiguration
            (
                new ReportParameterConstraintEntityConfiguration()
            );

            modelBuilder.ApplyConfiguration
            (
                new ReportRoleEntityConfiguration()
            );

            modelBuilder.ApplyConfiguration
            (
                new ReportRoleAssignmentEntityConfiguration()
            );
        }
    }
}
