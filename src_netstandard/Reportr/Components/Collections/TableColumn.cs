namespace Reportr.Components.Collections
{
    using System;

    /// <summary>
    /// Represents a single report table column
    /// </summary>
    public class TableColumn
    {
        /// <summary>
        /// Constructs the table column with the details
        /// </summary>
        /// <param name="name">The column name</param>
        /// <param name="title">The title</param>
        /// <param name="alignment">The alignment (optional)</param>
        /// <param name="importance">The importance (optional)</param>
        public TableColumn
            (
                string name,
                string title,
                ColumnAlignment alignment = default(ColumnAlignment),
                ColumnImportance importance = default(ColumnImportance)
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotEmpty(title);

            this.ColumnId = Guid.NewGuid();
            this.Name = name;
            this.Title = title;
            this.Alignment = alignment;
            this.Importance = importance;
        }

        /// <summary>
        /// Gets the column ID
        /// </summary>
        public Guid ColumnId { get; protected set; }

        /// <summary>
        /// Gets the column name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the column title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the column alignment
        /// </summary>
        public ColumnAlignment Alignment { get; protected set; }

        /// <summary>
        /// Gets the importance of the column
        /// </summary>
        public ColumnImportance Importance { get; private set; }
        
        /// <summary>
        /// Provides a custom string description
        /// </summary>
        /// <returns>The column name</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
