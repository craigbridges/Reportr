﻿namespace Reportr.Components.Metrics
{
    using Newtonsoft.Json;
    using Reportr.Data.Querying;
    using Reportr.Data.Querying.Functions;
    using Reportr.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the definition of a single report statistic
    /// </summary>
    public class StatisticDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the statistic definition with the details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="query">The query</param>
        /// <param name="function">The aggregate function</param>
        /// <param name="action">The action (optional)</param>
        public StatisticDefinition
            (
                string name,
                string title,
                IQuery query,
                IAggregateFunction function,
                ReportAction action = null
            )
            : base(name, title)
        {
            Validate.IsNotNull(query);
            Validate.IsNotNull(function);

            this.Query = query;
            this.Function = function;
            this.DefaultParameterValues = new Collection<ParameterValue>();
            this.Action = action;

            var defaultValues = query.CompileDefaultParameters();

            foreach (var value in defaultValues)
            {
                this.DefaultParameterValues.Add(value);
            }
        }

        /// <summary>
        /// Gets the statistic query
        /// </summary>
        [JsonIgnore]
        public IQuery Query { get; protected set; }

        /// <summary>
        /// Gets the statistic aggregate function
        /// </summary>
        [JsonIgnore]
        public IAggregateFunction Function { get; protected set; }

        /// <summary>
        /// Gets the default parameter values for the aggregator
        /// </summary>
        [JsonIgnore]
        public ICollection<ParameterValue> DefaultParameterValues
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public override IEnumerable<IQuery> GetQueriesUsed()
        {
            return new IQuery[]
            {
                this.Query
            };
        }

        /// <summary>
        /// Gets the lower range value
        /// </summary>
        public double? LowerRange { get; private set; }

        /// <summary>
        /// Gets the upper range value
        /// </summary>
        public double? UpperRange { get; private set; }

        /// <summary>
        /// Gets a flag indicating if the value has a range that it fits into
        /// </summary>
        public bool HasRange { get; private set; }

        /// <summary>
        /// Adds the range values to the statistic definition
        /// </summary>
        /// <param name="lower">The lower range</param>
        /// <param name="upper">The upper range</param>
        /// <returns>The updated statistic definition</returns>
        public StatisticDefinition WithRange
            (
                double? lower,
                double? upper
            )
        {
            if (lower == null && upper == null)
            {
                throw new InvalidOperationException
                (
                    "A lower or upper range value must be specified."
                );
            }

            this.LowerRange = lower;
            this.UpperRange = upper;
            this.HasRange = true;

            return this;
        }

        /// <summary>
        /// Gets the statistic action
        /// </summary>
        [JsonIgnore]
        public ReportAction Action { get; protected set; }

        /// <summary>
        /// Gets the component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Statistic;
            }
        }
    }
}
