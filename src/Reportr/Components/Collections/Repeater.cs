namespace Reportr.Components.Collections
{
    using Reportr.Data.Querying;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single report repeater
    /// </summary>
    public class Repeater : ReportComponentOutputBase, IEnumerable<RepeaterItem>
    {
        /// <summary>
        /// Constructs the repeater with the details
        /// </summary>
        /// <param name="definition">The repeater definition</param>
        /// <param name="results">The query results</param>
        /// <param name="type">The repeater type</param>
        /// <param name="items">The repeater items</param>
        public Repeater
            (
                RepeaterDefinition definition,
                QueryResults results,
                RepeaterType type,
                params RepeaterItem[] items
            )
            : base
            (
                definition,
                results
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(results);
            Validate.IsNotNull(items);

            this.RepeaterType = type;
            this.Items = items;
        }

        /// <summary>
        /// Gets the repeater type
        /// </summary>
        public RepeaterType RepeaterType { get; protected set; }

        /// <summary>
        /// Gets the items in the repeater
        /// </summary>
        public RepeaterItem[] Items { get; protected set; }

        /// <summary>
        /// Gets the item at the index specified
        /// </summary>
        /// <param name="index">The item index (zero based)</param>
        /// <returns>The matching repeater item</returns>
        public RepeaterItem this[int index]
        {
            get
            {
                return this.Items[index];
            }
        }

        /// <summary>
        /// Gets an enumerator for the collection of items
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<RepeaterItem> GetEnumerator()
        {
            return this.Items.ToList().GetEnumerator();
        }

        /// <summary>
        /// Gets a generic enumerator for the collection of items
        /// </summary>
        /// <returns>The generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
