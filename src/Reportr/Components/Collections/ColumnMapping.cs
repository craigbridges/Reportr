namespace Reportr.Components.Collections
{
    using System;
    
    /// <summary>
    /// Represents a single column mapping specification
    /// </summary>
    public class ColumnMapping
    {
        /// <summary>
        /// Constructs the column mapping with a from and to column
        /// </summary>
        /// <param name="fromColumnName">The from column name</param>
        /// <param name="toColumnName">The to column name</param>
        public ColumnMapping
            (
                string fromColumnName,
                string toColumnName
            )
        {
            Validate.IsNotEmpty(fromColumnName);
            Validate.IsNotNull(toColumnName);

            this.FromColumnName = fromColumnName;
            this.ToColumnName = toColumnName;
        }

        /// <summary>
        /// Gets the mapping from column name
        /// </summary>
        public string FromColumnName { get; private set; }

        /// <summary>
        /// Gets the mapping to column name
        /// </summary>
        public string ToColumnName { get; private set; }
    }
}
