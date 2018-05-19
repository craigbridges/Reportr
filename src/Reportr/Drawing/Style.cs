namespace Reportr.Drawing
{
    using System.Drawing;

    /// <summary>
    /// Represents the style of a single report item
    /// </summary>
    public class Style
    {
        /// <summary>
        /// Gets or sets the background color of the report item
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the border style
        /// </summary>
        public BorderStyle Border { get; set; }

        /// <summary>
        /// Gets or sets the foreground color of the report item
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the font of the report item
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets the color (stroke) representing the line color 
        /// of report items that support it, such as lines and shapes
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Gets or sets a line style used to define the line style of 
        /// report items that support it, such as lines and shapes
        /// </summary>
        public LineStyle LineStyle { get; set; }

        /// <summary>
        /// Gets or sets a Unit representing the line width of report 
        /// items that support it, such as lines and shapes
        /// </summary>
        public Unit LineWidth { get; set; }

        /// <summary>
        /// Gets or sets the padding of the report item
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of text in the report item
        /// </summary>
        public HorizontalAlign TextAlign { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment of text in the report item
        /// </summary>
        public VerticalAlign VerticalAlign { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the report item is displayed
        /// </summary>
        public bool Visible { get; set; }
    }
}
