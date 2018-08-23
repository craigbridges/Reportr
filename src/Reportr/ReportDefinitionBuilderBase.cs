namespace Reportr
{
    using Reportr.Components.Collections;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Data.Querying.Functions;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Represents a base implementation for a report definition builder
    /// </summary>
    public abstract class ReportDefinitionBuilderBase : IReportDefinitionBuilder
    {
        /// <summary>
        /// Builds the report definition
        /// </summary>
        /// <param name="queryRepository">The query repository</param>
        /// <returns>The report definition generated</returns>
        public abstract ReportDefinition Build
        (
            IQueryRepository queryRepository
        );

        /// <summary>
        /// Auto populates a report definitions parameters from a query
        /// </summary>
        /// <param name="reportDefinition">The report definition</param>
        /// <param name="query">The query</param>
        /// <param name="targetName">The target name (optional)</param>
        protected virtual void AutoPopulateParameters
            (
                ref ReportDefinition reportDefinition,
                IQuery query,
                string targetName = null
            )
        {
            Validate.IsNotNull(reportDefinition);
            Validate.IsNotNull(query);

            if (targetName == null)
            {
                targetName = query.Name;
            }

            foreach (var parameter in query.Parameters)
            {
                var hasParameter = reportDefinition.HasParameter
                (
                    parameter.Name
                );

                if (false == hasParameter)
                {
                    reportDefinition.AddParameter
                    (
                        parameter,
                        ReportParameterTargetType.Filter,
                        targetName
                    );
                }
            }
        }

        /// <summary>
        /// Auto generates a table based on the schema of a query
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="tableTitle">The table title</param>
        /// <param name="excludedColumns">The names of excluded columns</param>
        /// <returns>The table definition created</returns>
        protected virtual TableDefinition AutoGenerateTable
            (
                IQuery query,
                string tableTitle,
                params string[] excludedColumns
            )
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(excludedColumns);
            
            var tableDefinition = new TableDefinition
            (
                query.Name,
                tableTitle,
                query
            );

            foreach (var columnInfo in query.Columns)
            {
                var columnName = columnInfo.Column.Name;

                var isExcluded = excludedColumns.Any
                (
                    exclusion => exclusion.ToLower() == columnName.ToLower()
                );

                if (false == isExcluded)
                {
                    var valueType = columnInfo.Column.ValueType;
                    var totalAggregator = default(IAggregateFunction);

                    if (valueType.IsNumeric() && false == valueType.IsEnumAssignable())
                    {
                        totalAggregator = new SumFunction
                        (
                            new DataBinding
                            (
                                columnName
                            )
                        );
                    }

                    var columnDefinition = new TableColumnDefinition
                    (
                        columnName.Spacify(),
                        new DataBinding
                        (
                            DataBindingType.QueryPath,
                            columnName
                        ),
                        totalAggregator
                    );

                    tableDefinition.Columns.Add
                    (
                        columnDefinition
                    );
                }
            }

            return tableDefinition;
        }

        /// <summary>
        /// Generates a table based on the schema of a query with the columns specified
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="tableTitle">The table title</param>
        /// <param name="columnNames">The names of columns to map</param>
        /// <returns>The table definition created</returns>
        protected virtual TableDefinition GenerateTableWith
            (
                IQuery query,
                string tableTitle,
                params string[] columnNames
            )
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(columnNames);
            
            var columnMappings = new List<KeyValuePair<string, string>>();

            // Auto generate column mappings from the column names
            foreach (var name in columnNames)
            {
                var label = name.Spacify();

                columnMappings.Add
                (
                    new KeyValuePair<string, string>
                    (
                        name,
                        label
                    )
                );
            }

            var tableDefinition = GenerateTableWith
            (
                query,
                tableTitle,
                columnMappings.ToArray()
            );

            return tableDefinition;
        }

        /// <summary>
        /// Generates a table based on the schema of a query with the columns specified
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="tableTitle">The table title</param>
        /// <param name="columnMappings">The names of columns to map</param>
        /// <returns>The table definition created</returns>
        /// <remarks>
        /// The column mappings are represented as an array of key-value pairs.
        /// 
        /// Each pair represents a single query column (key) and the table 
        /// column (value) that it maps to.
        /// </remarks>
        protected virtual TableDefinition GenerateTableWith
            (
                IQuery query,
                string tableTitle,
                params KeyValuePair<string, string>[] columnMappings
            )
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(columnMappings);

            if (columnMappings.Length == 0)
            {
                throw new ArgumentException
                (
                    "At least one column must be specified to create a table."
                );
            }

            var tableDefinition = new TableDefinition
            (
                query.Name,
                tableTitle,
                query
            );

            foreach (var mapping in columnMappings)
            {
                var columnFound = query.HasColumn
                (
                    mapping.Key
                );

                if (false == columnFound)
                {
                    throw new InvalidOperationException
                    (
                        $"The query does not have a column named '{mapping.Key}'."
                    );
                }
                
                var columnInfo = query.GetColumn
                (
                    mapping.Key
                );
                
                var totalAggregator = default(IAggregateFunction);

                if (columnInfo.Column.ValueType.IsNumeric())
                {
                    totalAggregator = new SumFunction
                    (
                        new DataBinding
                        (
                            mapping.Key
                        )
                    );
                }

                var columnDefinition = new TableColumnDefinition
                (
                    mapping.Value,
                    new DataBinding
                    (
                        DataBindingType.QueryPath,
                        mapping.Key
                    ),
                    totalAggregator
                );
                
                tableDefinition.Columns.Add
                (
                    columnDefinition
                );
            }
            
            return tableDefinition;
        }
    }
}
