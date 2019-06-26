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

            SetValue
            (
                value,
                false,
                lookupParameterValues
            );
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
        /// Finds a lookup item matching a specific value
        /// </summary>
        /// <param name="lookupValue">The lookup item value</param>
        /// <returns>The matching item, if found; otherwise null</returns>
        protected internal KeyValuePair<object, string>? FindLookupItem
            (
                object lookupValue
            )
        {
            var lookupItems = this.LookupItems;
            IEnumerable<KeyValuePair<object, string>> matches;

            if (lookupValue == null)
            {
                matches = lookupItems.Where
                (
                    pair => pair.Key == null
                );
            }
            else
            {
                matches = lookupItems.Where
                (
                    pair => pair.Key != null
                        && pair.Key.ToString() == lookupValue.ToString()
                );
            }

            if (matches.Any())
            {
                return matches.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines if there is a lookup item matching a specific value
        /// </summary>
        /// <param name="lookupValue">The lookup item value</param>
        /// <returns>True, if a match was found; otherwise false</returns>
        public bool HasLookupItem
            (
                object lookupValue
            )
        {
            var matchingItem = FindLookupItem(lookupValue);

            return matchingItem != null;
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
                var parameterValues = 
                (
                    this.LookupParameterValues ?? new ParameterValue[] { }
                );

                var results = parameterInfo.LookupQuery.Execute
                (
                    parameterValues
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

                void AutoAddFilteredItem(object value)
                {
                    var matchingItem = FindLookupItem(value);

                    if (matchingItem != null)
                    {
                        var itemText = matchingItem.Value.Value;

                        if (false == filteredItems.ContainsKey(value))
                        {
                            filteredItems.Add
                            (
                                value,
                                itemText
                            );
                        }
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
        /// Gets a flag indicating if the parameter value was automatically 
        /// set by a parameter constraint rule.
        /// </summary>
        /// <remarks>
        /// If the value was automatically set by a constraint, then this 
        /// should be treated as a null parameter value when executing queries.
        /// </remarks>
        public bool ValueAutoSetByConstraint { get; private set; }

        /// <summary>
        /// Sets the parameter value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="autoSetByConstraint">Auto set by a constraint?</param>
        /// <param name="lookupParameterValues">The lookup parameter values</param>
        protected internal void SetValue
            (
                object value,
                bool autoSetByConstraint = false,
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
            
            var currentLookupParameters = this.LookupParameterValues;

            this.ValueAutoSetByConstraint = autoSetByConstraint;
            this.LookupParameterValues = lookupParameterValues;

            if (false == autoSetByConstraint)
            {
                var lookupParameterValuesChanged = false;

                if (currentLookupParameters == null || currentLookupParameters.Length == 0)
                {
                    if (lookupParameterValues.Length > 0)
                    {
                        lookupParameterValuesChanged = true;
                    }
                }
                else
                {
                    var isEqual = Enumerable.SequenceEqual
                    (
                        currentLookupParameters,
                        lookupParameterValues
                    );

                    if (false == isEqual)
                    {
                        lookupParameterValuesChanged = true;
                    }
                }

                if (false == _lookupItemsInitialised || lookupParameterValuesChanged)
                {
                    InitializeLookupItems();
                }
            }
        }

        /// <summary>
        /// Resets the parameter value to its default
        /// </summary>
        protected internal void ResetValue()
        {
            this.Value = this.Parameter.DefaultValue;
        }

        /// <summary>
        /// Generates a custom hash code for the unit
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            var tuple = Tuple.Create
            (
                this.Parameter,
                this.Name,
                this.Value,
                this.ValueAutoSetByConstraint,
                this.LookupItems
            );

            return tuple.GetHashCode();
        }

        /// <summary>
        /// Determines if an object is equal to the current parameter value
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public override bool Equals
            (
                object obj
            )
        {
            if (obj is null || false == (obj is ParameterValue))
            {
                return false;
            }
            else
            {
                var otherParam = (ParameterValue)obj;

                var nameMatches = otherParam.Name.Equals
                (
                    this.Name,
                    StringComparison.OrdinalIgnoreCase
                );

                bool valueMatches;

                if (otherParam.Value == null && this.Value == null)
                {
                    valueMatches = true;
                }
                else
                {
                    valueMatches = (otherParam.Value == this.Value);
                }

                return (nameMatches && valueMatches);
            }
        }

        /// <summary>
        /// Compares two parameter value instances to determine if they are equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public static bool operator ==(ParameterValue left, ParameterValue right)
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
        /// Compares two parameter value instances to determine if they are not equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are not equal; otherwise false</returns>
        public static bool operator !=(ParameterValue left, ParameterValue right)
        {
            if (left is null)
            {
                return false == (right is null);
            }
            else
            {
                return false == left.Equals(right);
            }
        }

        /// <summary>
        /// Provides a descriptor for the objects current state
        /// </summary>
        /// <returns>The name and value of the parameter</returns>
        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
