namespace Reportr.Registration.Entity.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Reportr.Registration.Authorization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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
                assignment => assignment.ReportName.Equals
                (
                    reportName,
                    StringComparison.OrdinalIgnoreCase
                )
                &&
                assignment.RoleName.Equals
                (
                    roleName,
                    StringComparison.OrdinalIgnoreCase
                )
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
            return GetAssignment
            (
                assignment => assignment.Id == id
            );
        }

        /// <summary>
        /// Gets a single report role assignment from the repository
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <returns>The matching role</returns>
        public ReportRoleAssignment GetAssignment
            (
                string reportName,
                string roleName
            )
        {
            Validate.IsNotEmpty(reportName);
            Validate.IsNotEmpty(roleName);

            return GetAssignment
            (
                assignment => assignment.ReportName.Equals
                (
                    reportName,
                    StringComparison.OrdinalIgnoreCase
                )
                &&
                assignment.RoleName.Equals
                (
                    roleName,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        /// <summary>
        /// Gets a single report role assignment from the repository
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The matching role</returns>
        private ReportRoleAssignment GetAssignment
            (
                Expression<Func<ReportRoleAssignment, bool>> predicate
            )
        {
            var set = _context.Set<ReportRoleAssignment>();

            var assignment = set.FirstOrDefault
            (
                predicate
            );

            if (assignment == null)
            {
                throw new KeyNotFoundException
                (
                    "No role assignment was found matching the key specified."
                );
            }

            return assignment;
        }

        /// <summary>
        /// Gets report role assignments for any of the roles specified
        /// </summary>
        /// <param name="roleNames">The role names</param>
        /// <returns>A collection of role assignments</returns>
        public IEnumerable<ReportRoleAssignment> GetAssignmentsForRoles
            (
                params string[] roleNames
            )
        {
            Validate.IsNotNull(roleNames);

            var set = _context.Set<ReportRoleAssignment>();

            var assignments = set.Where
            (
                a => roleNames.Any
                (
                    role => a.RoleName.Equals
                    (
                        role,
                        StringComparison.OrdinalIgnoreCase
                    )
                )
            );

            return assignments.OrderBy
            (
                a => a.ReportName
            );
        }

        /// <summary>
        /// Gets report role assignments for a specific role
        /// </summary>
        /// <param name="roleName">The role name</param>
        /// <returns>A collection of role assignments</returns>
        public IEnumerable<ReportRoleAssignment> GetAssignmentsForRole
            (
                string roleName
            )
        {
            Validate.IsNotEmpty(roleName);

            var set = _context.Set<ReportRoleAssignment>();

            var assignments = set.Where
            (
                a => a.RoleName.Equals
                (
                    roleName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            return assignments.OrderBy
            (
                a => a.ReportName
            );
        }

        /// <summary>
        /// Gets report role assignments for a specific report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A collection of role assignments</returns>
        public IEnumerable<ReportRoleAssignment> GetAssignmentsForReport
            (
                string reportName
            )
        {
            Validate.IsNotEmpty(reportName);

            var set = _context.Set<ReportRoleAssignment>();

            var assignments = set.Where
            (
                a => a.ReportName.Equals
                (
                    reportName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            return assignments.OrderBy
            (
                a => a.RoleName
            );
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
                a => a.ReportName.Equals
                (
                    reportName,
                    StringComparison.OrdinalIgnoreCase
                )
                && a.RoleName.Equals
                (
                    roleName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (assignment == null)
            {
                throw new InvalidOperationException
                (
                    $"Role '{roleName}' is not assigned to '{reportName}'."
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
