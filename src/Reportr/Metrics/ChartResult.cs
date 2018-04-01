namespace Reportr.Metrics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single two-dimensional chart result
    /// </summary>
    public class ChartResult : OutputResult
    {
        /// <summary>
        /// Constructs the chart with the chart configuration
        /// </summary>
        /// <param name="dataSets">The data sets</param>
        /// <param name="executionTime">The execution time in milliseconds</param>
        /// <param name="success">True, if the query executed successfully</param>
        /// <param name="errorMessage">The error message, if there was one</param>
        public ChartResult
            (
                IEnumerable<ChartDataSet> dataSets,
                int executionTime,
                bool success = true,
                string errorMessage = null
            )

            : base(executionTime, success, errorMessage)
        {
            Validate.IsNotNull(dataSets);

            AddDataSets(dataSets);
        }

        /// <summary>
        /// Adds the charts data sets against the result
        /// </summary>
        /// <param name="dataSets">The data sets</param>
        private void AddDataSets
            (
                IEnumerable<ChartDataSet> dataSets
            )
        {
            if (false == dataSets.Any())
            {
                throw new ArgumentException
                (
                    "At least one data set is required to create a chart."
                );
            }

            foreach (var set in dataSets.ToList())
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
                        String.Format(message, name)
                    );
                }
            }

            this.DataSets = dataSets.ToArray();
        }
        
        /// <summary>
        /// Gets an array of chart data sets
        /// </summary>
        public ChartDataSet[] DataSets { get; private set; }

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
                    String.Format(message, name)
                );
            }

            return set;
        }
    }
}
