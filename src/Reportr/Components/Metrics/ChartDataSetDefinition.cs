namespace Reportr.Components.Metrics
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the definition of a single chart data set
    /// </summary>
    public class ChartDataSetDefinition
    {
        /// <summary>
        /// Constructs the set definition with the details
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="xAxisBinding">The x-axis data binding</param>
        /// <param name="yAxisBinding">The y-axis data binding</param>
        /// <param name="xAxisLabels">The x-axis labels</param>
        public ChartDataSetDefinition
            (
                IQuery query,
                DataBinding xAxisBinding,
                DataBinding yAxisBinding,
                params ChartAxisLabel[] xAxisLabels
            )
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(xAxisBinding);
            Validate.IsNotNull(yAxisBinding);

            this.Query = query;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.XAxisBinding = xAxisBinding;
            this.YAxisBinding = yAxisBinding;

            if (xAxisLabels == null)
            {
                this.XAxisLabels = new ChartAxisLabel[] { };
            }
            else
            {
                this.XAxisLabels = xAxisLabels;
            }

            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }
        
        /// <summary>
        /// Gets the query associated with the data set
        /// </summary>
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the default parameter values for the query
        /// </summary>
        public ICollection<ParameterValue> DefaultParameterValues
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the x-axis value data binding
        /// </summary>
        public DataBinding XAxisBinding { get; protected set; }

        /// <summary>
        /// Gets the y-axis value data binding
        /// </summary>
        public DataBinding YAxisBinding { get; protected set; }

        /// <summary>
        /// Gets an array of x-axis labels
        /// </summary>
        public ChartAxisLabel[] XAxisLabels { get; protected set; }

        /// <summary>
        /// Gets the data set action
        /// </summary>
        /// <remarks>
        /// The action is applied to each data point generated
        /// </remarks>
        public ReportAction Action { get; protected set; }

        /// <summary>
        /// Adds the action to the data set definition
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The updated data set definition</returns>
        public ChartDataSetDefinition WithAction
            (
                ReportAction action
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;

            return this;
        }
    }
}
