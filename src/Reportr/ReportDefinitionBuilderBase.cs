namespace Reportr
{
    using Reportr.Components.Collections;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System;
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
        /// <param name="definition">The report definition</param>
        /// <param name="query">The query</param>
        /// <param name="targetName">The target name (optional)</param>
        protected virtual void AutoPopulateParameters
            (
                ref ReportDefinition definition,
                IQuery query,
                string targetName = null
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(query);

            if (targetName == null)
            {
                targetName = query.Name;
            }

            foreach (var parameter in query.Parameters)
            {
                var hasParameter = definition.HasParameter
                (
                    parameter.Name
                );

                if (false == hasParameter)
                {
                    definition.AddParameter
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
        /// <param name="definition">The report definition</param>
        /// <param name="query">The query</param>
        /// <param name="tableTitle">The table title</param>
        /// <param name="excludedColumns">The names of excluded columns</param>
        protected virtual void AutoCreateTable
            (
                ref ReportDefinition definition,
                IQuery query,
                string tableTitle,
                params string[] excludedColumns
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(query);
            Validate.IsNotNull(excludedColumns);

            AutoPopulateParameters(ref definition, query);

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

                tableDefinition.Columns.Add
                (
                    new TableColumnDefinition
                    (
                        columnName.Spacify(),
                        new DataBinding
                        (
                            DataBindingType.QueryPath,
                            columnName
                        )
                    )
                );
            }
        }
    }
}
