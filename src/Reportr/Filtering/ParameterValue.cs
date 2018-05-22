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

            this.Name = parameterInfo.Name;

            SetValue
            (
                parameterInfo,
                value
            );
        }

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameter value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// validates, then sets the parameter value
        /// </summary>
        /// <param name="parameterInfo">The parameter info</param>
        /// <param name="value">The value</param>
        private void SetValue
            (
                ParameterInfo parameterInfo,
                object value
            )
        {
            if (value == null)
            {
                if (parameterInfo.ValueRequired)
                {
                    var message = "A value for the parameter '{0}' is required.";

                    throw new ArgumentException
                    (
                        String.Format
                        (
                            message,
                            parameterInfo.Name
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
                var expectedType = parameterInfo.ExpectedType;

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
