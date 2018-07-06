namespace Reportr.Registration.Entity.Repositories
{
    using Reportr.Registration.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    
    /// <summary>
    /// Represents an Entity Framework registered report role repository
    /// </summary>
    public sealed class EfReportRoleRepository : IReportRoleRepository
    {
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfReportRoleRepository
            (
                ReportrDbContext context
            )
        {
            Validate.IsNotNull(context);

            _context = context;
        }

        /// <summary>
        /// Adds a single report role to the repository
        /// </summary>
        /// <param name="role">The role to add</param>
        public void AddRole
            (
                ReportRole role
            )
        {
            Validate.IsNotNull(role);

            _context.Set<ReportRole>().Add
            (
                role
            );
        }

        /// <summary>
        /// Gets a single report role from the repository
        /// </summary>
        /// <param name="name">The name of the role to get</param>
        /// <returns>The matching role</returns>
        public ReportRole GetRole
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var set = _context.Set<ReportRole>();

            var role = set.FirstOrDefault
            (
                r => r.Name.ToLower() == name.ToLower()
            );

            if (role == null)
            {
                var message = "No role was found matching the name '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        name.ToString()
                    )
                );
            }

            return role;
        }

        /// <summary>
        /// Gets all report roles in the repository
        /// </summary>
        /// <returns>A collection of roles</returns>
        public IEnumerable<ReportRole> GetAllRoles()
        {
            var roles = _context.Set<ReportRole>();

            return roles.OrderBy
            (
                a => a.Name
            );
        }
        
        /// <summary>
        /// Updates a single report role to the repository
        /// </summary>
        /// <param name="role">The role to update</param>
        public void UpdateRole
            (
                ReportRole role
            )
        {
            Validate.IsNotNull(role);

            var entry = _context.Entry<ReportRole>
            (
                role
            );

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Removes a single report role from the repository
        /// </summary>
        /// <param name="name">The name of the role to remove</param>
        public void RemoveRole
            (
                string name
            )
        {
            var role = GetRole(name);

            var entry = _context.Entry<ReportRole>
            (
                role
            );

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<ReportRole>().Attach
                (
                    role
                );
            }

            _context.Set<ReportRole>().Remove
            (
                role
            );
        }
    }
}
