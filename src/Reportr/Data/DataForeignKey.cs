namespace Reportr.Data
{
    /// <summary>
    /// Represents the details of a single foreign key
    /// </summary>
    public sealed class DataForeignKey
    {
        /// <summary>
        /// Constructs the foreign key with the details
        /// </summary>
        /// <param name="foreignKeyColumns">The foreign key columns</param>
        /// <param name="referencedTable">The referenced table</param>
        /// <param name="referencedColumns">The referenced columns</param>
        public DataForeignKey
            (
                DataColumnSchema[] foreignKeyColumns,
                DataTableSchema referencedTable,
                DataColumnSchema[] referencedColumns
            )
        {
            Validate.IsNotNull(foreignKeyColumns);
            Validate.IsNotNull(referencedTable);
            Validate.IsNotNull(referencedColumns);

            this.ForeignKeyColumns = foreignKeyColumns;
            this.ReferencedTable = referencedTable;
            this.ReferencedColumns = referencedColumns;
        }

        /// <summary>
        /// Gets an array of columns that make up the foreign key
        /// </summary>
        public DataColumnSchema[] ForeignKeyColumns { get; private set; }

        /// <summary>
        /// Gets the table being referenced by the foreign key
        /// </summary>
        public DataTableSchema ReferencedTable { get; private set; }

        /// <summary>
        /// Gets an array of columns that are being referenced
        /// </summary>
        public DataColumnSchema[] ReferencedColumns { get; private set; }
    }
}
