namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a dynamic column group for a report table
    /// </summary>
    public class TableDynamicColumnGroup
    {
        /// <summary>
        /// Constructs the dynamic column group with the configuration
        /// </summary>
        /// <param name="name">The group name</param>
        /// <param name="columnQuery">The column query</param>
        /// <param name="valueQuery">The value query</param>
        /// <param name="insertPosition">The column insert position</param>
        public TableDynamicColumnGroup
            (
                string name,
                IQuery columnQuery,
                IQuery valueQuery,
                int? insertPosition = null
            )
        {
            System.Validate.IsNotEmpty(name);
            System.Validate.IsNotNull(columnQuery);
            System.Validate.IsNotNull(valueQuery);

            this.GroupId = Guid.NewGuid();
            this.Columns = new Collection<TableDynamicColumnDefinition>();

            this.Name = name;
            this.ColumnQuery = columnQuery;
            this.ValueQuery = valueQuery;
            this.InsertPosition = insertPosition;
        }

        /// <summary>
        /// Gets the column group ID
        /// </summary>
        public Guid GroupId { get; protected set; }

        /// <summary>
        /// Gets the name of the column group
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the column group insert position index
        /// </summary>
        /// <remarks>
        /// The insert position, if specified, is used to determine
        /// where the dynamic columns will be inserted into the table.
        /// 
        /// For example, a value of 0 would force the columns to be 
        /// inserted before any of the static columns. 
        /// A value of 1 would insert the column group after the 
        /// first static column.
        /// </remarks>
        public int? InsertPosition { get; protected set; }

        /// <summary>
        /// Gets the query that will supply the data for the columns
        /// </summary>
        [JsonIgnore]
        public IQuery ColumnQuery { get; protected set; }

        /// <summary>
        /// Gets the key column mapping for the column query to the value query results
        /// </summary>
        public ColumnMapping ColumnToValueQueryKeyMap { get; private set; }

        /// <summary>
        /// Adds a column to value query key mapping to the dynamic column group
        /// </summary>
        /// <param name="columnQueryKeyFieldName">The column query key field name</param>
        /// <param name="valueQueryKeyFieldName">The value query key field name</param>
        /// <returns>The updated column group</returns>
        /// <remarks>
        /// The column query refers to the query we are using to generate the 
        /// dynamic column headings, which we will use to generate the table columns.
        /// 
        /// The value query refers to the query we are using to generate the data
        /// that is to be mapped to.
        /// </remarks>
        public TableDynamicColumnGroup WithColumnQueryMapping
            (
                string columnQueryKeyFieldName,
                string valueQueryKeyFieldName
            )
        {
            var mapping = new ColumnMapping
            (
                columnQueryKeyFieldName,
                valueQueryKeyFieldName
            );

            this.ColumnToValueQueryKeyMap = mapping;

            return this;
        }

        /// <summary>
        /// Gets the query that will supply the data for the cell values
        /// </summary>
        [JsonIgnore]
        public IQuery ValueQuery { get; protected set; }

        /// <summary>
        /// Gets the key column mapping for the master query to the value query results
        /// </summary>
        public ColumnMapping MasterToValueQueryKeyMap { get; private set; }

        /// <summary>
        /// Adds a master to value query key mapping to the dynamic column group
        /// </summary>
        /// <param name="masterQueryKeyFieldName">The master query key field name</param>
        /// <param name="valueQueryKeyFieldName">The value query key field name</param>
        /// <returns>The updated column group</returns>
        /// <remarks>
        /// The master query refers to the query generating the data for the master 
        /// table (i.e. the table we are trying to generate dynamic columns for).
        /// 
        /// The value query refers to the query we are using to generate the data
        /// that is to be mapped to.
        /// </remarks>
        public TableDynamicColumnGroup WithMasterQueryMapping
            (
                string masterQueryKeyFieldName,
                string valueQueryKeyFieldName
            )
        {
            var mapping = new ColumnMapping
            (
                masterQueryKeyFieldName,
                valueQueryKeyFieldName
            );

            this.MasterToValueQueryKeyMap = mapping;

            return this;
        }

        /// <summary>
        /// Gets the value query results
        /// </summary>
        internal QueryResults ValueResults { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the definition has value data
        /// </summary>
        internal bool HasValueData { get; private set; }

        /// <summary>
        /// Adds the value query results to the dynamic column group
        /// </summary>
        /// <param name="results">The query results</param>
        internal void AddValueResults
            (
                QueryResults results
            )
        {
            System.Validate.IsNotNull(results);

            this.ValueResults = results;
            this.HasValueData = true;
        }

        /// <summary>
        /// Gets the columns that are to be generated within the group
        /// </summary>
        [JsonIgnore]
        public ICollection<TableDynamicColumnDefinition> Columns
        {
            get;
            protected set;
        }

        /// <summary>
        /// Adds a column to the dynamic column group
        /// </summary>
        /// <param name="name">The column name</param>
        /// <param name="headerBinding">The column header binding</param>
        /// <param name="valueBinding">The row value binding</param>
        public void AddColumn
            (
                string name,
                DataBinding headerBinding,
                DataBinding valueBinding
            )
        {
            System.Validate.IsNotEmpty(name);

            var nameUsed = this.Columns.Any
            (
                c => c.Name.ToLower() == name.ToLower()
            );

            if (nameUsed)
            {
                throw new ArgumentException
                (
                    $"The column name '{name}' has already been used."
                );
            }

            var column = new TableDynamicColumnDefinition
            (
                name,
                this,
                headerBinding,
                valueBinding
            );

            this.Columns.Add(column);
        }

        /// <summary>
        /// Gets a single column definition from the table definition
        /// </summary>
        /// <param name="name">The column name</param>
        /// <returns>The matching column definition</returns>
        internal TableColumnDefinition GetColumn
            (
                string name
            )
        {
            System.Validate.IsNotNull(name);

            var column = this.Columns.FirstOrDefault
            (
                definition => definition.Name.Replace
                (
                    " ",
                    String.Empty
                )
                .ToLower() == name.ToLower()
            );

            if (column == null)
            {
                throw new InvalidOperationException
                (
                    $"The name '{name}' did not match any columns."
                );
            }

            return column;
        }

        /// <summary>
        /// Validates the table dynamic column group
        /// </summary>
        internal void Validate()
        {
            if (this.Columns.Count == 0)
            {
                throw new InvalidOperationException
                (
                    "At least one column must be defined for a dynamic column group."
                );
            }

            if (this.ColumnToValueQueryKeyMap == null)
            {
                throw new InvalidOperationException
                (
                    "The column to value query key mapping must be defined."
                );
            }

            if (this.MasterToValueQueryKeyMap == null)
            {
                throw new InvalidOperationException
                (
                    "The master to value query key mapping must be defined."
                );
            }
        }
    }
}
