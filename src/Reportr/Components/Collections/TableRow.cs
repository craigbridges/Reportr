namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using Reportr.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the data for a single table row
    /// </summary>
    [JsonObject]
    [DataContract]
    public class TableRow : IEnumerable<TableCell>
    {
        /// <summary>
        /// Constructs the table row with the cell data
        /// </summary>
        /// <param name="importance">The row importance</param>
        /// <param name="cells">The cells</param>
        public TableRow
            (
                DataImportance importance,
                params TableCell[] cells
            )
        {
            this.Importance = importance;

            PopulateCells(cells);
        }

        /// <summary>
        /// Constructs the table row with the cell data
        /// </summary>
        /// <param name="cells">The cells</param>
        public TableRow
            (
                params TableCell[] cells
            )
        {
            this.Importance = DataImportance.Default;

            PopulateCells(cells);
        }

        /// <summary>
        /// Populates the row cells with the table cells specified
        /// </summary>
        /// <param name="cells">The table cells</param>
        private void PopulateCells
            (
                params TableCell[] cells
            )
        {
            Validate.IsNotNull(cells);

            if (false == cells.Any())
            {
                throw new ArgumentException
                (
                    "At least one cell is required to create a row."
                );
            }

            foreach (var set in cells.ToList())
            {
                var name = set.Column.Name;

                var matchCount = cells.Count
                (
                    s => s.Column.Name.ToLower() == name.ToLower()
                );

                if (matchCount > 1)
                {
                    throw new ArgumentException
                    (
                        $"The column '{name}' can only be referenced once."
                    );
                }
            }

            this.Cells = cells;
        }

        /// <summary>
        /// Gets the importance of the row
        /// </summary>
        [DataMember]
        public DataImportance Importance { get; private set; }

        /// <summary>
        /// Gets an array of row cells
        /// </summary>
        public TableCell[] Cells { get; private set; }

        /// <summary>
        /// Adds the action to the table row
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The updated table row</returns>
        public TableRow WithAction
            (
                ReportAction action
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;

            return this;
        }

        /// <summary>
        /// Gets the rows action
        /// </summary>
        [DataMember]
        public ReportAction Action { get; private set; }

        /// <summary>
        /// Gets the cell at the index specified
        /// </summary>
        /// <param name="index">The row index (zero based)</param>
        /// <returns>The matching query cell</returns>
        public TableCell this[int index]
        {
            get
            {
                return this.Cells[index];
            }
        }

        /// <summary>
        /// Gets the cell matching the column name specified
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <returns>The matching query cell</returns>
        public TableCell this[string columnName]
        {
            get
            {
                var cell = this.Cells.FirstOrDefault
                (
                    c => c.Column.Name.ToLower() == columnName.ToLower()
                );

                if (cell == null)
                {
                    throw new KeyNotFoundException
                    (
                        $"No column was found with the name '{columnName}'."
                    );
                }

                return cell;
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of cells
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<TableCell> GetEnumerator()
        {
            return this.Cells.ToList().GetEnumerator();
        }

        /// <summary>
        /// Gets a generic enumerator for the collection of cells
        /// </summary>
        /// <returns>The generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
