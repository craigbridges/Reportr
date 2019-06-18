namespace Reportr.Registration.Authorization
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a repository that manages report roles
    /// </summary>
    public interface IReportRoleRepository
    {
        /// <summary>
        /// Adds a single report role to the repository
        /// </summary>
        /// <param name="role">The role to add</param>
        void AddRole
        (
            ReportRole role
        );

        /// <summary>
        /// Determines if a role exists with the name specified
        /// </summary>
        /// <param name="name">The role name</param>
        /// <returns>True, if the role exists; otherwise false</returns>
        bool RoleExists
        (
            string name
        );

        /// <summary>
        /// Gets a single report role from the repository
        /// </summary>
        /// <param name="name">The name of the role to get</param>
        /// <returns>The matching role</returns>
        ReportRole GetRole
        (
            string name
        );

        /// <summary>
        /// Gets all report roles in the repository
        /// </summary>
        /// <returns>A collection of roles</returns>
        IEnumerable<ReportRole> GetAllRoles();

        /// <summary>
        /// Updates a single report role to the repository
        /// </summary>
        /// <param name="role">The role to update</param>
        void UpdateRole
        (
            ReportRole role
        );

        /// <summary>
        /// Removes a single report role from the repository
        /// </summary>
        /// <param name="name">The name of the role to remove</param>
        void RemoveRole
        (
            string name
        );
    }
}
