namespace Reportr.Registration.Entity
{
    using Reportr.Registration.Authorization;
    using Reportr.Registration.Categorization;
    using Reportr.Registration.Globalization;
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
        /// Gets or sets the registered languages set
        /// </summary>
        public DbSet<RegisteredLanguage> RegisteredLanguages { get; set; }

        /// <summary>
        /// Gets or sets the registered phrases set
        /// </summary>
        public DbSet<RegisteredPhrase> RegisteredPhrases { get; set; }

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
            
            registrar.Add(new RegisteredReportEntityConfiguration());
            registrar.Add(new RegisteredReportSourceRevisionEntityConfiguration());
            registrar.Add(new ReportCategoryEntityConfiguration());
            registrar.Add(new ReportCategoryAssignmentEntityConfiguration());
            registrar.Add(new ReportRoleEntityConfiguration());
            registrar.Add(new ReportRoleAssignmentEntityConfiguration());
            registrar.Add(new RegisteredLanguageEntityConfiguration());
            registrar.Add(new RegisteredPhraseEntityConfiguration());
            registrar.Add(new RegisteredPhraseTranslationEntityConfiguration());

            ConfigureTypes(modelBuilder);
        }

        /// <summary>
        /// Changes the default EF types conversion configuration
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        private void ConfigureTypes
            (
                DbModelBuilder modelBuilder
            )
        {
            // Changes decimal or decimal? precision
            modelBuilder.Properties().Where
            (
                x => x.PropertyType == typeof(decimal) || x.PropertyType == typeof(decimal?)
            )
            .Configure
            (
                c => c.HasPrecision(19, 4)
            );

            // Changes string to nvarchar 4000
            modelBuilder.Properties().Where
            (
                x => x.PropertyType == typeof(string)
            )
            .Configure
            (
                c => c.HasMaxLength(4000).IsVariableLength()
            );
        }

        public void Migrate()
        {
            Database.SetInitializer
            (
                new MigrateDatabaseToLatestVersion<ReportrDbContext, Configuration>()
            );

            this.ReadAllDateTimeValuesAsUtc();

            this.Database.Initialize(false);
        }
    }
}
