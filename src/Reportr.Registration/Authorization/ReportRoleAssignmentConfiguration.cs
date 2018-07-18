namespace Reportr.Registration.Authorization
{
    /// <summary>
    /// Represents configuration details for a report role assignment
    /// </summary>
    public class ReportRoleAssignmentConfiguration
    {
        /// <summary>
        /// Gets or sets the report name
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// Gets or sets the role name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets an array of parameter constraints
        /// </summary>
        public ReportParameterConstraintConfiguration[] Constraints { get; set; }
    }
}
