namespace Reportr.Registration.Authorization
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents the default report role manager implementation
    /// </summary>
    public sealed class ReportRoleManager : IReportRoleManager
    {
        private readonly IReportRoleRepository _roleRepository;
        private readonly IReportRoleAssignmentRepository _assignmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructs the report role manager with required dependencies
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="assignmentRepository">The assignment repository</param>
        /// <param name="unitOfWork">The unit of work</param>
        public ReportRoleManager
            (
                IReportRoleRepository roleRepository,
                IReportRoleAssignmentRepository assignmentRepository,
                IUnitOfWork unitOfWork
            )
        {
            Validate.IsNotNull(roleRepository);
            Validate.IsNotNull(assignmentRepository);
            Validate.IsNotNull(unitOfWork);

            _roleRepository = roleRepository;
            _assignmentRepository = assignmentRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new report role
        /// </summary>
        /// <param name="name">The role name</param>
        /// <param name="title">The role title</param>
        /// <param name="description">The role description</param>
        /// <returns>The role created</returns>
        public ReportRole CreateRole
            (
                string name,
                string title,
                string description
            )
        {
            var role = new ReportRole
            (
                name,
                title,
                description
            );

            _roleRepository.AddRole(role);
            _unitOfWork.SaveChanges();

            return role;
        }

        /// <summary>
        /// Gets a single report role
        /// </summary>
        /// <param name="name">The role name</param>
        /// <returns></returns>
        public ReportRole GetRole
            (
                string name
            )
        {
            return _roleRepository.GetRole
            (
                name
            );
        }

        /// <summary>
        /// Gets all report roles
        /// </summary>
        /// <returns>A collection of report roles</returns>
        public IEnumerable<ReportRole> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        /// <summary>
        /// Configures a single report role
        /// </summary>
        /// <param name="currentName">The current role name</param>
        /// <param name="newName">The new role name</param>
        /// <param name="title">The new role title</param>
        /// <param name="description">The new role description</param>
        public void ConfigureRole
            (
                string currentName,
                string newName,
                string title,
                string description
            )
        {
            var role = _roleRepository.GetRole
            (
                currentName
            );

            role.Configure
            (
                newName,
                title,
                description
            );

            _roleRepository.UpdateRole(role);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Deletes a single report role
        /// </summary>
        /// <param name="name">The name of the role to delete</param>
        public void DeleteRole
            (
                string name
            )
        {
            _roleRepository.RemoveRole(name);
            _unitOfWork.SaveChanges();
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
            return _assignmentRepository.IsRoleAssigned
            (
                reportName,
                roleName
            );
        }

        /// <summary>
        /// Assigns a role to a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        public void AssignRoleToReport
            (
                string reportName,
                string roleName
            )
        {
            var assigned = _assignmentRepository.IsRoleAssigned
            (
                reportName,
                roleName
            );

            if (assigned)
            {
                var message = "The role '{0}' has already been assigned to '{1}'.";

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

            var assignment = new ReportRoleAssignment
            (
                reportName,
                roleName
            );

            _assignmentRepository.AddAssignment(assignment);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Unassigns a role from a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        public void UnassignRoleFromReport
            (
                string reportName,
                string roleName
            )
        {
            var assigned = _assignmentRepository.IsRoleAssigned
            (
                reportName,
                roleName
            );

            if (false == assigned)
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

            _assignmentRepository.RemoveAssignment
            (
                reportName,
                roleName
            );

            _unitOfWork.SaveChanges();
        }
    }
}
