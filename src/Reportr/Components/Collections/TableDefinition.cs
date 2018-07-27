namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using Reportr.Data.Querying;
    using Reportr.Filtering;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a single report table definition
    /// </summary>
    public class TableDefinition : ReportComponentDefinitionBase, ISortableComponent
    {
        /// <summary>
        /// Constructs the table definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="query">The query</param>
        /// <param name="rowAction">The row action (optional)</param>
        public TableDefinition
            (
                string name,
                string title,
                IQuery query,
                ReportActionDefinition rowAction = null
            )
            : base(name, title)
        {
            Validate.IsNotNull(query);
            
            this.Query = query;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.Columns = new Collection<TableColumnDefinition>();
            this.RowAction = rowAction;

            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }
        
        /// <summary>
        /// Gets the query that will supply the tables data
        /// </summary>
        [JsonIgnore]
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the default parameter values for the query
        /// </summary>
        [JsonIgnore]
        public ICollection<ParameterValue> DefaultParameterValues
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the columns defined by the table
        /// </summary>
        [JsonIgnore]
        public ICollection<TableColumnDefinition> Columns
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a flag indicating if the table definition has any totals
        /// </summary>
        public bool HasTotals
        {
            get
            {
                return this.Columns.Any
                (
                    column => column.HasTotal
                );
            }
        }

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
        /// Gets the action associated with each table row
        /// </summary>
        /// <remarks>
        /// The row action can be used to make an entire table row
        /// a click-able action, instead of just a single table cell.
        /// 
        /// The row action can be null, but when set, it overrides
        /// any column actions that have been set.
        /// </remarks>
        [JsonIgnore]
        public ReportActionDefinition RowAction { get; protected set; }

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
                return this.Columns.Select(c => c.Name).ToArray();
            }
        }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Table;
            }
        }
    }
}
