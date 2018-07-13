namespace Reportr.Registration.Authorization
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the constraint between a report parameter and a value
    /// </summary>
    public class ReportParameterConstraint
    {
        /// <summary>
        /// Constructs the report role with its default configuration
        /// </summary>
        protected ReportParameterConstraint() { }

        /// <summary>
        /// Constructs the report parameter constraint with the details
        /// </summary>
        /// <param name="assignment">The role assignment</param>
        /// <param name="configuration">The constraint configuration</param>
        internal ReportParameterConstraint
            (
                ReportRoleAssignment assignment,
                ReportParameterConstraintConfiguration configuration
            )
        {
            Validate.IsNotNull(assignment);

            this.Id = Guid.NewGuid();
            this.Assignment = assignment;

            Configure(configuration);
        }

        /// <summary>
        /// Gets the unique ID of the report role
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the role assignment
        /// </summary>
        public ReportRoleAssignment Assignment { get; protected set; }
        
        /// <summary>
        /// Gets the role assignment ID
        /// </summary>
        public Guid AssignmentId { get; protected set; }
        
        /// <summary>
        /// Gets the name of the report parameter
        /// </summary>
        public string ParameterName { get; protected set; }
        
        /// <summary>
        /// Gets the report parameter mapping type
        /// </summary>
        public ReportParameterMappingType MappingType { get; protected set; }

        /// <summary>
        /// Gets the report parameter mapping value
        /// </summary>
        public object MappingValue { get; protected set; }

        /// <summary>
        /// Configures the report parameter constraint
        /// </summary>
        /// <param name="configuration">The constraint configuration</param>
        internal void Configure
            (
                ReportParameterConstraintConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            if (String.IsNullOrEmpty(configuration.ParameterName))
            {
                throw new ArgumentException
                (
                    "The constraint parameter name is required."
                );
            }

            this.ParameterName = configuration.ParameterName;
            this.MappingType = configuration.MappingType;
            this.MappingValue = configuration.MappingValue;
        }

        /// <summary>
        /// Resolves the parameter constraint value for the user specified
        /// </summary>
        /// <param name="userInfo">The user information</param>
        /// <returns>The resolved value</returns>
        public object ResolveValue
            (
                ReportUserInfo userInfo
            )
        {
            Validate.IsNotNull(userInfo);

            if (this.MappingType == ReportParameterMappingType.Literal)
            {
                return this.MappingValue;
            }
            else
            {
                if (this.MappingValue == null)
                {
                    throw new InvalidOperationException
                    (
                        "The mapping value must contain a valid meta data key."
                    );
                }

                var metaKey = this.MappingValue.ToString();

                if (userInfo.MetaData == null)
                {
                    throw new InvalidOperationException
                    (
                        "The user meta data has not been defined."
                    );
                }

                if (false == userInfo.MetaData.ContainsKey(metaKey))
                {
                    var message = "No user meta data was found matching the key '{0}'.";

                    throw new KeyNotFoundException
                    (
                        String.Format
                        (
                            message,
                            metaKey
                        )
                    );
                }

                return userInfo.MetaData[metaKey];
            }
        }
    }
}
