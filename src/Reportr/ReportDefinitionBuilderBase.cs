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
        /// Auto creates a table based on the schema of a query
        /// </summary>
        /// <param name="reportDefinition">The report definition</param>
        /// <param name="query">The query</param>
        /// <param name="tableTitle">The table title</param>
        /// <param name="excludedColumns">The names of excluded columns</param>
        protected virtual void AutoCreateTable
            (
                ref ReportDefinition reportDefinition,
                IQuery query,
                string tableTitle,
                params string[] excludedColumns
            )
        {
            Validate.IsNotNull(reportDefinition);
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
                    var totalAggregator = default(IAggregateFunction);

                    if (columnInfo.Column.ValueType.IsNumeric())
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

            reportDefinition.Body.Components.Add
            (
                tableDefinition
            );

            AutoPopulateParameters
            (
                ref reportDefinition,
                query
            );
        }

        /// <summary>
        /// Creates a table based on the schema of a query with the columns specified
        /// </summary>
        /// <param name="reportDefinition">The report definition</param>
        /// <param name="query">The query</param>
        /// <param name="tableTitle">The table title</param>
        /// <param name="columnMappings">The names of columns to map</param>
        /// <remarks>
        /// The column mappings are represented as an array of key-value pairs.
        /// 
        /// Each pair represents a single query column (key) and the table 
        /// column (value) that it maps to.
        /// </remarks>
        protected virtual void CreateTableWith
            (
                ref ReportDefinition reportDefinition,
                IQuery query,
                string tableTitle,
                params KeyValuePair<string, string>[] columnMappings
            )
        {
            Validate.IsNotNull(reportDefinition);
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
                    var message = "The query does not have a column named '{0}'.";

                    throw new InvalidOperationException
                    (
                        String.Format
                        (
                            message,
                            mapping.Key
                        )
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
            
            reportDefinition.Body.Components.Add
            (
                tableDefinition
            );

            AutoPopulateParameters
            (
                ref reportDefinition,
                query
            );
        }
    }
}
