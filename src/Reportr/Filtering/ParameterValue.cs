namespace Reportr.Filtering
{
    using System;
    using System.Collections.Generic;

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
            
            if (parameterInfo.HasLookup)
            {
                var results = parameterInfo.LookupQuery.Execute();
                var lookupItems = new List<KeyValuePair<object, string>>();

                var valueBinding = parameterInfo.LookupValueBinding;
                var textBinding = parameterInfo.LookupDisplayTextBinding;

                foreach (var row in results.AllRows)
                {
                    var lookupValue = valueBinding.Resolve
                    (
                        row
                    );

                    var lookupText = textBinding.Resolve<string>
                    (
                        row
                    );

                    lookupItems.Add
                    (
                        new KeyValuePair<object, string>
                        (
                            lookupValue,
                            lookupText
                        )
                    );
                }

                this.LookupItems = lookupItems.ToArray();
            }

            SetValue(value);
        }

        /// <summary>
        /// Gets the parameter information
        /// </summary>
        public ParameterInfo Parameter { get; private set; }

        /// <summary>
        /// Gets the lookup items available for the parameter value
        /// </summary>
        public KeyValuePair<object, string>[] LookupItems { get; private set; }

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
