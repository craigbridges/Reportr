namespace Reportr.Registration.Authorization
{
    using System;

    /// <summary>
    /// Represents a single report role
    /// </summary>
    public class ReportRole
    {
        /// <summary>
        /// Constructs the report role with its default configuration
        /// </summary>
        protected ReportRole() { }

        /// <summary>
        /// Constructs the report role with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        public ReportRole
            (
                string name,
                string title,
                string description
            )
        {
            Validate.IsNotNull(name);
            Validate.IsNotNull(title);

            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;

            this.Name = name;
            this.Title = title;
            this.Description = description;
        }
        
        /// <summary>
        /// Gets the unique ID of the report role
        /// </summary>
        public Guid Id { get; protected set; }

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
    }
}
