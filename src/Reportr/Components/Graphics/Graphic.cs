namespace Reportr.Components.Graphics
{
    using Reportr.Data.Querying;
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a single graphic
    /// </summary>
    public class Graphic : ReportComponentOutputBase
    {
        /// <summary>
        /// Constructs the graphic with the details
        /// </summary>
        /// <param name="definition">The graphic definition</param>
        /// <param name="results">The query results</param>
        public Graphic
            (
                IGraphic definition,
                QueryResults results
            )
            : base
            (
                definition,
                results
            )
        {
            Validate.IsNotNull(definition);
            Validate.IsNotNull(results);

            this.Overlays = new GraphicOverlay[] { };
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

            return this;
        }

        /// <summary>
        /// Gets the image to display
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// Gets an array of overlays to display over the graphic
        /// </summary>
        public GraphicOverlay[] Overlays { get; private set; }

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
        public GraphicArea[] Areas { get; private set; }

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
        public double? Width { get; private set; }

        /// <summary>
        /// Gets the image height
        /// </summary>
        public double? Height { get; private set; }
    }
}
