namespace Reportr.Drawing
{
    /// <summary>
    /// Represents the style rules for a border
    /// </summary>
    public class BorderStyle
    {
        /// <summary>
        /// Gets or sets the default side style
        /// </summary>
        public BorderSideStyle Default { get; set; }

        /// <summary>
        /// Gets or sets the bottom side style
        /// </summary>
        public BorderSideStyle Bottom { get; set; }

        /// <summary>
        /// Gets or sets the left side style
        /// </summary>
        public BorderSideStyle Left { get; set; }

        /// <summary>
        /// Gets or sets the right side style
        /// </summary>
        public BorderSideStyle Right { get; set; }

        /// <summary>
        /// Gets or sets the top side style
        /// </summary>
        public BorderSideStyle Top { get; set; }
    }
}
