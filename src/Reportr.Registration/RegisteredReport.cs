namespace Reportr.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a single registered report
    /// </summary>
    /// <remarks>
    /// A registered report is used to keep track of a report that
    /// can be generated through Reportr.
    /// </remarks>
    public class RegisteredReport : IAggregate
    {
        /// <summary>
        /// Constructs the registered report with its default configuration
        /// </summary>
        protected RegisteredReport() { }

        /// <summary>
        /// Constructs the registered report with basic details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        protected RegisteredReport
            (
                string name,
                string title,
                string description
            )
        {
            Validate.IsNotNull(name);
            Validate.IsNotNull(title);

            this.Id = Guid.NewGuid();
            this.Version = 1;
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;
            this.SourceRevisions = new Collection<RegisteredReportSourceRevision>();

            this.Name = name;
            this.Title = title;
            this.Description = description;
        }

        /// <summary>
        /// Constructs the registered report with a builder
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="builder">The definition builder</param>
        public RegisteredReport
            (
                string name,
                string title,
                string description,
                IReportDefinitionBuilder builder
            )

            : this(name, title, description)
        {
            SpecifySource(builder);
        }

        /// <summary>
        /// Constructs the registered report with the script source code
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="scriptSourceCode">The script source code</param>
        public RegisteredReport
            (
                string name,
                string title,
                string description,
                string scriptSourceCode
            )

            : this(name, title, description)
        {
            SpecifySource(scriptSourceCode);
        }

        /// <summary>
        /// Gets the unique ID of the registered report
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets the version number of the registered report
        /// </summary>
        public int Version { get; protected set; }

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
        /// Gets the title for the registered report
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Gets a description of the report
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets a collection of report source revisions
        /// </summary>
        public virtual ICollection<RegisteredReportSourceRevision> SourceRevisions
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the report definition source type
        /// </summary>
        public ReportDefinitionSourceType SourceType { get; protected set; }

        /// <summary>
        /// Gets the report definition builder type name
        /// </summary>
        public string BuilderTypeName { get; protected set; }

        /// <summary>
        /// Gets the report definition builder type full name
        /// </summary>
        public string BuilderTypeFullName { get; protected set; }

        /// <summary>
        /// Gets the report definition builder type assembly qualified name
        /// </summary>
        public string BuilderTypeAssemblyQualifiedName { get; protected set; }

        /// <summary>
        /// Gets the source code of the report definition script
        /// </summary>
        public string ScriptSourceCode { get; protected set; }

        /// <summary>
        /// Gets the date and time the definition source was specified
        /// </summary>
        public DateTime DateSourceSpecified { get; protected set; }

        /// <summary>
        /// Specifies the report definition source as a builder
        /// </summary>
        /// <param name="builder">The definition builder</param>
        public void SpecifySource
            (
                IReportDefinitionBuilder builder
            )
        {
            Validate.IsNotNull(builder);

            this.SourceRevisions.Add
            (
                new RegisteredReportSourceRevision(this)
            );

            var builderType = builder.GetType();

            this.SourceType = ReportDefinitionSourceType.Builder;
            this.BuilderTypeName = builderType.Name;
            this.BuilderTypeFullName = builderType.FullName;
            this.BuilderTypeAssemblyQualifiedName = builderType.AssemblyQualifiedName;
            this.ScriptSourceCode = null;
            this.DateSourceSpecified = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }

        /// <summary>
        /// Specifies the report definition source as a script
        /// </summary>
        /// <param name="scriptSourceCode">The script source code</param>
        public void SpecifySource
            (
                string scriptSourceCode
            )
        {
            this.SourceRevisions.Add
            (
                new RegisteredReportSourceRevision(this)
            );

            this.SourceType = ReportDefinitionSourceType.Script;
            this.ScriptSourceCode = scriptSourceCode;
            this.BuilderTypeName = null;
            this.DateSourceSpecified = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }
    }
}
