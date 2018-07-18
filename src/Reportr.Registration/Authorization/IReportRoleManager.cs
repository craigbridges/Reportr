namespace Reportr.Registration.Authorization
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a service that manages report roles
    /// </summary>
    public interface IReportRoleManager
    {
        /// <summary>
        /// Creates a new report role
        /// </summary>
        /// <param name="configuration">The role configuration</param>
        /// <returns>The role created</returns>
        ReportRole CreateRole
        (
            ReportRoleConfiguration configuration
        );

        /// <summary>
        /// Auto creates multiple report roles
        /// </summary>
        /// <param name="configurations">The role configurations</param>
        void AutoCreateRoles
        (
            params ReportRoleConfiguration[] configurations
        );

        /// <summary>
        /// Gets a single report role
        /// </summary>
        /// <param name="name">The role name</param>
        /// <returns></returns>
        ReportRole GetRole
        (
            string name
        );

        /// <summary>
        /// Gets all report roles
        /// </summary>
        /// <returns>A collection of report roles</returns>
        IEnumerable<ReportRole> GetAllRoles();

        /// <summary>
        /// Configures a single report role
        /// </summary>
        /// <param name="roleName">The role name</param>
        /// <param name="configuration">The role configuration</param>
        void ConfigureRole
        (
            string roleName,
            ReportRoleConfiguration configuration
        );

        /// <summary>
        /// Deletes a single report role
        /// </summary>
        /// <param name="name">The name of the role to delete</param>
        void DeleteRole
        (
            string name
        );

        /// <summary>
        /// Determines if a role has been assigned to a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <returns>True, if the role has been assigned; otherwise false</returns>
        bool IsRoleAssigned
        (
            string reportName,
            string roleName
        );

        /// <summary>
        /// Assigns a role to a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <param name="constraints">The parameter constraints</param>
        void AssignRoleToReport
        (
            string reportName,
            string roleName,
            params ReportParameterConstraintConfiguration[] constraints
        );

        /// <summary>
        /// Auto assigns multiple roles to multiple reports
        /// </summary>
        /// <param name="assignments">The assignment configurations</param>
        void AutoAssignRolesToReports
        (
            params ReportRoleAssignmentConfiguration[] assignments
        );

        /// <summary>
        /// Sets the report parameter constraints for a role assignment
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <param name="constraints">The parameter constraints</param>
        void SetAssignedRoleConstraints
        (
            string reportName,
            string roleName,
            params ReportParameterConstraintConfiguration[] constraints
        );

        /// <summary>
        /// Unassigns a role from a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        void UnassignRoleFromReport
        (
            string reportName,
            string roleName
        );
    }
}
