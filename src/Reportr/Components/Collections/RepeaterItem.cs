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
        public RepeaterItem
            (
                object value,
                ReportActionOutput action = null
            )
        {
            this.Value = value;
            this.Action = action;
        }

        /// <summary>
        /// Gets the items value
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// Gets the items action
        /// </summary>
        public ReportActionOutput Action { get; protected set; }
    }
}
