namespace Reportr.Components.Metrics
{
    /// <summary>
    /// Represents a report action implementation for statistics
    /// </summary>
    public class StatisticAction : ReportActionBase
    {
        /// <summary>
        /// Constructs the report action with the type and template
        /// </summary>
        /// <param name="actionType">The action type</param>
        /// <param name="actionTemplate">The action template</param>
        public StatisticAction
            (
                ReportActionType actionType,
                string actionTemplate
            )
            : base(actionType, actionTemplate)
        { }
    }
}
