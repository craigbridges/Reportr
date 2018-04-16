namespace Reportr.Components.Querying
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the data for a single query row
    /// </summary>
    public class QueryRow : IEnumerable<QueryCell>
    {
        /// <summary>
        /// Constructs the query row with the cell data
        /// </summary>
        /// <param name="cells">The cells</param>
        public QueryRow
            (
                params QueryCell[] cells
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
                var name = set.ColumnName;

                var matchCount = cells.Count
                (
                    s => s.ColumnName.Trim().ToLower() == name.Trim().ToLower()
                );

                if (matchCount > 1)
                {
                    var message = "The column '{0}' can only be referenced once.";

                    throw new ArgumentException
                    (
                        String.Format(message, name)
                    );
                }
            }
        }

        /// <summary>
        /// Gets an array of row cells
        /// </summary>
        public QueryCell[] Cells { get; private set; }

        /// <summary>
        /// Gets the cell at the index specified
        /// </summary>
        /// <param name="index">The row index (zero based)</param>
        /// <returns>The matching query cell</returns>
        public QueryCell this[int index]
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
        public QueryCell this[string columnName]
        {
            get
            {
                var cell = this.Cells.FirstOrDefault
                (
                    c => c.ColumnName == columnName
                );

                if (cell == null)
                {
                    var message = "No column was found matching the name '{0}'.";

                    throw new KeyNotFoundException
                    (
                        String.Format(message, columnName)
                    );
                }

                return cell;
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of cells
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<QueryCell> GetEnumerator()
        {
            return this.Cells.ToList().GetEnumerator();
        }

        /// <summary>
        /// Gets a generic enumerator for the collection of cells
        /// </summary>
        /// <returns>The generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
