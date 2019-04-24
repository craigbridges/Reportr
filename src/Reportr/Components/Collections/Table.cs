namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using System;
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
        /// Constructs the table with all rows
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="generatedColumns">The columns that were generated</param>
        /// <param name="rows">The table rows</param>
        public Table
            (
                TableDefinition definition,
                IEnumerable<TableColumnDefinition> generatedColumns,
                IEnumerable<TableRow> rows
            )
            : base(definition)
        {
            Validate.IsNotNull(rows);

            BuildColumns(generatedColumns);

            this.AllRows = rows.ToArray();
            this.HasGroupings = false;
        }

        /// <summary>
        /// Constructs the table with groupings
        /// </summary>
        /// <param name="definition">The table definition</param>
        /// <param name="generatedColumns">The columns that were generated</param>
        /// <param name="groupings">The table groupings</param>
        public Table
            (
                TableDefinition definition,
                IEnumerable<TableColumnDefinition> generatedColumns,
                IEnumerable<TableGrouping> groupings
            )
            : base(definition)
        {
            Validate.IsNotNull(groupings);

            BuildColumns(generatedColumns);

            var allRows = new List<TableRow>();

            foreach (var group in groupings)
            {
                allRows.AddRange
                (
                    group.Rows
                );
            }

            this.HasGroupings = true;
            this.Groupings = groupings.ToArray();
            this.AllRows = allRows.ToArray();
        }

        /// <summary>
        /// Builds the tables columns from the definition specified
        /// </summary>
        /// <param name="columnDefinitions">The column definitions</param>
        private void BuildColumns
            (
                IEnumerable<TableColumnDefinition> columnDefinitions
            )
        {
            var columns = new List<TableColumn>();

            foreach (var definition in columnDefinitions)
            {
                columns.Add
                (
                    new TableColumn
                    (
                        definition.Name,
                        definition.Title,
                        definition.Alignment,
                        definition.Importance,
                        definition.NoWrap
                    )
                );
            }

            this.Columns = columns.ToArray();
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
        public TableRow[] AllRows { get; protected set; }

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
                return this.AllRows[index];
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of rows
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<TableRow> GetEnumerator()
        {
            return this.AllRows.ToList().GetEnumerator();
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
