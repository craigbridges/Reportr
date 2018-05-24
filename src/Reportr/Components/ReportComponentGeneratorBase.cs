namespace Reportr.Components
{
    using Nito.AsyncEx.Synchronous;
    using Reportr.Filtering;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a base class for report component generators
    /// </summary>
    public abstract class ReportComponentGeneratorBase : IReportComponentGenerator
    {
        /// <summary>
        /// Generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        public virtual IReportComponent Generate
            (
                IReportComponentDefinition definition,
                ReportSectionType sectionType,
                ReportFilter filter
            )
        {
            var task = GenerateAsync
            (
                definition,
                sectionType,
                filter
            );

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously generates a component from a report definition and filter
        /// </summary>
        /// <param name="definition">The component definition</param>
        /// <param name="sectionType">The report section type</param>
        /// <param name="filter">The report filter</param>
        /// <returns>The report component generated</returns>
        public abstract Task<IReportComponent> GenerateAsync
        (
            IReportComponentDefinition definition,
            ReportSectionType sectionType,
            ReportFilter filter
        );
    }
}
