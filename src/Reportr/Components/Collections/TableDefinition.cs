namespace Reportr.Components.Collections
{
    using Reportr.Data.Querying;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a single report table definition
    /// </summary>
    public class TableDefinition : ReportComponentBase
    {
        /// <summary>
        /// Constructs the table definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="query">The query</param>
        public TableDefinition
            (
                string name,
                string title,
                IQuery query
            )
            : base(name, title)
        {
            Validate.IsNotNull(query);

            this.Query = query;
            this.Columns = new Collection<TableColumnDefinition>();
        }

        /// <summary>
        /// Gets the query that will supply the tables data
        /// </summary>
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the columns defined by the table
        /// </summary>
        public ICollection<TableColumnDefinition> Columns
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
                return ReportComponentType.Table;
            }
        }
    }
}
