namespace Reportr.Registration
{
    using System;
    
    /// <summary>
    /// Represents a single registered report
    /// </summary>
    /// <remarks>
    /// A registered report is used to keep track of a report that
    /// can be generated through Reportr.
    /// </remarks>
    public class RegisteredReport
    {
        /// <summary>
        /// Constructs the registered report with its default configuration
        /// </summary>
        protected RegisteredReport() { }

        /// <summary>
        /// Constructs the registered report with basic details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        protected RegisteredReport
            (
                string name,
                string description
            )
        {
            Validate.IsNotNull(name);

            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Constructs the registered report with a builder
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="builder">The definition builder</param>
        public RegisteredReport
            (
                string name,
                string description,
                IReportDefinitionBuilder builder
            )

            : this(name, description)
        {
            Validate.IsNotNull(builder);

            this.SourceType = ReportDefinitionSourceType.Builder;
            this.BuilderTypeName = builder.GetType().Name;
        }

        /// <summary>
        /// Constructs the registered report with the script source code
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="scriptSourceCode">The script source code</param>
        public RegisteredReport
            (
                string name,
                string description,
                string scriptSourceCode
            )

            : this(name, description)
        {
            this.SourceType = ReportDefinitionSourceType.Script;
            this.ScriptSourceCode = scriptSourceCode;
        }

        /// <summary>
        /// Gets the unique ID of the registered report
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the date and time the registered report was created
        /// </summary>
        public DateTime DateCreated { get; protected set; }

        /// <summary>
        /// Gets the date and time the registered report was modified
        /// </summary>
        public DateTime DateModified { get; protected set; }

        /// <summary>
        /// Gets the name of the registered report
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets a description of the report
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets the report definition source type
        /// </summary>
        public ReportDefinitionSourceType SourceType { get; protected set; }

        /// <summary>
        /// Gets the report definition builder type name
        /// </summary>
        public string BuilderTypeName { get; protected set; }

        /// <summary>
        /// Gets the source code of the report definition script
        /// </summary>
        public string ScriptSourceCode { get; protected set; }
    }
}
