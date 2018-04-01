namespace Reportr.Querying
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
        /// <param name="name">The column name</param>
        /// <param name="dataType">The data type</param>
        /// <param name="importance">The importance (optional)</param>
        public QueryColumnInfo
            (
                string name,
                Type dataType,
                QueryColumnImportance importance = QueryColumnImportance.Low
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(dataType);

            this.Name = name;
            this.DataType = dataType;
            this.Importance = importance;
        }

        /// <summary>
        /// Gets the column name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the columns data type
        /// </summary>
        public Type DataType { get; private set; }

        /// <summary>
        /// Gets the importance of the column
        /// </summary>
        public QueryColumnImportance Importance { get; private set; }
    }
}
