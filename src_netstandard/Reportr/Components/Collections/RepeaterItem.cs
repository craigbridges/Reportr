namespace Reportr.Components.Collections
{
    /// <summary>
    /// Represents a single report repeater item
    /// </summary>
    public class RepeaterItem
    {
        /// <summary>
        /// Constructs the repeater item with the details
        /// </summary>
        /// <param name="value">The item value</param>
        /// <param name="action">The items action (optional)</param>
        /// <param name="components">The nested components</param>
        public RepeaterItem
            (
                object value,
                ReportAction action = null,
                params IReportComponent[] components
            )
        {
            this.Value = value;
            this.Action = action;

            if (components == null)
            {
                this.NestedComponents = new IReportComponent[] { };
            }
            else
            {
                this.NestedComponents = components;
            }
        }

        /// <summary>
        /// Gets the items value
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// Gets the items action
        /// </summary>
        public ReportAction Action { get; protected set; }

        /// <summary>
        /// Gets an array of nested report components
        /// </summary>
        public IReportComponent[] NestedComponents { get; protected set; }
    }
}
