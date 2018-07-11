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
        /// <param name="name">The role name</param>
        /// <param name="title">The role title</param>
        /// <param name="description">The role description</param>
        /// <returns>The role created</returns>
        ReportRole CreateRole
        (
            string name,
            string title,
            string description
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
        /// <param name="currentName">The current role name</param>
        /// <param name="newName">The new role name</param>
        /// <param name="title">The new role title</param>
        /// <param name="description">The new role description</param>
        void ConfigureRole
        (
            string currentName,
            string newName,
            string title,
            string description
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
        void AssignRoleToReport
        (
            string reportName,
            string roleName
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
