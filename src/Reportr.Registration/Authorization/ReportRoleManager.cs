namespace Reportr.Registration.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// Auto registers multiple report roles
        /// </summary>
        /// <param name="configurations">The role configurations</param>
        public void AutoRegisterRoles
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

                if (exists)
                {
                    var role = _roleRepository.GetRole
                    (
                        configuration.Name
                    );

                    role.Configure(configuration);

                    _roleRepository.UpdateRole(role);

                    changesMade = true;
                }
                else
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
        /// Gets a collection of role assignments for a report
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <returns>A collection of report role assignments</returns>
        public IEnumerable<ReportRoleAssignment> GetRoleAssignments
            (
                string reportName
            )
        {
            return _assignmentRepository.GetAssignmentsForReport
            (
                reportName
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
                throw new InvalidOperationException
                (
                    $"The role '{roleName}' has already been assigned to '{reportName}'."
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
                var reportNames = configuration.ReportNames;
                var roleNames = configuration.RoleNames;

                if (reportNames == null || reportNames.Length == 0)
                {
                    throw new InvalidOperationException
                    (
                        "At least one report name must be specified for assignments."
                    );
                }

                if (roleNames == null || roleNames.Length == 0)
                {
                    throw new InvalidOperationException
                    (
                        "At least one role name must be specified for assignments."
                    );
                }

                foreach (var reportName in reportNames)
                {
                    foreach (var roleName in roleNames)
                    {
                        var constraints = configuration.Constraints;

                        var assigned = _assignmentRepository.IsRoleAssigned
                        (
                            reportName,
                            roleName
                        );

                        if (assigned)
                        {
                            var assignment = _assignmentRepository.GetAssignment
                            (
                                reportName,
                                roleName
                            );

                            assignment.SetParameterConstraints
                            (
                                constraints
                            );

                            _assignmentRepository.UpdateAssignment
                            (
                                assignment
                            );

                            changesMade = true;
                        }
                        else
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
                throw new InvalidOperationException
                (
                    $"The role '{roleName}' has not been assigned to '{reportName}'."
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
        /// Gets parameter constraints for a report role assignment
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        /// <returns>A collection of parameter constraints</returns>
        public IEnumerable<ReportParameterConstraint> GetAssignmentConstraints
            (
                string reportName,
                string roleName
            )
        {
            var assignment = _assignmentRepository.GetAssignment
            (
                reportName,
                roleName
            );

            return assignment.ParameterConstraints.ToList();
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
                throw new InvalidOperationException
                (
                    $"The role '{roleName}' has not been assigned to '{reportName}'."
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
