namespace Reportr.Data.Querying
{
    using System;

    /// <summary>
    /// Represents information about a single query column
    /// </summary>
    public class QueryColumnInfo
    {
        /// <summary>
        /// Constructs the column information with the details
        /// </summary>
        /// <param name="table">The table schema</param>
        /// <param name="column">The column schema</param>
        public QueryColumnInfo
            (
                DataTableSchema table,
                DataColumnSchema column
            )
        {
            Validate.IsNotNull(table);
            Validate.IsNotNull(column);

            this.Table = table;
            this.Column = column;
        }

        /// <summary>
        /// Gets the table schema
        /// </summary>
        public DataTableSchema Table { get; private set; }

        /// <summary>
        /// Gets the column schema
        /// </summary>
        public DataColumnSchema Column { get; private set; }

        /// <summary>
        /// Gets the hash code related to the column
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return this.Column.Name.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if both objects are equal; otherwise, false</returns>
        public override bool Equals
            (
                object obj
            )
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj.GetType() != typeof(QueryColumnInfo))
            {
                return false;
            }
            else
            {
                var leftName = this.Column.Name;
                var rightName = ((QueryColumnInfo)obj).Column.Name;

                return leftName == rightName;
            }
        }
    }
}
