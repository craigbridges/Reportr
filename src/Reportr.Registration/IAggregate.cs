namespace Reportr.Registration
{
    using System;
    
    /// <summary>
    /// Defines a contract for an aggregate root
    /// </summary>
    public interface IAggregate
    {
        /// <summary>
        /// Gets the ID of the aggregate
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the version number of the aggregate
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Gets the data and time the aggregate was created
        /// </summary>
        DateTime DateCreated { get; }

        /// <summary>
        /// Gets the data and time the aggregate was modified
        /// </summary>
        DateTime DateModified { get; }
    }
}
