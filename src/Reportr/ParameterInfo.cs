namespace Reportr
{
    using System;
    
    /// <summary>
    /// Represents information about a single report parameter
    /// </summary>
    public class ParameterInfo
    {
        /// <summary>
        /// Constructs the parameter info with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="expectedType">The expected value type</param>
        /// <param name="isRequired">True, if the parameter is required</param>
        /// <param name="description">The description</param>
        public ParameterInfo
            (
                string name,
                Type expectedType,
                bool isRequired = false,
                string description = null
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(expectedType);

            this.Name = name;
            this.ExpectedType = expectedType;
            this.IsRequired = isRequired;
            this.Description = description;
        }

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a description of the parameter
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the parameter is required
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Gets the expected value type
        /// </summary>
        public Type ExpectedType { get; private set; }

        /// <summary>
        /// Gets the parameters default value
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Adds the default value to the parameter info
        /// </summary>
        /// <param name="value">The default value</param>
        /// <returns>The updated parameter info</returns>
        public ParameterInfo WithDefault
            (
                object value
            )
        {
            if (value == null)
            {
                this.DefaultValue = null;
            }
            else
            {
                var valueType = value.GetType();
                var expectedType = this.ExpectedType;

                if (valueType != expectedType)
                {
                    var message = "The type is {0} but the type {1} was expected.";

                    throw new ArgumentException
                    (
                        String.Format
                        (
                            message,
                            valueType,
                            expectedType
                        )
                    );
                }
                else
                {
                    this.DefaultValue = value;
                }
            }

            return this;
        }
    }
}
