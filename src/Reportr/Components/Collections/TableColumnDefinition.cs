namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using System;

    /// <summary>
    /// Represents a single report table column definition
    /// </summary>
    public class TableColumnDefinition
    {
        /// <summary>
        /// Constructs the column definition with the details
        /// </summary>
        /// <param name="name">The column name</param>
        /// <param name="binding">The data binding</param>
        /// <param name="title">The title (optional)</param>
        /// <param name="alignment">The alignment (optional)</param>
        /// <param name="importance">The importance (optional)</param>
        public TableColumnDefinition
            (
                string name,
                DataBinding binding,
                string title = null,
                ColumnAlignment alignment = default(ColumnAlignment),
                ColumnImportance importance = default(ColumnImportance)
            )
        {
            Validate.IsNotEmpty(name);
            Validate.IsNotNull(binding);

            this.ColumnId = Guid.NewGuid();
            this.Name = name;
            this.Binding = binding;
            this.Alignment = alignment;
            this.Importance = importance;

            if (String.IsNullOrEmpty(title))
            {
                this.Title = name;
            }
            else
            {
                this.Title = title;
            }
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
        /// Gets the columns data binding
        /// </summary>
        public DataBinding Binding { get; protected set; }

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
