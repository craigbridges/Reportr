namespace Reportr.Registration
{
    using Reportr.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a single registered report
    /// </summary>
    /// <remarks>
    /// A registered report is used to keep track of a report that
    /// can be generated through Reportr.
    /// </remarks>
    public class RegisteredReport : IAggregate
    {
        private readonly bool _constructing;

        /// <summary>
        /// Constructs the registered report with its default configuration
        /// </summary>
        protected RegisteredReport() { }

        /// <summary>
        /// Constructs the registered report with basic details
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        protected RegisteredReport
            (
                RegisteredReportConfiguration configuration
            )
        {
            _constructing = true;

            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
            this.SourceRevisions = new Collection<RegisteredReportSourceRevision>();

            Configure(configuration);
        }

        /// <summary>
        /// Constructs the registered report with a builder
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        /// <param name="builderType">The definition builder type</param>
        internal RegisteredReport
            (
                RegisteredReportConfiguration configuration,
                Type builderType
            )

            : this(configuration)
        {
            SpecifyBuilder(builderType);

            _constructing = false;
        }

        /// <summary>
        /// Constructs the registered report with the script source code
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        /// <param name="scriptSourceCode">The script source code</param>
        internal RegisteredReport
            (
                RegisteredReportConfiguration configuration,
                string scriptSourceCode
            )

            : this(configuration)
        {
            SpecifySource(scriptSourceCode);

            _constructing = false;
        }

        /// <summary>
        /// Gets the unique ID of the registered report
        /// </summary>
        [Key]
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
        /// Configures the registered report
        /// </summary>
        /// <param name="configuration">The report configuration</param>
        public void Configure
            (
                RegisteredReportConfiguration configuration
            )
        {
            Validate.IsNotNull(configuration);

            if (String.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new ArgumentException
                (
                    "The report name is required."
                );
            }

            if (String.IsNullOrWhiteSpace(configuration.Title))
            {
                throw new ArgumentException
                (
                    "The report title is required."
                );
            }

            this.Name = configuration.Name;
            this.Title = configuration.Title;
            this.Description = configuration.Description;

            this.DateModified = DateTime.UtcNow;
            this.Version++;
        }

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
        /// Gets the report definition builder assembly name
        /// </summary>
        public string BuilderAssemblyName { get; protected set; }

        /// <summary>
        /// Gets the report definition builder assembly location
        /// </summary>
        public string BuilderAssemblyLocation { get; protected set; }

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
        /// <param name="builderType">The definition builder type</param>
        internal void SpecifyBuilder
            (
                Type builderType
            )
        {
            Validate.IsNotNull(builderType);

            var isValidype = builderType.ImplementsInterface
            (
                typeof(IReportDefinitionBuilder)
            );

            if (false == isValidype)
            {
                var foundName = builderType.Name;
                var expectedName = nameof(IReportDefinitionBuilder);

                throw new InvalidOperationException
                (
                    $"The type {foundName} does not implement {expectedName}."
                );
            }

            if (false == _constructing)
            {
                this.SourceRevisions.Add
                (
                    new RegisteredReportSourceRevision(this)
                );
            }
            
            this.SourceType = ReportDefinitionSourceType.Builder;
            this.BuilderTypeName = builderType.Name;
            this.BuilderTypeFullName = builderType.FullName;
            this.BuilderTypeAssemblyQualifiedName = builderType.AssemblyQualifiedName;
            this.BuilderAssemblyName = builderType.Assembly.FullName;
            this.BuilderAssemblyLocation = builderType.Assembly.Location;
            this.ScriptSourceCode = null;
            this.DateSourceSpecified = DateTime.UtcNow;

            if (false == _constructing)
            {
                this.DateModified = DateTime.UtcNow;
                this.Version++;
            }
        }

        /// <summary>
        /// Specifies the report definition source as a script
        /// </summary>
        /// <param name="scriptSourceCode">The script source code</param>
        internal void SpecifySource
            (
                string scriptSourceCode
            )
        {
            if (false == _constructing)
            {
                this.SourceRevisions.Add
                (
                    new RegisteredReportSourceRevision(this)
                );
            }

            this.SourceType = ReportDefinitionSourceType.Script;
            this.ScriptSourceCode = scriptSourceCode;
            this.BuilderTypeName = null;
            this.DateSourceSpecified = DateTime.UtcNow;

            if (false == _constructing)
            {
                this.DateModified = DateTime.UtcNow;
                this.Version++;
            }
        }

        /// <summary>
        /// Gets a flag indicating if the report is disabled
        /// </summary>
        public bool Disabled { get; protected set; }

        /// <summary>
        /// Gets the date and time the report was disabled
        /// </summary>
        public DateTime? DateDisabled { get; protected set; }

        /// <summary>
        /// Gets the date and time the report was enabled
        /// </summary>
        public DateTime? DateEnabled { get; protected set; }

        /// <summary>
        /// Disables the registered report
        /// </summary>
        internal void Disable()
        {
            if (this.Disabled)
            {
                throw new InvalidOperationException
                (
                    "The report has already been disabled."
                );
            }

            this.Disabled = true;
            this.DateDisabled = DateTime.UtcNow;
        }

        /// <summary>
        /// Enables the registered report
        /// </summary>
        internal void Enable()
        {
            if (false == this.Disabled)
            {
                throw new InvalidOperationException
                (
                    "The report has not been disabled."
                );
            }

            this.Disabled = false;
            this.DateEnabled = DateTime.UtcNow;
        }

        /// <summary>
        /// Translates the registered report to the language specified
        /// </summary>
        /// <param name="translator">The translation dictionary</param>
        /// <param name="language">The language to translate into</param>
        public void Translate
            (
                PhraseTranslationDictionary translator,
                Language language
            )
        {
            Validate.IsNotNull(translator);
            Validate.IsNotNull(language);

            this.Title = translator.Translate
            (
                this.Title,
                language
            );

            this.Description = translator.Translate
            (
                this.Description,
                language
            );
        }
    }
}
