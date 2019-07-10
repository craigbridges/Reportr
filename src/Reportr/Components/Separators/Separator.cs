namespace Reportr.Components.Separators
{
    using Reportr.Culture;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a report component separator
    /// </summary>
    [DataContract]
    public class Separator : ReportComponentBase
    {
        /// <summary>
        /// Constructs the report component with the details
        /// </summary>
        /// <param name="definition">The separator definition</param>
        public Separator
            (
                SeparatorDefinition definition
            )
            : base(definition)
        {
            this.Title = definition.Title;
            this.SeparatorType = definition.SeparatorType;
        }

        /// <summary>
        /// Gets the separator title
        /// </summary>
        [DataMember]
        public string Title { get; protected set; }

        /// <summary>
        /// Gets or sets the separator type
        /// </summary>
        [DataMember]
        public ReportComponentSeparatorType SeparatorType
        {
            get;
            protected set;
        }

        /// <summary>
        /// Translates the text in the component to the language specified
        /// </summary>
        /// <param name="translator">The translation dictionary</param>
        /// <param name="language">The language to translate into</param>
        public override void Translate
            (
                PhraseTranslationDictionary translator,
                Language language
            )
        {
            base.Translate(translator, language);

            this.Title = translator.Translate
            (
                this.Title,
                language
            );
        }
    }
}
