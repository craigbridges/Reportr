namespace Reportr.Components.Graphics
{
    using Reportr.Components.Metrics;
    using Reportr.Globalization;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a single report graphic
    /// </summary>
    [DataContract]
    public class Graphic : ReportComponentBase
    {
        /// <summary>
        /// Constructs the graphic with the details
        /// </summary>
        /// <param name="definition">The graphic definition</param>
        public Graphic
            (
                GraphicDefinition definition
            )
            : base(definition)
        {
            this.Image = definition.Image;
            this.Overlays = definition.Overlays;
            this.OverlayStatistics = new Dictionary<Guid, List<Statistic>>();

            this.Areas = definition.Areas;
            this.Width = definition.Width;
            this.Height = definition.Height;
        }
        
        /// <summary>
        /// Adds the image and overlays to the result
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="overlays">The overlays</param>
        /// <returns>The updated graphic result</returns>
        public Graphic WithImage
            (
                Image image,
                params GraphicOverlay[] overlays
            )
        {
            Validate.IsNotNull(image);
            Validate.IsNotNull(overlays);

            this.Image = image;
            this.Overlays = overlays;

            foreach (var overlay in overlays)
            {
                this.OverlayStatistics.Add
                (
                    overlay.OverlayId,
                    new List<Statistic>()
                );
            }

            return this;
        }

        /// <summary>
        /// Gets the image to display
        /// </summary>
        [DataMember]
        public Image Image { get; protected set; }

        /// <summary>
        /// Gets an array of overlays to display over the graphic
        /// </summary>
        [DataMember]
        public GraphicOverlay[] Overlays { get; protected set; }

        /// <summary>
        /// Gets a dictionary of overlay statistics
        /// </summary>
        [DataMember]
        public Dictionary<Guid, List<Statistic>> OverlayStatistics
        {
            get;
            protected set;
        }

        /// <summary>
        /// Adds a statistic against an overlay
        /// </summary>
        /// <param name="overlay">The overlay</param>
        /// <param name="statistic">The statistic</param>
        internal void AddOverlayStatistic
            (
                GraphicOverlay overlay,
                Statistic statistic
            )
        {
            Validate.IsNotNull(overlay);
            Validate.IsNotNull(statistic);

            var overlayId = overlay.OverlayId;
            var entry = this.OverlayStatistics[overlayId];

            entry.Add(statistic);
        }

        /// <summary>
        /// Adds a collection of areas to the result
        /// </summary>
        /// <param name="areas">The areas to add</param>
        /// <returns>The updated graphic result</returns>
        public Graphic WithAreas
            (
                GraphicArea[] areas
            )
        {
            Validate.IsNotNull(areas);

            this.Areas = areas;

            return this;
        }

        /// <summary>
        /// Gets an array of areas to render over the graphic
        /// </summary>
        [DataMember]
        public GraphicArea[] Areas { get; protected set; }

        /// <summary>
        /// Adds the image dimension values to the result
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>The updated graphic result</returns>
        public Graphic WithDimensions
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
        [DataMember]
        public double? Width { get; protected set; }

        /// <summary>
        /// Gets the image height
        /// </summary>
        [DataMember]
        public double? Height { get; protected set; }

        /// <summary>
        /// Translates the text in the component to the language specified
        /// </summary>
        /// <param name="translator">The translation dictionary</param>
        /// <param name="language">The language to translate into</param>
        public override void Translate
            (
                PhraseTranslationDictionary translator,
                Language language
            )
        {
            base.Translate(translator, language);

            foreach (var overlay in this.Overlays)
            {
                if (overlay.HasLabel)
                {
                    overlay.Label = translator.Translate
                    (
                        overlay.Label,
                        language
                    );
                }
            }

            foreach (var statistic in this.OverlayStatistics)
            {
                statistic.Value.ForEach
                (
                    s => s.Translate(translator, language)
                );
            }

            foreach (var area in this.Areas)
            {
                area.ToolTipText = translator.Translate
                (
                    area.ToolTipText,
                    language
                );
            }
        }
    }
}
