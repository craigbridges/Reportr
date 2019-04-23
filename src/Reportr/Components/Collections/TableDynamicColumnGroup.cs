namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
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
        /// <param name="insertPosition">The column insert position</param>
        public TableDynamicColumnGroup
            (
                string name,
                IQuery columnQuery,
                int? insertPosition = null
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(columnQuery);
            
            this.GroupId = Guid.NewGuid();
            this.Columns = new Collection<TableDynamicColumnDefinition>();

            this.Name = name;
            this.ColumnQuery = columnQuery;
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
        /// Gets the columns that are to be generated within the group
        /// </summary>
        [JsonIgnore]
        public ICollection<TableDynamicColumnDefinition> Columns
        {
            get;
            protected set;
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
            Validate.IsNotNull(name);

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
    }
}
