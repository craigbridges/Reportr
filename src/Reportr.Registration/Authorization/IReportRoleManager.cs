namespace Reportr.Registration.Authorization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Defines a contract for a service that manages report roles
    /// </summary>
    public interface IReportRoleManager
    {
        // TODO: CRUD assignments

        
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


    }
}
