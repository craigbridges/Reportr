namespace Reportr.Components.Separators
{
    /// <summary>
    /// Represents a report component separator
    /// </summary>
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
        public string Title { get; protected set; }

        /// <summary>
        /// Gets or sets the separator type
        /// </summary>
        public ReportComponentSeparatorType SeparatorType
        {
            get;
            protected set;
        }
    }
}
