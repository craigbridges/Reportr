namespace Reportr.Registration.Authorization
{
    using System;

    /// <summary>
    /// Represents a single report role
    /// </summary>
    public class ReportRole : IAggregate
    {
        /// <summary>
        /// Constructs the report role with its default configuration
        /// </summary>
        protected ReportRole() { }

        /// <summary>
        /// Constructs the report role with the details
        /// </summary>
        /// <param name="configuration">The role configuration</param>
        public ReportRole
            (
                ReportRoleConfiguration configuration
            )
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;

            Configure(configuration);
        }
        
        /// <summary>
        /// Gets the unique ID of the report role
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the version number of the report role
        /// </summary>
        public int Version { get; protected set; }

        /// <summary>
        /// Gets the date and time the report role was created
        /// </summary>
        public DateTime DateCreated { get; protected set; }

        /// <summary>
        /// Gets the date and time the report role was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the name of the report role
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the title for the report role
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets a description of the report
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Configures the report role
        /// </summary>
        /// <param name="configuration">The role configuration</param>
        public void Configure
            (
                ReportRoleConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            if (String.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new ArgumentException
                (
                    "The report role name is required."
                );
            }

            if (String.IsNullOrWhiteSpace(configuration.Title))
            {
                throw new ArgumentException
                (
                    "The report role title is required."
                );
            }

            this.Name = configuration.Name;
            this.Title = configuration.Title;
            this.Description = configuration.Description;

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }
    }
}
