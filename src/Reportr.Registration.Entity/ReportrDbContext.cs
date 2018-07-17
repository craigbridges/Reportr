namespace Reportr.Registration.Entity
{
    using Reportr.Registration.Authorization;
    using Reportr.Registration.Categorization;
    using Reportr.Registration.Entity.Configurations;
    using Reportr.Registration.Entity.Migrations;
    using System.Data.Entity;

    /// <summary>
    /// Represents a Reportr database context implementation
    /// </summary>
    public class ReportrDbContext : DbContext
    {
        /// <summary>
        /// Constructs the context with migrations enabled
        /// </summary>
        public ReportrDbContext()
            : this(typeof(ReportrDbContext).Name)
        { }

        /// <summary>
        /// Constructs the context with migrations enabled
        /// </summary>
        /// <param name="connectionStringName">The name of the connection string</param>
        public ReportrDbContext
            (
                string connectionStringName
            )

            : base(connectionStringName)
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

            Database.SetInitializer
            (
                new MigrateDatabaseToLatestVersion<ReportrDbContext, Configuration>()
            );

            this.ReadAllDateTimeValuesAsUtc();
        }

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
        protected override void OnModelCreating
            (
                DbModelBuilder modelBuilder
            )
        {
            base.OnModelCreating(modelBuilder);

            var registrar = modelBuilder.Configurations;

            //modelBuilder.HasDefaultSchema("reportr");
            
            registrar.Add(new RegisteredReportConfiguration());
            registrar.Add(new RegisteredReportSourceRevisionConfiguration());
            registrar.Add(new ReportCategoryConfiguration());
            registrar.Add(new ReportCategoryAssignmentConfiguration());
            registrar.Add(new ReportRoleConfiguration());
            registrar.Add(new ReportRoleAssignmentConfiguration());
        }
    }
}
