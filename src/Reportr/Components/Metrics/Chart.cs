namespace Reportr.Components.Metrics
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single two-dimensional chart
    /// </summary>
    public class Chart : ReportComponentOutputBase, IEnumerable<ChartDataSet>
    {
        /// <summary>
        /// Constructs the chart with the details
        /// </summary>
        /// <param name="chart">The chart definition</param>
        /// <param name="results">The query results</param>
        public Chart
            (
                ChartDefinition chart,
                QueryResults results
            )
            : base
            (
                chart,
                results
            )
        {
            Validate.IsNotNull(chart);

            this.DataSets = new ChartDataSet[] { };
        }

        /// <summary>
        /// Gets an array of chart data sets
        /// </summary>
        public ChartDataSet[] DataSets { get; private set; }

        /// <summary>
        /// Adds the data sets to the chart
        /// </summary>
        /// <param name="dataSets">The data sets</param>
        /// <returns>The updated chart</returns>
        public Chart WithDataSets
            (
                params ChartDataSet[] dataSets
            )
        {
            Validate.IsNotNull(dataSets);

            if (false == dataSets.Any())
            {
                throw new ArgumentException
                (
                    "At least one data set is required to create a chart."
                );
            }

            foreach (var set in dataSets)
            {
                var name = set.Name;

                var matchCount = dataSets.Count
                (
                    s => s.Name.Trim().ToLower() == name.Trim().ToLower()
                );

                if (matchCount > 1)
                {
                    var message = "The set name '{0}' has already been used.";

                    throw new ArgumentException
                    (
                        String.Format
                        (
                            message,
                            name
                        )
                    );
                }
            }

            this.DataSets = dataSets;

            return this;
        }

        /// <summary>
        /// Gets a single data set from the chart result
        /// </summary>
        /// <param name="name">The data set name</param>
        /// <returns>The matching data set</returns>
        public ChartDataSet GetSet
            (
                string name
            )
        {
            Validate.IsNotEmpty(name);

            var set = this.DataSets.FirstOrDefault
            (
                s => s.Name.Trim().ToLower() == name.Trim().ToLower()
            );

            if (set == null)
            {
                var message = "No data set exists with the name '{0}'.";

                throw new KeyNotFoundException
                (
                    String.Format
                    (
                        message,
                        name
                    )
                );
            }

            return set;
        }

        /// <summary>
        /// Gets the data set at the index specified
        /// </summary>
        /// <param name="index">The data set index (zero based)</param>
        /// <returns>The matching data set</returns>
        public ChartDataSet this[int index]
        {
            get
            {
                return this.DataSets[index];
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of data sets
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<ChartDataSet> GetEnumerator()
        {
            return this.DataSets.ToList().GetEnumerator();
        }

        /// <summary>
        /// Gets a generic enumerator for the collection of data sets
        /// </summary>
        /// <returns>The generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}