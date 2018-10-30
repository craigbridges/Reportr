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
        public virtual ReportRoleAssignment Assignment { get; protected set; }
        
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
        /// Gets the report parameter mapping value as a string
        /// </summary>
        public string MappingValue { get; protected set; }

        /// <summary>
        /// Gets the report parameter mapping value type name
        /// </summary>
        public string MappingValueTypeName { get; protected set; }

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

            if (configuration.MappingValue == null)
            {
                throw new ArgumentException
                (
                    "The constraint mapping value is required."
                );
            }

            var mappingValue = configuration.MappingValue;

            this.ParameterName = configuration.ParameterName;
            this.MappingType = configuration.MappingType;
            this.MappingValue = mappingValue.ToString();
            this.MappingValueTypeName = mappingValue.GetType().AssemblyQualifiedName;
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
                var rawValue = this.MappingValue;

                var expectedType = Type.GetType
                (
                    this.MappingValueTypeName
                );

                return ObjectConverter.Convert
                (
                    rawValue,
                    expectedType
                );
            }
            else
            {
                var metaKey = this.MappingValue;

                if (String.IsNullOrEmpty(metaKey))
                {
                    throw new InvalidOperationException
                    (
                        "The constraints meta data key has not been defined."
                    );
                }

                if (userInfo.MetaData == null)
                {
                    throw new InvalidOperationException
                    (
                        "The user meta data has not been defined."
                    );
                }

                if (false == userInfo.MetaData.ContainsKey(metaKey))
                {
                    throw new KeyNotFoundException
                    (
                        $"No user meta data was found matching the key '{metaKey}'."
                    );
                }

                return userInfo.MetaData[metaKey];
            }
        }
    }
}
