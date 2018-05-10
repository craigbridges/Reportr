namespace Reportr.Components.Collections
{
    using Reportr.Data.Querying;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single report table
    /// </summary>
    public class Table : ReportComponentOutputBase, IEnumerable<TableRow>
    {
        /// <summary>
        /// Constructs the table with the details
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="results">The query results</param>
        /// <param name="columns">The table columns</param>
        /// <param name="rows">The table rows</param>
        public Table
            (
                TableDefinition definition,
                QueryResults results,
                TableColumn[] columns,
                TableRow[] rows
            )
            : base
            (
                definition,
                results
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(results);
            Validate.IsNotNull(columns);
            Validate.IsNotNull(rows);

            this.Columns = columns;
            this.Rows = rows;
        }
        
        /// <summary>
        /// Gets the columns in the table
        /// </summary>
        public TableColumn[] Columns { get; protected set; }

        /// <summary>
        /// Gets the rows in the table
        /// </summary>
        public TableRow[] Rows { get; protected set; }

        /// <summary>
        /// Gets the row at the index specified
        /// </summary>
        /// <param name="index">The row index (zero based)</param>
        /// <returns>The matching table row</returns>
        public TableRow this[int index]
        {
            get
            {
                return this.Rows[index];
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of rows
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<TableRow> GetEnumerator()
        {
            return this.Rows.ToList().GetEnumerator();
        }

        /// <summary>
        /// Gets a generic enumerator for the collection of rows
        /// </summary>
        /// <returns>The generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
