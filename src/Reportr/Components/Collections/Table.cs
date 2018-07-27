namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a single report table
    /// </summary>
    [JsonObject]
    [DataContract]
    public class Table : ReportComponentBase, IEnumerable<TableRow>
    {
        /// <summary>
        /// Constructs the table with the details
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="rows">The table rows</param>
        /// <param name="totals">The table totals</param>
        public Table
            (
                TableDefinition definition,
                IEnumerable<TableRow> rows,
                IEnumerable<TableCell> totals = null
            )
            : base(definition)
        {
            Validate.IsNotNull(rows);
            Validate.IsNotNull(totals);

            var columns = new List<TableColumn>();

            foreach (var columnDefinition in definition.Columns)
            {
                columns.Add
                (
                    new TableColumn
                    (
                        columnDefinition.Name,
                        columnDefinition.Title,
                        columnDefinition.Alignment,
                        columnDefinition.Importance
                    )
                );
            }

            this.Columns = columns.ToArray();
            this.Rows = rows.ToArray();

            SetTotals(totals);
        }

        /// <summary>
        /// Gets the columns in the table
        /// </summary>
        [DataMember]
        public TableColumn[] Columns { get; protected set; }

        /// <summary>
        /// Gets an array of the groupings in the table
        /// </summary>
        [DataMember]
        public TableGrouping[] Groupings { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the table has any groupings
        /// </summary>
        public bool HasGroupings { get; private set; }

        /// <summary>
        /// Gets all the rows in the table
        /// </summary>
        [DataMember]
        public TableRow[] Rows { get; protected set; }

        /// <summary>
        /// Gets an array of row cells representing the table totals
        /// </summary>
        [DataMember]
        public TableCell[] Totals { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the table has totals
        /// </summary>
        public bool HasTotals { get; private set; }

        /// <summary>
        /// Sets the tables totals values
        /// </summary>
        /// <param name="totals">The totals to set</param>
        internal void SetTotals
            (
                IEnumerable<TableCell> totals
            )
        {
            if (totals != null && totals.Any())
            {
                this.Totals = totals.ToArray();
                this.HasTotals = true;
            }
            else
            {
                this.Totals = new TableCell[] { };
                this.HasTotals = false;
            }
        }

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
