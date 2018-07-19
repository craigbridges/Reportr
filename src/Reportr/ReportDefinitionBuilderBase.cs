namespace Reportr
{
    using Reportr.Data.Querying;
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

        // TODO: method that automatically creates report filter parameters based on a query

        // TODO: method that automatically creates a table component and adds it to the report based on a query
            // - Adds report parameters based on query parameters
            // - Creates columns that map directly to the query columns
            // - Optional params array of excluded columns
    }
}
