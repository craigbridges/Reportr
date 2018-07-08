namespace Reportr.Registration.Authorization
{
    using System;

    /// <summary>
    /// Represents a single report role assignment
    /// </summary>
    public class ReportRoleAssignment
    {
        /// <summary>
        /// Constructs the report role with its default configuration
        /// </summary>
        protected ReportRoleAssignment() { }

        /// <summary>
        /// Constructs the report role assignment with the details
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        public ReportRoleAssignment
            (
                string reportName,
                string roleName
            )
        {
            Validate.IsNotNull(reportName);
            Validate.IsNotNull(roleName);

            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;

            this.ReportName = reportName;
            this.RoleName = roleName;
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
        /// Gets the name of the registered report
        /// </summary>
        public string ReportName { get; protected set; }

        /// <summary>
        /// Gets the name of the report role
        /// </summary>
        public string RoleName { get; protected set; }
    }
}
