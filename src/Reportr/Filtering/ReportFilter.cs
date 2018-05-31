namespace Reportr.Filtering
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a single report filter
    /// </summary>
    /// <remarks>
    /// A report filter is used to filter the data in one or more
    /// sections within the report. This includes sorting columns
    /// within queries that have been defined in sections.
    /// </remarks>
    public class ReportFilter
    {
        /// <summary>
        /// Constructs an empty report filter
        /// </summary>
        public ReportFilter()
        {
            this.ParameterDefinitions = new Collection<ReportParameterDefinition>();
            this.ParameterValues = new Collection<ReportFilterParameterValue>();
            this.SortingRules = new Collection<ReportFilterSortingRule>();
        }

        /// <summary>
        /// Gets the parameter definitions for the report
        /// </summary>
        public ICollection<ReportParameterDefinition> ParameterDefinitions
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets report parameter definitions for a target type
        /// </summary>
        /// <param name="targetType">The target types</param>
        /// <returns>A collection of parameter definitions</returns>
        public IEnumerable<ReportParameterDefinition> GetDefinitions
            (
                ReportParameterTargetType targetType
            )
        {
            return this.ParameterDefinitions.Where
            (
                p => p.TargetType == targetType
            );
        }

        /// <summary>
        /// Determines if a parameter has been defined
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>True, if the parameter has been defined; otherwise false</returns>
        public bool IsDefined
            (
                string parameterName
            )
        {
            Validate.IsNotEmpty(parameterName);

            return this.ParameterDefinitions.Any
            (
                d => d.Parameter.Name.ToLower() == parameterName.ToLower()
            );
        }

        /// <summary>
        /// Gets a single report parameter definition
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>The parameter definition</returns>
        public ReportParameterDefinition GetDefinition
            (
                string parameterName
            )
        {
            Validate.IsNotEmpty(parameterName);

            var definition = this.ParameterDefinitions.FirstOrDefault
            (
                d => d.Parameter.Name.ToLower() == parameterName.ToLower()
            );

            if (definition == null)
            {
                var message = "The name '{0}' did not match any parameters.";

                throw new KeyNotFoundException
                (
                    String.Format(message, parameterName)
                );
            }

            return definition;
        }

        /// <summary>
        /// Gets a collection of parameter values for the filter
        /// </summary>
        public ICollection<ReportFilterParameterValue> ParameterValues
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets a single report filter parameter value
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <param name="value">The value to set</param>
        public void SetParameterValue
            (
                string parameterName,
                object value
            )
        {
            Validate.IsNotEmpty(parameterName);

            var parameterValue = this.ParameterValues.FirstOrDefault
            (
                p => p.Name.ToLower() == parameterName.ToLower()
            );

            if (parameterValue != null)
            {
                parameterValue.SetValue(value);
            }
            else
            {
                var definition = GetDefinition
                (
                    parameterName
                );

                parameterValue = new ReportFilterParameterValue
                (
                    definition,
                    value
                );

                this.ParameterValues.Add
                (
                    parameterValue
                );
            }
        }

        /// <summary>
        /// Sets a collection of report filter parameter values
        /// </summary>
        /// <param name="parameterValues">The parameter values</param>
        public void SetParameterValues
            (
                IDictionary<string, object> parameterValues
            )
        {
            Validate.IsNotNull(parameterValues);
            
            foreach (var pair in parameterValues)
            {
                SetParameterValue
                (
                    pair.Key,
                    pair.Value
                );
            }
        }

        /// <summary>
        /// Gets a single report parameter value from the filter
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <returns>The parameter value</returns>
        public ReportFilterParameterValue GetParameterValue
            (
                string parameterName
            )
        {
            Validate.IsNotEmpty(parameterName);

            var value = this.ParameterValues.FirstOrDefault
            (
                p => p.Name.ToLower() == parameterName.ToLower()
            );

            if (value == null)
            {
                var definition = GetDefinition(parameterName);

                value = new ReportFilterParameterValue
                (
                    definition,
                    definition.Parameter.DefaultValue
                );
            }

            return value;
        }

        /// <summary>
        /// Gets parameter values for a query
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="defaultValues">The default values</param>
        /// <returns>A collection of parameter values</returns>
        public IEnumerable<ParameterValue> GetQueryParameters
            (
                IQuery query,
                params ParameterValue[] defaultValues
            )
        {
            Validate.IsNotNull(query);
            
            var filterValues = this.ParameterValues.Where
            (
                p => p.Definition.TargetType == ReportParameterTargetType.Filter
            );

            var queryParameters = new List<ParameterValue>();

            foreach (var parameter in query.Parameters)
            {
                var matchingValue = default(ParameterValue);

                matchingValue = filterValues.FirstOrDefault
                (
                    pv => pv.Parameter == parameter
                );

                if (matchingValue != null)
                {
                    queryParameters.Add(matchingValue);
                }
                else if (defaultValues != null)
                {
                    // Try and find a matching parameter from the default values
                    matchingValue = defaultValues.FirstOrDefault
                    (
                        pv => pv.Parameter == parameter
                    );

                    if (matchingValue != null)
                    {
                        queryParameters.Add(matchingValue);
                    }
                }
            }

            return queryParameters;
        }

        /// <summary>
        /// Gets parameter values for the report fields
        /// </summary>
        /// <param name="defaultValues">The default values</param>
        /// <returns>A collection of parameter values</returns>
        public IEnumerable<ParameterValue> GetFieldParameters
            (
                params ParameterValue[] defaultValues
            )
        {
            return GetParameterValues
            (
                ReportParameterTargetType.SetField,
                defaultValues
            );
        }

        /// <summary>
        /// Gets parameter values for the component exclusions
        /// </summary>
        /// <param name="defaultValues">The default values</param>
        /// <returns>A collection of parameter values</returns>
        public IEnumerable<ParameterValue> GetComponentExclusionParameters
            (
                params ParameterValue[] defaultValues
            )
        {
            return GetParameterValues
            (
                ReportParameterTargetType.ExcludeComponent,
                defaultValues
            );
        }

        /// <summary>
        /// Gets parameter values for the a specific target type
        /// </summary>
        /// <param name="targetType">The target type</param>
        /// <param name="defaultValues">The default values</param>
        /// <returns>A collection of parameter values</returns>
        protected IEnumerable<ParameterValue> GetParameterValues
            (
                ReportParameterTargetType targetType,
                params ParameterValue[] defaultValues
            )
        {
            var targetValues = new List<ParameterValue>();

            var fieldDefinitions = GetDefinitions
            (
                targetType
            );
            
            var matchingValues = this.ParameterValues.Where
            (
                p => p.Definition.TargetType == targetType
            );

            if (matchingValues.Any())
            {
                targetValues.AddRange(matchingValues);
            }

            if (defaultValues != null)
            {
                foreach (var value in defaultValues)
                {
                    var valueFound = targetValues.Any
                    (
                        v => v.Name.ToLower() == value.Name.ToLower()
                    );

                    if (false == valueFound)
                    {
                        targetValues.Add(value);
                    }
                }
            }

            return targetValues;
        }

        /// <summary>
        /// Gets a dictionary of query sorting rules by query
        /// </summary>
        public ICollection<ReportFilterSortingRule> SortingRules
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets sorting rules for a section type
        /// </summary>
        /// <param name="sectionType">The report section type</param>
        /// <param name="componentName">The component name</param>
        /// <returns>A collection of sorting rules</returns>
        public IEnumerable<ReportFilterSortingRule> GetSortingRules
            (
                ReportSectionType sectionType,
                string componentName
            )
        {
            return this.SortingRules.Where
            (
                rule => rule.SectionType == sectionType
                    && rule.ComponentName.ToLower() == componentName.ToLower()
            );
        }
    }
}
