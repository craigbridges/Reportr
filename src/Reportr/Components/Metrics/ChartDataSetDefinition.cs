namespace Reportr.Components.Metrics
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;

    /// <summary>
    /// Represents the definition of a single chart data set
    /// </summary>
    public class ChartDataSetDefinition
    {
        /// <summary>
        /// Constructs the set definition with the details
        /// </summary>
        /// <param name="name">The name of the set</param>
        /// <param name="query">The query</param>
        /// <param name="yAxisBinding">The y-axis data binding</param>
        /// <param name="xAxisBinding">The x-axis data binding (optional)</param>
        public ChartDataSetDefinition
            (
                string name,
                IQuery query,
                DataBinding yAxisBinding,
                DataBinding xAxisBinding = null
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(query);
            Validate.IsNotNull(yAxisBinding);

            this.Name = name;
            this.Query = query;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.XAxisBinding = xAxisBinding;
            this.YAxisBinding = yAxisBinding;
            
            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }
        
        /// <summary>
        /// Gets the name of the data set
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the data sets label text
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// Gets a description of the data set
        /// </summary>
        public string Description { get; protected set; }

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
        /// Gets the color of the data set
        /// </summary>
        public Color? Color { get; protected set; }


        // TODO: get label matching x-axis binding result


        /// <summary>
        /// Adds the descriptors to the data set definition
        /// </summary>
        /// <param name="label">The label</param>
        /// <param name="description">The description</param>
        /// <param name="color">The color (optional)</param>
        /// <param name="xAxisLabels">The x-axis labels</param>
        /// <returns>The updated data set definition</returns>
        public ChartDataSetDefinition WithDescriptors
            (
                string label,
                string description,
                Color? color = null,
                params ChartAxisLabel[] xAxisLabels
            )
        {
            this.Label = label;
            this.Description = description;
            this.Color = color;

            if (xAxisLabels == null)
            {
                this.XAxisLabels = new ChartAxisLabel[] { };
            }
            else
            {
                this.XAxisLabels = xAxisLabels;
            }

            return this;
        }

        /// <summary>
        /// Gets the data set action
        /// </summary>
        /// <remarks>
        /// The action is applied to each data point generated
        /// </remarks>
        public ReportActionDefinition Action { get; protected set; }

        /// <summary>
        /// Adds the action to the data set definition
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The updated data set definition</returns>
        public ChartDataSetDefinition WithAction
            (
                ReportActionDefinition action
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;

            return this;
        }
    }
}
