namespace Reportr.Graphics
{
    using System;
    using System.Drawing;
    
    /// <summary>
    /// Represents a single graphic overlay
    /// </summary>
    public class GraphicOverlay
    {
        /// <summary>
        /// Constructs the overlay with the position coordinates
        /// </summary>
        /// <param name="x">The X position</param>
        /// <param name="y">The Y position</param>
        public GraphicOverlay
            (
                double x,
                double y
            )
        {
            this.PositionX = x;
            this.PositionY = y;
        }

        /// <summary>
        /// Constructs the overlay with the coordinates and image
        /// </summary>
        /// <param name="x">The X position</param>
        /// <param name="y">The Y position</param>
        /// <param name="image">The image</param>
        public GraphicOverlay
            (
                double x,
                double y,
                Image image
            )

            : this(x, y)
        {
            Validate.IsNotNull(image);

            this.Image = image;
        }

        /// <summary>
        /// Gets the X display position
        /// </summary>
        public double PositionX { get; private set; }

        /// <summary>
        /// Gets the Y display position
        /// </summary>
        public double PositionY { get; private set; }

        /// <summary>
        /// Gets the image to display
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// Adds the image dimension values to the overlay
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>The updated graphic overlay</returns>
        public GraphicOverlay WithDimensions
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

        /// <summary>
        /// Adds a label to the graphic overlay
        /// </summary>
        /// <param name="label">The label text</param>
        /// <param name="backgroundColor">The background color (optional)</param>
        /// <param name="foregroundColor">The foreground color (optional)</param>
        /// <returns>The updated graphic overlay</returns>
        public GraphicOverlay WithLabel
            (
                string label,
                Color? backgroundColor = null,
                Color? foregroundColor = null
            )
        {
            Validate.IsNotEmpty(label);

            this.Label = label;
            this.BackgroundColor = backgroundColor;
            this.ForegroundColor = foregroundColor;

            return this;
        }

        /// <summary>
        /// Gets the label text to display
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// Gets the background colour
        /// </summary>
        public Color? BackgroundColor { get; private set; }

        /// <summary>
        /// Gets the foreground colour
        /// </summary>
        public Color? ForegroundColor { get; private set; }
    }
}
