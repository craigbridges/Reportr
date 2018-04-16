namespace Reportr.Components.Graphics
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a single graphic result
    /// </summary>
    public class GraphicResult : ReportComponentOutput
    {
        /// <summary>
        /// Constructs the result with the graphic configuration
        /// </summary>
        /// <param name="graphic">The graphic that generated the result</param>
        /// <param name="executionTime">The execution time in milliseconds</param>
        /// <param name="success">True, if the query executed successfully</param>
        public GraphicResult
            (
                IGraphic graphic,
                int executionTime,
                bool success = true
            )
            : base
            (
                graphic,
                executionTime,
                success
            )
        {
            Validate.IsNotNull(graphic);

            this.Overlays = new GraphicOverlay[] { };
        }
        
        /// <summary>
        /// Adds the image and overlays to the result
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="overlays">The overlays</param>
        /// <returns>The updated graphic result</returns>
        public GraphicResult WithImage
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
        public GraphicResult WithAreas
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
        public GraphicResult WithDimensions
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
