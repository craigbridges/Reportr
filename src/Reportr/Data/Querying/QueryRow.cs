namespace Reportr.Data.Querying
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
                var name = set.Column.Name;

                var matchCount = cells.Count
                (
                    s => s.Column.Name.Trim().ToLower() == name.Trim().ToLower()
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
        /// Finds a cell in the query row by column name
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <returns>The matching cell, if found; otherwise null</returns>
        public QueryCell FindCell
            (
                string columnName
            )
        {
            Validate.IsNotEmpty(columnName);

            return this.Cells.FirstOrDefault
            (
                c => c.Column.Name.ToLower() == columnName.ToLower()
            );
        }

        /// <summary>
        /// Finds a cell value in the query row by column name
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <returns>The matching cell value, if found; otherwise null</returns>
        public object FindCellValue
            (
                string columnName
            )
        {
            var cell = FindCell(columnName);

            if (cell == null)
            {
                return null;
            }
            else
            {
                return cell.Value;
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
            return GetEnumerator();
        }
    }
}
