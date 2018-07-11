namespace Reportr.Registration.Entity.Repositories
{
    using Reportr.Registration.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    
    /// <summary>
    /// Represents an Entity Framework report role assignment repository
    /// </summary>
    public sealed class EfReportRoleAssignmentRepository : IReportRoleAssignmentRepository
    {
        private readonly ReportrDbContext _context;

        /// <summary>
        /// Constructs the repository with a database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EfReportRoleAssignmentRepository
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
        /// <param name="assignment">The assignment to add</param>
        public void AddAssignment
            (
                ReportRoleAssignment assignment
            )
        {
            Validate.IsNotNull(assignment);

            _context.Set<ReportRoleAssignment>().Add
            (
                assignment
            );
        }

        /// <summary>
        /// Determines if a role has been assigned to a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <returns>True, if the role has been assigned; otherwise false</returns>
        public bool IsRoleAssigned
            (
                string reportName,
                string roleName
            )
        {
            Validate.IsNotEmpty(reportName);
            Validate.IsNotEmpty(roleName);

            var set = _context.Set<ReportRoleAssignment>();

            return set.Any
            (
                assignment => assignment.ReportName.ToLower() == reportName.ToLower() 
                    && assignment.RoleName.ToLower() == roleName.ToLower()
            );
        }

        /// <summary>
        /// Gets a single report role assignment from the repository
        /// </summary>
        /// <param name="id">The ID of the assignment to get</param>
        /// <returns>The matching role</returns>
        public ReportRoleAssignment GetAssignment
            (
                Guid id
            )
        {
            var set = _context.Set<ReportRoleAssignment>();

            var assignment = set.FirstOrDefault
            (
                r => r.Id == id
            );

            if (assignment == null)
            {
                var message = "No role assignment was found matching the ID '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        id.ToString()
                    )
                );
            }

            return assignment;
        }

        /// <summary>
        /// Gets all report role assignments in the repository
        /// </summary>
        /// <returns>A collection of role assignments</returns>
        public IEnumerable<ReportRoleAssignment> GetAllAssignments()
        {
            var assignments = _context.Set<ReportRoleAssignment>();

            return assignments.OrderByDescending
            (
                a => a.DateCreated
            );
        }

        /// <summary>
        /// Updates a single report role assignment to the repository
        /// </summary>
        /// <param name="assignment">The assignment to update</param>
        public void UpdateAssignment
            (
                ReportRoleAssignment assignment
            )
        {
            Validate.IsNotNull(assignment);

            var entry = _context.Entry<ReportRoleAssignment>
            (
                assignment
            );

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Removes a single report role assignment from the repository
        /// </summary>
        /// <param name="ID">The ID of the assignment to remove</param>
        public void RemoveAssignment
            (
                Guid id
            )
        {
            var assignment = GetAssignment(id);

            RemoveAssignment(assignment);
        }

        /// <summary>
        /// Removes a single report role assignment from the repository
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        public void RemoveAssignment
            (
                string reportName,
                string roleName
            )
        {
            var set = _context.Set<ReportRoleAssignment>();

            var assignment = set.FirstOrDefault
            (
                a => a.ReportName.ToLower() == reportName.ToLower()
                    && a.RoleName.ToLower() == roleName.ToLower()
            );

            if (assignment == null)
            {
                var message = "The role '{0}' has not been assigned to '{1}'.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        roleName,
                        reportName
                    )
                );
            }

            RemoveAssignment(assignment);
        }

        /// <summary>
        /// Removes a single report role assignment from the repository
        /// </summary>
        /// <param name="assignment">The assignment</param>
        private void RemoveAssignment
            (
                ReportRoleAssignment assignment
            )
        {
            var entry = _context.Entry<ReportRoleAssignment>
            (
                assignment
            );

            // Ensure the entity has been attached to the object state manager
            if (entry.State == EntityState.Detached)
            {
                _context.Set<ReportRoleAssignment>().Attach
                (
                    assignment
                );
            }

            _context.Set<ReportRoleAssignment>().Remove
            (
                assignment
            );
        }
    }
}
