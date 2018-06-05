namespace Reportr.Components.Collections
{
    /// <summary>
    /// Defines a contract for a sortable component
    /// </summary>
    public interface ISortableComponent
    {
        /// <summary>
        /// Gets or sets a flag indicating if column sorting is disabled
        /// </summary>
        bool DisableSorting { get; set; }

        /// <summary>
        /// Gets an array of sortable columns
        /// </summary>
        string[] SortableColumns { get; }
    }
}
