namespace Reportr.Components.Metrics
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents the definition of a single chart data set
    /// </summary>
    public class ChartDataSetDefinition
    {
        public ChartDataSetDefinition
            (
                params ChartAxisDefinition[] xAxises
            )
        {
            this.XAxises = xAxises;
        }

        /// <summary>
        /// Gets an array of x-axis definitions
        /// </summary>
        public ChartAxisDefinition[] XAxises { get; protected set; }


        
        // query and binding (each row in the query results would be treated as an X value entry)
    }
}
