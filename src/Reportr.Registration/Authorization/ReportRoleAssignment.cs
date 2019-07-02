namespace Reportr.Registration.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Represents a single report role assignment
    /// </summary>
    public class ReportRoleAssignment : IAggregate
    {
        /// <summary>
        /// Constructs the report role with its default configuration
        /// </summary>
        protected ReportRoleAssignment() { }

        /// <summary>
        /// Constructs the report role assignment with the details
        /// </summary>
        /// <param name="reportName">The report name</param>
        /// <param name="roleName">The role name</param>
        public ReportRoleAssignment
            (
                string reportName,
                string roleName,
                params ReportParameterConstraintConfiguration[] constraints
            )
        {
            Validate.IsNotNull(reportName);
            Validate.IsNotNull(roleName);
            Validate.IsNotNull(constraints);

            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.ParameterConstraints = new Collection<ReportParameterConstraint>();

            this.ReportName = reportName;
            this.RoleName = roleName;

            SetParameterConstraints(constraints);
        }

        /// <summary>
        /// Gets the unique ID of the report role
        /// </summary>
        [Key]
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the version number of the report role assignment
        /// </summary>
        public int Version { get; protected set; }

        /// <summary>
        /// Gets the date and time the report role was created
        /// </summary>
        public DateTime DateCreated { get; protected set; }

        /// <summary>
        /// Gets the date and time the report role was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the name of the registered report
        /// </summary>
        public string ReportName { get; protected set; }

        /// <summary>
        /// Gets the name of the report role
        /// </summary>
        public string RoleName { get; protected set; }

        /// <summary>
        /// Gets a collection of report parameter constraints
        /// </summary>
        public virtual ICollection<ReportParameterConstraint> ParameterConstraints
        {
            get;
            protected set;
        }

        /// <summary>
        /// Sets parameter value constraints against the role assignment
        /// </summary>
        /// <param name="configurations">The constraint configurations</param>
        public void SetParameterConstraints
            (
                params ReportParameterConstraintConfiguration[] configurations
            )
        {
            Validate.IsNotNull(configurations);

            this.ParameterConstraints.Clear();

            foreach (var configuration in configurations)
            {
                SetParameterConstraint(configuration);
            }

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }

        /// <summary>
        /// Sets a parameter value constraint against the role assignment
        /// </summary>
        /// <param name="configuration">The constraint configuration</param>
        protected virtual void SetParameterConstraint
            (
                ReportParameterConstraintConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            var parameterName = configuration.ParameterName;

            var constraint = this.ParameterConstraints.FirstOrDefault
            (
                m => m.ParameterName.ToLower() == parameterName.ToLower()
            );

            if (constraint == null)
            {
                constraint = new ReportParameterConstraint
                (
                    this,
                    configuration
                );

                this.ParameterConstraints.Add(constraint);
            }
            else
            {
                constraint.Configure(configuration);
            }
        }

        /// <summary>
        /// Gets a single parameter constraint from the role assignment
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>The matching parameter mapping</returns>
        public ReportParameterConstraint GetParameterConstraint
            (
                string parameterName
            )
        {
            Validate.IsNotEmpty(parameterName);

            var constraint = this.ParameterConstraints.FirstOrDefault
            (
                m => m.ParameterName.ToLower() == parameterName.ToLower()
            );

            if (constraint == null)
            {
                throw new KeyNotFoundException
                (
                    $"No parameter constraint was found for '{parameterName}'."
                );
            }

            return constraint;
        }

        /// <summary>
        /// Removes a single parameter constraint
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        public void RemoveParameterConstraint
            (
                string parameterName
            )
        {
            Validate.IsNotEmpty(parameterName);

            var constraint = GetParameterConstraint
            (
                parameterName
            );

            this.ParameterConstraints.Remove
            (
                constraint
            );

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }
    }
}
