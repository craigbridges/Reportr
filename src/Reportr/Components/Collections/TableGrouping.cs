namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Represents the data for a single table grouping
    /// </summary>
    [JsonObject]
    [DataContract]
    public sealed class TableGrouping : IEnumerable<TableRow>
    {
        /// <summary>
        /// Constructs the table grouping with the details
        /// </summary>
        /// <param name="groupingValues">The grouping values</param>
        /// <param name="rows">The rows</param>
        /// <param name="totals">The totals (optional)</param>
        public TableGrouping
            (
                Dictionary<TableColumn, object> groupingValues,
                TableRow[] rows,
                TableCell[] totals = null
            )
        {
            Validate.IsNotNull(groupingValues);
            Validate.IsNotNull(rows);

            SetGroupingValues(groupingValues);

            this.Rows = rows;

            if (totals == null)
            {
                this.Totals = new TableCell[] { };
            }
            else
            {
                this.Totals = totals;
            }
        }
        
        /// <summary>
        /// Sets the table grouping values
        /// </summary>
        /// <param name="groupingValues">The grouping values</param>
        private void SetGroupingValues
            (
                Dictionary<TableColumn, object> groupingValues
            )
        {
            if (groupingValues.Count == 0)
            {
                throw new ArgumentException
                (
                    "At least one grouping value must be defined."
                );
            }

            this.GroupingValues = groupingValues;

            var builder = new StringBuilder();

            foreach (var item in groupingValues)
            {
                if (item.Value != null)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" - ");
                    }

                    builder.Append
                    (
                        item.Value.ToString()
                    );
                }
            }

            this.GroupingValueString = builder.ToString();
        }

        /// <summary>
        /// Gets the grouping values by column
        /// </summary>
        public Dictionary<TableColumn, object> GroupingValues
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a string description of the grouping values
        /// </summary>
        public string GroupingValueString { get; private set; }

        /// <summary>
        /// Gets an array of the rows in the grouping
        /// </summary>
        [DataMember]
        public TableRow[] Rows { get; private set; }

        /// <summary>
        /// Gets an array of row cells representing the grouping totals
        /// </summary>
        [DataMember]
        public TableCell[] Totals { get; private set; }
        
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
