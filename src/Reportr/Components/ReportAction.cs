namespace Reportr.Components
{
    /// <summary>
    /// Represents the output of a single report action
    /// </summary>
    public class ReportAction
    {
        /// <summary>
        /// Constructs the action output with the action details
        /// </summary>
        /// <param name="actionType">The action type</param>
        /// <param name="actionValue">The action value</param>
        public ReportAction
            (
                ReportActionType actionType,
                string actionValue
            )
        {
            this.ActionType = actionType;
            this.ActionValue = actionValue;
        }

        /// <summary>
        /// Gets the action type
        /// </summary>
        public ReportActionType ActionType { get; protected set; }

        /// <summary>
        /// Gets the action value
        /// </summary>
        public string ActionValue { get; protected set; }
    }
}
