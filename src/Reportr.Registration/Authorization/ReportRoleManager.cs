﻿namespace Reportr.Registration.Authorization
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
        /// <param name="configuration">The role configuration</param>
        /// <returns>The role created</returns>
        public ReportRole CreateRole
            (
                ReportRoleConfiguration configuration
            )
        {
            var role = new ReportRole
            (
                configuration
            );

            _roleRepository.AddRole(role);
            _unitOfWork.SaveChanges();

            return role;
        }

        /// <summary>
        /// Auto creates multiple report roles
        /// </summary>
        /// <param name="configurations">The role configurations</param>
        public void AutoCreateRoles
            (
                params ReportRoleConfiguration[] configurations
            )
        {
            Validate.IsNotNull(configurations);

            var changesMade = false;

            foreach (var configuration in configurations)
            {
                var exists = _roleRepository.RoleExists
                (
                    configuration.Name
                );

                if (false == exists)
                {
                    var role = new ReportRole
                    (
                        configuration
                    );

                    _roleRepository.AddRole(role);

                    changesMade = true;
                }
            }

            if (changesMade)
            {
                _unitOfWork.SaveChanges();
            }
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
        /// <param name="roleName">The role name</param>
        /// <param name="configuration">The role configuration</param>
        public void ConfigureRole
            (
                string roleName,
                ReportRoleConfiguration configuration
            )
        {
            var role = _roleRepository.GetRole
            (
                roleName
            );

            role.Configure
            (
                configuration
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
        /// <param name="constraints">The parameter constraints</param>
        public void AssignRoleToReport
            (
                string reportName,
                string roleName,
                params ReportParameterConstraintConfiguration[] constraints
            )
        {
            Validate.IsNotNull(constraints);

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
                roleName,
                constraints
            );

            _assignmentRepository.AddAssignment(assignment);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Auto assigns multiple roles to multiple reports
        /// </summary>
        /// <param name="assignments">The assignment configurations</param>
        public void AutoAssignRolesToReports
            (
                params ReportRoleAssignmentConfiguration[] assignments
            )
        {
            Validate.IsNotNull(assignments);

            var changesMade = false;

            foreach (var configuration in assignments)
            {
                var reportName = configuration.ReportName;
                var roleName = configuration.RoleName;
                var constraints = configuration.Constraints;

                var assigned = _assignmentRepository.IsRoleAssigned
                (
                    reportName,
                    roleName
                );

                if (false == assigned)
                {
                    var assignment = new ReportRoleAssignment
                    (
                        reportName,
                        roleName,
                        constraints
                    );

                    _assignmentRepository.AddAssignment
                    (
                        assignment
                    );

                    changesMade = true;
                }
            }

            if (changesMade)
            {
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Sets the report parameter constraints for a role assignment
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <param name="constraints">The parameter constraints</param>
        public void SetAssignedRoleConstraints
            (
                string reportName,
                string roleName,
                params ReportParameterConstraintConfiguration[] constraints
            )
        {
            Validate.IsNotNull(constraints);

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

            var assignment = _assignmentRepository.GetAssignment
            (
                reportName,
                roleName
            );

            assignment.SetParameterConstraints
            (
                constraints
            );

            _assignmentRepository.UpdateAssignment(assignment);
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
