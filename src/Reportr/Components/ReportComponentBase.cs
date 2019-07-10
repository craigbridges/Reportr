namespace Reportr.Components
{
    using Reportr.Culture;
    using Reportr.Drawing;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a base class for a report component
    /// </summary>
    public abstract class ReportComponentBase : IReportComponent
    {
        /// <summary>
        /// Constructs the report component with the details
        /// </summary>
        /// <param name="definition">The component definition</param>
        protected ReportComponentBase
            (
                IReportComponentDefinition definition
            )
        {
            Validate.IsNotNull(definition);

            this.ComponentDefinition = definition;
            this.Fields = definition.Fields;
            this.Style = definition.Style;
        }

        /// <summary>
        /// Gets the definition used to generate the component
        /// </summary>
        public IReportComponentDefinition ComponentDefinition
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a dictionary of component fields
        /// </summary
        /// <remarks>
        /// The fields are a component level collection of 
        /// name-values, where the value can be of any type.
        /// 
        /// Component fields can be used by report templates 
        /// for applying rendering logic. This way a template
        /// can conditionally render something based on the 
        /// state of a field.
        /// </remarks>
        public Dictionary<string, object> Fields
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the style information
        /// </summary>
        public Style Style { get; protected set; }

        /// <summary>
        /// Translates the text in the component to the language specified
        /// </summary>
        /// <param name="translator">The translation dictionary</param>
        /// <param name="language">The language to translate into</param>
        public virtual void Translate
            (
                PhraseTranslationDictionary translator,
                Language language
            )
        {
            Validate.IsNotNull(translator);
            Validate.IsNotNull(language);

            // NOTE: nothing to do for the base component
        }
    }
}
