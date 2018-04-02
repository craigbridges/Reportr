namespace Reportr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Represents a single report section
    /// </summary>
    public class ReportSection
    {
        /// <summary>
        /// Gets the sections name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the name of the template assigned to the section
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Gets the sections title
        /// </summary>
        string Title { get; }

        // column span (e.g. 2 columns of the 4 in each row)
        // component (e.g. chart, statistic or query)
        // templates (for html, pdf, csv etc)
    }
}
