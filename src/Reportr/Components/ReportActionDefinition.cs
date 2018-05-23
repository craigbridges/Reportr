namespace Reportr.Components
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents the default implementation of a report action definition
    /// </summary>
    public sealed class ReportActionDefinition
    {
        /// <summary>
        /// Constructs the report action with the details
        /// </summary>
        /// <param name="actionType">The action type</param>
        /// <param name="actionTemplate">The action template</param>
        public ReportActionDefinition
            (
                ReportActionType actionType,
                string actionTemplate
            )
        {
            this.ActionType = actionType;
            this.ActionTemplate = actionTemplate;
            this.TemplateBindings = new Collection<DataBinding>();
        }

        /// <summary>
        /// Gets the action type
        /// </summary>
        public ReportActionType ActionType { get; private set; }

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
        public string ActionTemplate { get; private set; }

        /// <summary>
        /// Gets a collection of data bindings for the template
        /// </summary>
        /// <remarks>
        /// The template bindings are used by the reporting engine
        /// as the argument values.
        /// </remarks>
        public ICollection<DataBinding> TemplateBindings { get; private set; }

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
            
            this.TemplateBindings.Add(binding);
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
            var binding = this.TemplateBindings.ElementAtOrDefault
            (
                index
            );

            if (binding == null)
            {
                throw new IndexOutOfRangeException
                (
                    "The binding index specified is out of range."
                );
            }

            this.TemplateBindings.Remove
            (
                binding
            );
        }

        /// <summary>
        /// Resolves the report action using the query row specified
        /// </summary>
        /// <param name="row">The query row</param>
        /// <returns>The action output</returns>
        public ReportAction Resolve
            (
                QueryRow row
            )
        {
            Validate.IsNotNull(row);

            if (this.TemplateBindings.Any())
            {
                var bindingValues = new List<object>();

                foreach (var binding in this.TemplateBindings)
                {
                    bindingValues.Add
                    (
                        binding.Resolve(row)
                    );
                }

                var formattedAction = String.Format
                (
                    this.ActionTemplate,
                    bindingValues.ToArray()
                );

                return new ReportAction
                (
                    this.ActionType,
                    formattedAction
                );
            }
            else
            {
                return new ReportAction
                (
                    this.ActionType,
                    this.ActionTemplate
                );
            }
        }
    }
}
