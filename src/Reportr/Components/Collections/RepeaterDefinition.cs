namespace Reportr.Components.Collections
{
    using Reportr.Data;
    using Reportr.Data.Querying;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a single report repeater definition
    /// </summary>
    public class RepeaterDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the repeater definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="query">The query</param>
        /// <param name="binding">The data binding</param>
        public RepeaterDefinition
            (
                string name,
                string title,
                IQuery query,
                DataBinding binding
            )
            : base(name, title)
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(binding);

            this.Query = query;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.Binding = binding;

            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }

        /// <summary>
        /// Gets the query that will supply the tables data
        /// </summary>
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the default parameter values for the query
        /// </summary>
        public ICollection<ParameterValue> DefaultParameterValues
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the data binding for the repeater
        /// </summary>
        public DataBinding Binding { get; protected set; }

        /// <summary>
        /// Adds the action and type to the repeater
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="type">The repeater type</param>
        /// <returns></returns>
        public RepeaterDefinition WithAction
            (
                ReportAction action,
                RepeaterType type
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;
            this.RepeaterType = type;

            return this;
        }

        /// <summary>
        /// Gets the action associated with the repeater
        /// </summary>
        public ReportAction Action { get; protected set; }

        /// <summary>
        /// Gets the repeater type
        /// </summary>
        public RepeaterType RepeaterType { get; protected set; }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Repeater;
            }
        }
    }
}
