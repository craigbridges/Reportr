namespace Reportr.Filtering
{
    using Reportr.Data;
    using Reportr.Data.Querying;
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
        /// <param name="displayText">The display text</param>
        /// <param name="expectedType">The expected value type</param>
        /// <param name="description">The description</param>
        public ParameterInfo
            (
                string name,
                string displayText,
                Type expectedType = null,
                string description = null
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotEmpty(displayText);

            if (expectedType == null)
            {
                expectedType = typeof(string);
            }

            this.Name = name;
            this.DisplayText = displayText;
            this.ExpectedType = expectedType;
            this.Description = description;
            this.Visible = true;
            this.ValueRequired = false;
        }

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameters display text
        /// </summary>
        public string DisplayText { get; private set; }

        /// <summary>
        /// Gets a description of the parameter
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the expected value type
        /// </summary>
        public Type ExpectedType { get; private set; }

        /// <summary>
        /// Adds configuration details to the parameter info
        /// </summary>
        /// <param name="valueRequired">True, if a value is required</param>
        /// <param name="visible">True, if the parameter is visible</param>
        /// <returns>The updated parameter info</returns>
        public ParameterInfo WithConfiguration
            (
                bool valueRequired,
                bool visible
            )
        {
            this.ValueRequired = valueRequired;
            this.Visible = visible;

            return this;
        }

        /// <summary>
        /// Gets a flag indicating if a value is required
        /// </summary>
        public bool ValueRequired { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the parameter should be visible in the UI
        /// </summary>
        /// <remarks>
        /// If this property is set to false, the parameter must
        /// be populated programmatically or at design time.
        /// </remarks>
        public bool Visible { get; internal set; }

        /// <summary>
        /// Adds a lookup to the parameter info
        /// </summary>
        /// <param name="query">The lookup query</param>
        /// <param name="valueBinding">The value data binding</param>
        /// <param name="displayTextBinding">The display text data binding</param>
        /// <param name="filterParameters">The filter parameters</param>
        /// <returns>The updated parameter info</returns>
        public ParameterInfo WithLookup
            (
                IQuery query,
                DataBinding valueBinding,
                DataBinding displayTextBinding,
                params ParameterInfo[] filterParameters
            )
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(valueBinding);
            Validate.IsNotNull(displayTextBinding);
            Validate.IsNotNull(filterParameters);

            this.HasLookup = true;
            this.LookupQuery = query;
            this.LookupValueBinding = valueBinding;
            this.LookupDisplayTextBinding = displayTextBinding;
            this.LookupFilterParameters = filterParameters;

            return this;
        }

        /// <summary>
        /// Gets a flag indicating if the parameter info has a lookup
        /// </summary>
        public bool HasLookup { get; private set; }

        /// <summary>
        /// Gets the lookup query
        /// </summary>
        public IQuery LookupQuery { get; private set; }

        /// <summary>
        /// Gets the lookup value binding
        /// </summary>
        public DataBinding LookupValueBinding { get; private set; }

        /// <summary>
        /// Gets the lookup display text binding
        /// </summary>
        public DataBinding LookupDisplayTextBinding { get; private set; }

        /// <summary>
        /// Gets the lookup filter parameters
        /// </summary>
        public ParameterInfo[] LookupFilterParameters { get; private set; }

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

        /// <summary>
        /// Gets the parameters default value
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Generates a custom hash code for the parameter info
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            var tuple = Tuple.Create
            (
                this.Name,
                this.DisplayText,
                this.Description,
                this.ExpectedType,
                this.ValueRequired,
                this.Visible,
                this.DefaultValue
            );

            return tuple.GetHashCode();
        }

        /// <summary>
        /// Determines if an object is equal to the current parameter info instance
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public override bool Equals
            (
                object obj
            )
        {
            if (obj is null || false == (obj is ParameterInfo))
            {
                return false;
            }

            return obj.GetHashCode() == this.GetHashCode();
        }

        /// <summary>
        /// Compares two parameter info instances to determine if they are equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public static bool operator ==
            (
                ParameterInfo left,
                ParameterInfo right
            )
        {
            if (left is null)
            {
                return right is null;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// Compares two parameter info instances to determine if they are not equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are not equal; otherwise false</returns>
        public static bool operator !=
            (
                ParameterInfo left,
                ParameterInfo right
            )
        {
            if (left is null && false == right is null)
            {
                return true;
            }
            else if (false == left is null && right is null)
            {
                return true;
            }
            else if (left is null && right is null)
            {
                return false;
            }
            else
            {
                return false == left.Equals(right);
            }
        }
    }
}
