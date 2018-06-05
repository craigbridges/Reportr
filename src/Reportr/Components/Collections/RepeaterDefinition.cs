namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a single report repeater definition
    /// </summary>
    public class RepeaterDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the repeater definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="query">The query</param>
        /// <param name="binding">The data binding</param>
        public RepeaterDefinition
            (
                string name,
                string title,
                IQuery query,
                DataBinding binding
            )
            : base(name, title)
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(binding);

            this.Query = query;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.NestedComponents = new Collection<NestedReportComponentDefinition>();
            this.Binding = binding;

            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }

        /// <summary>
        /// Gets the query that will supply the tables data
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
        /// Gets the data binding for the repeater
        /// </summary>
        public DataBinding Binding { get; protected set; }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public override IEnumerable<IQuery> GetQueriesUsed()
        {
            return new IQuery[]
            {
                this.Query
            };
        }

        /// <summary>
        /// Adds the action and type to the repeater
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="type">The repeater type</param>
        /// <returns></returns>
        public RepeaterDefinition WithAction
            (
                ReportActionDefinition action,
                RepeaterType type
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;
            this.RepeaterType = type;

            return this;
        }

        /// <summary>
        /// Gets the action associated with the repeater
        /// </summary>
        public ReportActionDefinition Action { get; protected set; }

        /// <summary>
        /// Gets the repeater type
        /// </summary>
        public RepeaterType RepeaterType { get; protected set; }

        /// <summary>
        /// Gets or sets a flag indicating if column sorting is disabled
        /// </summary>
        public bool DisableSorting { get; set; }

        /// <summary>
        /// Gets an array of sortable columns
        /// </summary>
        public string[] SortableColumns
        {
            get
            {
                return new string[] { "Item" };
            }
        }

        /// <summary>
        /// Gets a collection of nested report component definitions
        /// </summary>
        /// <remarks>
        /// Nested components are generated for each row in the query results.
        /// 
        /// The component, in addition to the report filter gets given the
        /// current row. This way the component can filter it's own query
        /// based on data contained in the row.
        /// 
        /// An example of where this could be used would be a repeater that
        /// generates a list of employees, but for each employee we want to
        /// generate a table of their sales for the month along with a pie 
        /// chart showing a breakdown of which product they sold.
        /// </remarks>
        public ICollection<NestedReportComponentDefinition> NestedComponents
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Repeater;
            }
        }
    }
}
