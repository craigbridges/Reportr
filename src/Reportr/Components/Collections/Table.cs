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
        public Table
            (
                TableDefinition definition,
                params TableRow[] rows
            )
            : base(definition)
        {
            Validate.IsNotNull(rows);
            
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
            this.Rows = rows;
        }

        /// <summary>
        /// Gets the columns in the table
        /// </summary>
        [DataMember]
        public TableColumn[] Columns { get; protected set; }

        /// <summary>
        /// Gets the rows in the table
        /// </summary>
        [DataMember]
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
