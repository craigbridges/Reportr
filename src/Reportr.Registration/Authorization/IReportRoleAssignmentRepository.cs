namespace Reportr.Registration.Authorization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages report role assignments
    /// </summary>
    public interface IReportRoleAssignmentRepository
    {
        /// <summary>
        /// Adds a single report role to the repository
        /// </summary>
        /// <param name="assignment">The assignment to add</param>
        void AddAssignment
        (
            ReportRoleAssignment assignment
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
        /// Gets a single report role assignment from the repository
        /// </summary>
        /// <param name="id">The ID of the assignment to get</param>
        /// <returns>The matching role</returns>
        ReportRoleAssignment GetAssignment
        (
            Guid id
        );

        /// <summary>
        /// Gets a single report role assignment from the repository
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <returns>The matching role</returns>
        ReportRoleAssignment GetAssignment
        (
            string reportName,
            string roleName
        );

        /// <summary>
        /// Gets all report role assignments in the repository
        /// </summary>
        /// <returns>A collection of role assignments</returns>
        IEnumerable<ReportRoleAssignment> GetAllAssignments();

        /// <summary>
        /// Gets report role assignments for a specific role
        /// </summary>
        /// <param name="roleName">The role name</param>
        /// <returns>A collection of role assignments</returns>
        IEnumerable<ReportRoleAssignment> GetAssignmentsForRole
        (
            string roleName
        );

        /// <summary>
        /// Gets report role assignments for any of the roles specified
        /// </summary>
        /// <param name="roleNames">The role names</param>
        /// <returns>A collection of role assignments</returns>
        IEnumerable<ReportRoleAssignment> GetAssignmentsForRoles
        (
            params string[] roleNames
        );

        /// <summary>
        /// Gets report role assignments for a specific report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A collection of role assignments</returns>
        IEnumerable<ReportRoleAssignment> GetAssignmentsForReport
        (
            string reportName
        );

        /// <summary>
        /// Updates a single report role assignment to the repository
        /// </summary>
        /// <param name="assignment">The assignment to update</param>
        void UpdateAssignment
        (
            ReportRoleAssignment assignment
        );

        /// <summary>
        /// Removes a single report role assignment from the repository
        /// </summary>
        /// <param name="ID">The ID of the assignment to remove</param>
        void RemoveAssignment
        (
            Guid id
        );

        /// <summary>
        /// Removes a single report role assignment from the repository
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        void RemoveAssignment
        (
            string reportName,
            string roleName
        );
    }
}
