namespace Reportr.Registration.Authorization
{
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
        /// Gets a single report role assignment from the repository
        /// </summary>
        /// <param name="name">The key of the assignment to get</param>
        /// <returns>The matching role</returns>
        ReportRoleAssignment GetAssignment
        (
            string key
        );

        /// <summary>
        /// Gets all report role assignments in the repository
        /// </summary>
        /// <returns>A collection of role assignments</returns>
        IEnumerable<ReportRoleAssignment> GetAllAssignments();

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
        /// <param name="key">The key of the assignment to remove</param>
        void RemoveAssignment
        (
            string key
        );
    }
}
