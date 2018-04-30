namespace Reportr.Components
{
    using Reportr.Data;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a report action
    /// </summary>
    public interface IReportAction
    {
        /// <summary>
        /// Gets the action type
        /// </summary>
        ReportActionType ActionType { get; }

        /// <summary>
        /// Gets the action template
        /// </summary>
        /// <remarks>
        /// The action template is used by the reporting engine to 
        /// generate the action content for a specific query row.
        /// 
        /// The template syntax uses arguments in the same way as
        /// the String.Format method.
        /// 
        /// E.g. The template "http://url/{0}" specifies a single 
        /// argument and will be replaced with the first value in 
        /// the parameter bindings collection.
        /// </remarks>
        string ActionTemplate { get; }

        /// <summary>
        /// Gets a collection of data bindings for the template
        /// </summary>
        /// <remarks>
        /// The template bindings are used by the reporting engine
        /// as the argument values.
        /// </remarks>
        ICollection<DataBinding> TemplateBindings { get; }

        /// <summary>
        /// Adds an action template binding
        /// </summary>
        /// <param name="binding">The data binding</param>
        void AddTemplateBinding
        (
            DataBinding binding
        );

        /// <summary>
        /// Removes an action template binding
        /// </summary>
        /// <param name="index">The index of the binding to remove</param>
        /// <remarks>
        /// The index numbers of zero-based.
        /// </remarks>
        void RemoveTemplateBinding
        (
            int index
        );
    }
}
