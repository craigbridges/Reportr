namespace Reportr.Filtering
{
    using System;
    
    /// <summary>
    /// Represents a single report parameter value
    /// </summary>
    public class ParameterValue
    {
        /// <summary>
        /// Constructs the parameter value with the details
        /// </summary>
        /// <param name="parameterInfo">The parameter info</param>
        /// <param name="value">The value</param>
        public ParameterValue
            (
                ParameterInfo parameterInfo,
                object value
            )
        {
            Validate.IsNotNull(parameterInfo);

            this.Parameter = parameterInfo;
            this.Name = parameterInfo.Name;

            SetValue(value);
        }

        /// <summary>
        /// Gets the parameter information
        /// </summary>
        public ParameterInfo Parameter { get; private set; }

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameter value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Sets the parameter value
        /// </summary>
        /// <param name="value">The value</param>
        protected internal void SetValue
            (
                object value
            )
        {
            var parameter = this.Parameter;

            if (value == null)
            {
                if (parameter.ValueRequired)
                {
                    var message = "A value for the parameter '{0}' is required.";

                    throw new ArgumentException
                    (
                        String.Format
                        (
                            message,
                            parameter.Name
                        )
                    );
                }
                else
                {
                    this.Value = null;
                }
            }
            else
            {
                var valueType = value.GetType();
                var expectedType = parameter.ExpectedType;

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
                    this.Value = value;
                }
            }
        }
    }
}
