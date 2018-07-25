namespace Reportr.Registration.Authorization
{
    /// <summary>
    /// Represents configuration details for a report role assignment
    /// </summary>
    public class ReportRoleAssignmentConfiguration
    {
        /// <summary>
        /// Gets or sets the report names
        /// </summary>
        public string[] ReportNames { get; set; }

        /// <summary>
        /// Gets or sets the role names
        /// </summary>
        public string[] RoleNames { get; set; }

        /// <summary>
        /// Gets or sets an array of parameter constraints
        /// </summary>
        public ReportParameterConstraintConfiguration[] Constraints { get; set; }
    }
}
