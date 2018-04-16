namespace Reportr.Components.Metrics
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for a repository that manages queries
    /// </summary>
    public interface IStatisticRepository
    {
        /// <summary>
        /// Adds a statistic to the repository
        /// </summary>
        /// <param name="statistic">The statistic</param>
        void AddStatistic
        (
            IStatistic statistic
        );

        /// <summary>
        /// Gets all statistics in the repository
        /// </summary>
        /// <returns>A collection of statistics</returns>
        IEnumerable<IStatistic> GetAllStatistics();

        /// <summary>
        /// Gets a single statistic by name from the repository
        /// </summary>
        /// <param name="name">The statistic name</param>
        /// <returns>The matching statistic</returns>
        IStatistic GetStatistic
        (
            string name
        );
    }
}
