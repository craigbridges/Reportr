namespace Reportr.Components
{
    using Reportr.Data;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a base class for all report action implementations
    /// </summary>
    public abstract class ReportActionBase : IReportAction
    {
        /// <summary>
        /// Constructs the report action with the type and template
        /// </summary>
        /// <param name="actionType">The action type</param>
        /// <param name="actionTemplate">The action template</param>
        public ReportActionBase
            (
                ReportActionType actionType,
                string actionTemplate
            )
        {
            Validate.IsNotEmpty(actionTemplate);

            this.ActionType = actionType;
            this.ActionTemplate = actionTemplate;
            this.TemplateBindings = new Collection<DataBinding>();
        }

        /// <summary>
        /// Gets the action type
        /// </summary>
        public ReportActionType ActionType { get; protected set; }

        /// <summary>
        /// Gets the action template
        /// </summary>
        public string ActionTemplate { get; protected set; }

        /// <summary>
        /// Gets a collection of data bindings for the template
        /// </summary>
        public ICollection<DataBinding> TemplateBindings { get; protected set; }

        /// <summary>
        /// Adds an action template binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        public void AddTemplateBinding
            (
                DataBinding binding
            )
        {
            Validate.IsNotNull(binding);

            this.TemplateBindings.Add
            (
                binding
            );
        }

        /// <summary>
        /// Removes an action template binding
        /// </summary>
        /// <param name="index">The index of the binding to remove</param>
        /// <remarks>
        /// The index numbers of zero-based.
        /// </remarks>
        public void RemoveTemplateBinding
            (
                int index
            )
        {
            var binding = this.TemplateBindings.ElementAt
            (
                index
            );

            this.TemplateBindings.Remove
            (
                binding
            );
        }
    }
}
