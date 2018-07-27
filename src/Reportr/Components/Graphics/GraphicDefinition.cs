namespace Reportr.Components.Graphics
{
    using Newtonsoft.Json;
    using Reportr.Components.Metrics;
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Represents a single report graphic definition
    /// </summary>
    public class GraphicDefinition : ReportComponentDefinitionBase
    {
        /// <summary>
        /// Constructs the graphic definition with the core details
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="image">The image</param>
        protected GraphicDefinition
            (
                string name,
                string title,
                Image image
            )
            : base(name, title)
        {
            Validate.IsNotNull(image);

            this.Image = image;
            this.Overlays = new GraphicOverlay[] { };
            this.OverlayStatistics = new Dictionary<Guid, List<StatisticDefinition>>();
            this.Areas = new GraphicArea[] { };
        }

        /// <summary>
        /// Constructs the graphic definition with overlays
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="image">The image</param>
        /// <param name="overlays">The overlays</param>
        public GraphicDefinition
            (
                string name,
                string title,
                Image image,
                params GraphicOverlay[] overlays
            )
            : this(name, title, image)
        {
            if (overlays != null)
            {
                this.Overlays = overlays;

                foreach (var overlay in overlays)
                {
                    this.OverlayStatistics.Add
                    (
                        overlay.OverlayId,
                        new List<StatisticDefinition>()
                    );
                }
            }
        }

        /// <summary>
        /// Constructs the graphic definition with areas
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="image">The image</param>
        /// <param name="areas">The areas</param>
        public GraphicDefinition
            (
                string name,
                string title,
                Image image,
                params GraphicArea[] areas
            )
            : this(name, title, image)
        {
            if (areas != null)
            {
                this.Areas = areas;
            }
        }

        /// <summary>
        /// Gets the image to display
        /// </summary>
        [JsonIgnore]
        public Image Image { get; protected set; }

        /// <summary>
        /// Gets an array of overlays to display over the graphic
        /// </summary>
        [JsonIgnore]
        public GraphicOverlay[] Overlays { get; protected set; }

        /// <summary>
        /// Gets a dictionary of overlay statistics
        /// </summary>
        [JsonIgnore]
        public Dictionary<Guid, List<StatisticDefinition>> OverlayStatistics
        {
            get;
            protected set;
        }

        /// <summary>
        /// Adds a statistic definition against an overlay
        /// </summary>
        /// <param name="overlay">The overlay</param>
        /// <param name="statistic">The statistic definition</param>
        public void AddOverlayStatistic
            (
                GraphicOverlay overlay,
                StatisticDefinition statistic
            )
        {
            Validate.IsNotNull(overlay);
            Validate.IsNotNull(statistic);

            var overlayId = overlay.OverlayId;
            var entry = this.OverlayStatistics[overlayId];

            var statisticAlreadyAdded = entry.Exists
            (
                s => s.ComponentId == statistic.ComponentId
            );

            if (statisticAlreadyAdded)
            {
                var message = "'{0}' has already been added to the overlay.";

                throw new InvalidOperationException
                (
                    String.Format
                    (
                        message,
                        statistic.Name
                    )
                );
            }
            else
            {
                entry.Add(statistic);
            }
        }

        /// <summary>
        /// Removes a statistic definition for an overlay
        /// </summary>
        /// <param name="overlay">The overlay</param>
        /// <param name="statistic">The statistic definition</param>
        public void RemoveOverlayStatistic
            (
                GraphicOverlay overlay,
                StatisticDefinition statistic
            )
        {
            Validate.IsNotNull(overlay);
            Validate.IsNotNull(statistic);

            var overlayId = overlay.OverlayId;

            var entryFound = this.OverlayStatistics.ContainsKey
            (
                overlayId
            );

            if (false == entryFound)
            {
                throw new KeyNotFoundException
                (
                    "The overlay specified was not found."
                );
            }
            else
            {
                var entry = this.OverlayStatistics[overlayId];

                if (false == entry.Contains(statistic))
                {
                    var message = "'{0}' has not been added to the overlay.";

                    throw new InvalidOperationException
                    (
                        String.Format
                        (
                            message,
                            statistic.Name
                        )
                    );
                }
                else
                {
                    entry.Remove(statistic);
                }
            }
        }

        /// <summary>
        /// Gets an array of areas to render over the graphic
        /// </summary>
        [JsonIgnore]
        public GraphicArea[] Areas { get; protected set; }

        /// <summary>
        /// Adds the image dimension values to the result
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>The updated graphic result</returns>
        public GraphicDefinition WithDimensions
            (
                double width,
                double height
            )
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The width must be greater than zero."
                );
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The height must be greater than zero."
                );
            }

            this.Width = width;
            this.Height = height;

            return this;
        }

        /// <summary>
        /// Gets the image width
        /// </summary>
        public double? Width { get; protected set; }

        /// <summary>
        /// Gets the image height
        /// </summary>
        public double? Height { get; protected set; }

        /// <summary>
        /// Gets a collection of all queries being used by the component
        /// </summary>
        /// <returns>A collection of queries</returns>
        public override IEnumerable<IQuery> GetQueriesUsed()
        {
            foreach (var item in this.OverlayStatistics)
            {
                foreach (var statistic in item.Value)
                {
                    yield return statistic.Query;
                }
            }
        }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Graphic;
            }
        }
    }
}
