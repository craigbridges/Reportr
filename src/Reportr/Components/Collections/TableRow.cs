namespace Reportr.Components.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the data for a single table row
    /// </summary>
    public class TableRow : IEnumerable<TableCell>
    {
        /// <summary>
        /// Constructs the table row with the cell data
        /// </summary>
        /// <param name="cells">The cells</param>
        public TableRow
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
                    s => s.Column.Name.Trim().ToLower() == name.Trim().ToLower()
                );

                if (matchCount > 1)
                {
                    var message = "The column '{0}' can only be referenced once.";

                    throw new ArgumentException
                    (
                        String.Format
                        (
                            message,
                            name
                        )
                    );
                }
            }

            this.Cells = cells;
        }

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
                    var message = "No column was found with the name '{0}'.";

                    throw new KeyNotFoundException
                    (
                        String.Format
                        (
                            message,
                            columnName
                        )
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
