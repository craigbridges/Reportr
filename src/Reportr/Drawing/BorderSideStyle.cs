namespace Reportr.Drawing
{
    using System.Drawing;

    /// <summary>
    /// Represents the style of a single border side
    /// </summary>
    public class BorderSideStyle
    {
        /// <summary>
        /// Constructs the border style with default values
        /// </summary>
        public BorderSideStyle()
        {
            this.Width = new Unit(0, UnitType.Pixel);
        }

        /// <summary>
        /// Gets or sets the border side color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the border type
        /// </summary>
        public BorderType Type { get; set; }

        /// <summary>
        /// Gets or sets the width of the border side
        /// </summary>
        public Unit Width { get; set; }
    }
}
