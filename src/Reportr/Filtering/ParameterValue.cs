namespace Reportr.Filtering
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single report parameter value
    /// </summary>
    public class ParameterValue
    {
        private bool _lookupItemsInitialised = false;
        private KeyValuePair<object, string>[] _lookupItems = null;

        /// <summary>
        /// Constructs the parameter value with the details
        /// </summary>
        /// <param name="parameterInfo">The parameter info</param>
        /// <param name="value">The value</param>
        /// <param name="lookupParameterValues">The lookup parameter values</param>
        public ParameterValue
            (
                ParameterInfo parameterInfo,
                object value,
                params ParameterValue[] lookupParameterValues
            )
        {
            Validate.IsNotNull(parameterInfo);

            this.Parameter = parameterInfo;
            this.Name = parameterInfo.Name;

            SetValue(value, lookupParameterValues);
        }

        /// <summary>
        /// Gets the parameter information
        /// </summary>
        [JsonIgnore]
        public ParameterInfo Parameter { get; private set; }

        /// <summary>
        /// Gets an array of lookup parameter values
        /// </summary>
        /// <remarks>
        /// The lookup parameter values are used to filter the lookup 
        /// items when executing the lookup query.
        /// </remarks>
        public ParameterValue[] LookupParameterValues { get; private set; }

        /// <summary>
        /// Gets the lookup items available for the parameter value
        /// </summary>
        public KeyValuePair<object, string>[] LookupItems
        {
            get
            {
                if (false == _lookupItemsInitialised)
                {
                    InitializeLookupItems();
                }

                return _lookupItems;
            }
        }

        /// <summary>
        /// Determines if there is a lookup item matching
        /// </summary>
        /// <param name="lookupValue">The lookup item value</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        public bool HasLookupItem
            (
                object lookupValue
            )
        {
            return this.LookupItems.Any
            (
                pair => pair.Key == lookupValue
            );
        }

        /// <summary>
        /// Initializes the lookup items for the parameter value
        /// </summary>
        private void InitializeLookupItems()
        {
            var parameterInfo = this.Parameter;

            if (parameterInfo.HasLookup && parameterInfo.Visible)
            {
                var sourceType = parameterInfo.LookupSourceType.Value;

                List<KeyValuePair<object, string>> items;

                switch (sourceType)
                {
                    case ParameterLookupSourceType.Query:
                    {
                        items = ExecuteLookupQuery
                        (
                            parameterInfo
                        );

                        break;
                    }
                    case ParameterLookupSourceType.Enum:
                    {
                        items = GetLookupEnumValues
                        (
                            parameterInfo
                        );

                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException
                        (
                            $"The lookup source type {sourceType} is not supported."
                        );
                    }
                }

                if (parameterInfo.InsertBlankLookupItem)
                {
                    items.Insert
                    (
                        0,
                        new KeyValuePair<object, string>
                        (
                            String.Empty,
                            String.Empty
                        )
                    );
                }

                _lookupItems = items.ToArray();
            }

            _lookupItemsInitialised = true;
        }

        /// <summary>
        /// Executes a lookup query and returns the results as a key-value list
        /// </summary>
        /// <param name="parameterInfo">The parameter information</param>
        /// <returns>A list of key-value pairs representing the query results</returns>
        private List<KeyValuePair<object, string>> ExecuteLookupQuery
            (
                ParameterInfo parameterInfo
            )
        {
            try
            {
                var results = parameterInfo.LookupQuery.Execute
                (
                    this.LookupParameterValues
                );

                var valueBinding = parameterInfo.LookupValueBinding;
                var textBinding = parameterInfo.LookupDisplayTextBinding;
                var items = new List<KeyValuePair<object, string>>();

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

                    items.Add
                    (
                        new KeyValuePair<object, string>
                        (
                            lookupValue,
                            lookupText
                        )
                    );
                }

                return items;
            }
            catch (Exception ex)
            {
                return new List<KeyValuePair<object, string>>()
                {
                    new KeyValuePair<object, string>
                    (
                        null,
                        ex.Message
                    )
                };
            }
        }

        /// <summary>
        /// Auto filters the lookup items for a parameter constraint value
        /// </summary>
        /// <param name="constraintValue">The constraint value</param>
        protected internal void AutoFilterLookupItemsForConstraint
            (
                object constraintValue
            )
        {
            if (false == _lookupItemsInitialised)
            {
                InitializeLookupItems();
            }

            if (_lookupItems == null || _lookupItems.Length == 0)
            {
                return;
            }
            else if (constraintValue == null)
            {
                _lookupItems = new KeyValuePair<object, string>[] { };
            }
            else
            {
                var filteredItems = new Dictionary<object, string>();
                var constraintType = constraintValue.GetType();

                string GetLookupItemText(object value)
                {
                    var item = _lookupItems.First
                    (
                        pair => pair.Key == value
                    );

                    return item.Value;
                }

                void AutoAddFilteredItem(object value)
                {
                    var matchFound = HasLookupItem(constraintValue);

                    if (matchFound)
                    {
                        var itemText = GetLookupItemText(constraintValue);

                        filteredItems.Add
                        (
                            constraintValue,
                            itemText
                        );
                    }
                }

                if (constraintType.IsEnumerable())
                {
                    foreach (var value in constraintValue as IEnumerable)
                    {
                        AutoAddFilteredItem(value);
                    }
                }
                else
                {
                    AutoAddFilteredItem(constraintValue);
                }

                _lookupItems = filteredItems.ToArray();
            }
        }

        /// <summary>
        /// Gets the enum values for a parameter lookup
        /// </summary>
        /// <param name="parameterInfo">The parameter information</param>
        /// <returns>A list of key-value pairs representing the enum</returns>
        private List<KeyValuePair<object, string>> GetLookupEnumValues
            (
                ParameterInfo parameterInfo
            )
        {
            var inspector = new EnumInspector();

            var enumInfo = inspector.GetEnumInfo
            (
                parameterInfo.LookupEnumType
            );

            var pairs = new List<KeyValuePair<object, string>>();

            foreach (var item in enumInfo)
            {
                pairs.Add
                (
                    new KeyValuePair<object, string>
                    (
                        item.Name,
                        item.Description
                    )
                );
            }

            return pairs;
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
        /// Sets the parameter value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="lookupParameterValues">The lookup parameter values</param>
        protected internal void SetValue
            (
                object value,
                params ParameterValue[] lookupParameterValues
            )
        {
            Validate.IsNotNull(lookupParameterValues);

            var parameter = this.Parameter;
            
            if (value == null)
            {
                if (parameter.ValueRequired)
                {
                    throw new ArgumentException
                    (
                        $"A value for the parameter '{parameter.Name}' is required."
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

                if (false == expectedType.IsAssignableFrom(valueType))
                {
                    throw new ArgumentException
                    (
                        $"The type for the parameter {parameter.Name} is " +
                        $"{valueType} but the type {expectedType} was expected."
                    );
                }
                else
                {
                    this.Value = value;
                }
            }

            this.LookupParameterValues = lookupParameterValues;

            if (_lookupItems == null || lookupParameterValues.Any())
            {
                InitializeLookupItems();
            }
        }

        /// <summary>
        /// Resets the parameter value to its default
        /// </summary>
        protected internal void ResetValue()
        {
            this.Value = this.Parameter.DefaultValue;
        }
    }
}
