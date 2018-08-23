namespace Reportr.Data
{
    using System;
    using System.Linq;
    
    /// <summary>
    /// Represents schema information about a single data table
    /// </summary>
    public sealed class DataTableSchema
    {
        /// <summary>
        /// Constructs the table schema with the core details
        /// </summary>
        /// <param name="name">The table name</param>
        /// <param name="columns">The columns in the table</param>
        public DataTableSchema
            (
                string name,
                params DataColumnSchema[] columns
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(columns);

            this.Name = name;
            this.Columns = columns;
        }

        /// <summary>
        /// Gets the name of the table
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets an array of columns in the table
        /// </summary>
        public DataColumnSchema[] Columns { get; private set; }

        /// <summary>
        /// Determines if the table has a column with a given name
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>True, if a matching column was found; otherwise false</returns>
        public bool HasColumn
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            return this.Columns.Any
            (
                c => c.Name.ToLower() == name.ToLower()
            );
        }

        /// <summary>
        /// Ensures all columns specified exists in the table schema
        /// </summary>
        /// <param name="columns">The columns to check</param>
        private void EnsureColumnsExist
            (
                DataColumnSchema[] columns
            )
        {
            Validate.IsNotNull(columns);

            foreach (var column in columns)
            {
                var exists = HasColumn
                (
                    column.Name
                );

                if (false == exists)
                {
                    throw new InvalidOperationException
                    (
                        $"No column was found matching the name '{column.Name}'."
                    );
                }
            }
        }

        /// <summary>
        /// Adds the primary key to the table schema
        /// </summary>
        /// <param name="columns">The columns that make up the primary key</param>
        /// <returns>The updated table schema</returns>
        public DataTableSchema WithPrimaryKey
            (
                params DataColumnSchema[] columns
            )
        {
            Validate.IsNotNull(columns);

            if (columns.Length == 0)
            {
                throw new ArgumentException
                (
                    "The primary key must use at least one column."
                );
            }
            
            EnsureColumnsExist(columns);

            this.PrimaryKey = columns;

            return this;
        }

        /// <summary>
        /// Gets an array of columns that make up the primary key
        /// </summary>
        public DataColumnSchema[] PrimaryKey { get; private set; }

        /// <summary>
        /// Adds the foreign keys to the table schema
        /// </summary>
        /// <param name="foreignKeys">The foreign keys to add</param>
        /// <returns>The updated table schema</returns>
        public DataTableSchema WithForeignKeys
            (
                params DataForeignKey[] foreignKeys
            )
        {
            Validate.IsNotNull(foreignKeys);

            // Ensure all columns specified exist in both tables
            foreach (var foreignKey in foreignKeys)
            {
                EnsureColumnsExist
                (
                    foreignKey.ForeignKeyColumns
                );

                foreignKey.ReferencedTable.EnsureColumnsExist
                (
                    foreignKey.ReferencedColumns
                );
            }

            this.ForeignKeys = foreignKeys;

            return this;
        }

        /// <summary>
        /// Gets an array of foreign keys managed by the table
        /// </summary>
        public DataForeignKey[] ForeignKeys { get; private set; }
    }
}
