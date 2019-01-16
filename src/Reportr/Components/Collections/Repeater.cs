namespace Reportr.Components.Collections
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a single report repeater
    /// </summary>
    [JsonObject]
    [DataContract]
    public class Repeater : ReportComponentBase, IEnumerable<RepeaterItem>
    {
        /// <summary>
        /// Constructs the repeater with the details
        /// </summary>
        /// <param name="definition">The repeater definition</param>
        /// <param name="items">The repeater items</param>
        public Repeater
            (
                RepeaterDefinition definition,
                params RepeaterItem[] items
            )
            : base(definition)
        {
            Validate.IsNotNull(items);

            this.RepeaterType = definition.RepeaterType;
            this.Items = items;
        }

        /// <summary>
        /// Gets the repeater type
        /// </summary>
        [DataMember]
        public RepeaterType RepeaterType { get; protected set; }

        /// <summary>
        /// Gets the items in the repeater
        /// </summary>
        [DataMember]
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
