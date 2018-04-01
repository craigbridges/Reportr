namespace Reportr
{
    using System;
    
    /// <summary>
    /// Represents information about a single parameter
    /// </summary>
    public class ParameterInfo
    {
        /// <summary>
        /// Constructs the parameter info with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="isRequired">True, if the parameter is required</param>
        /// <param name="defaultValue">The default value (optional)</param>
        public ParameterInfo
            (
                string name,
                string description,
                bool isRequired,
                object defaultValue = null
            )
        {
            Validate.IsNotEmpty(name);

            this.Name = name;
            this.Description = description;
            this.IsRequired = isRequired;
            this.DefaultValue = defaultValue;
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
        /// Gets the parameters default value
        /// </summary>
        public object DefaultValue { get; private set; }
    }
}
