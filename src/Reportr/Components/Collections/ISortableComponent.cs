namespace Reportr.Components.Collections
{
    /// <summary>
    /// Defines a contract for a sortable component
    /// </summary>
    public interface ISortableComponent
    {
        /// <summary>
        /// Gets or sets a flag indicating if column sorting is enabled
        /// </summary>
        bool EnableSorting { get; set; }

        /// <summary>
        /// Gets an array of sortable columns
        /// </summary>
        string[] SortableColumns { get; }
    }
}
