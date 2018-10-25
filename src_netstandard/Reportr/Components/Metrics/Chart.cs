﻿namespace Reportr.Components.Metrics
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a single two-dimensional report chart
    /// </summary>
    [JsonObject]
    [DataContract]
    public class Chart : ReportComponentBase, IEnumerable<ChartDataSet>
    {
        /// <summary>
        /// Constructs the chart with the details
        /// </summary>
        /// <param name="definition">The chart definition</param>
        /// <param name="xAxisLabels">The x-axis labels</param>
        /// <param name="dataSets">The data sets</param>
        public Chart
            (
                ChartDefinition definition,
                IEnumerable<ChartAxisLabel> xAxisLabels,
                params ChartDataSet[] dataSets
            )
            : base(definition)
        {
            Validate.IsNotNull(xAxisLabels);
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
                    throw new ArgumentException
                    (
                        $"The set name '{name}' has already been used."
                    );
                }
            }

            this.XAxisLabels = xAxisLabels.ToArray();
            this.YAxisInterval = definition.YAxisInterval;
            this.DataSets = dataSets;
        }

        /// <summary>
        /// Gets an array of x-axis labels
        /// </summary>
        [DataMember]
        public ChartAxisLabel[] XAxisLabels { get; private set; }

        /// <summary>
        /// Gets the y-axis step interval
        /// </summary>
        [DataMember]
        public double? YAxisInterval { get; private set; }

        /// <summary>
        /// Gets an array of chart data sets
        /// </summary>
        [DataMember]
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
                throw new KeyNotFoundException
                (
                    $"No data set exists with the name '{name}'."
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