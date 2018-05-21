namespace Reportr.Drawing
{
    /// <summary>
    /// Represents padding information associated with a report item
    /// </summary>
    public class Padding
    {
        /// <summary>
        /// Constructs the padding with default values
        /// </summary>
        public Padding()
        {
            this.Bottom = new Unit(0, UnitType.Pixel);
            this.Left = new Unit(0, UnitType.Pixel);
            this.Right = new Unit(0, UnitType.Pixel);
            this.Top = new Unit(0, UnitType.Pixel);
        }

        /// <summary>
        /// Gets or sets the padding value for the bottom edge
        /// </summary>
        public Unit Bottom { get; set; }

        /// <summary>
        /// Gets or sets the padding value for the left edge
        /// </summary>
        public Unit Left { get; set; }

        /// <summary>
        /// Gets or sets the padding value for the right edge
        /// </summary>
        public Unit Right { get; set; }

        /// <summary>
        /// Gets or sets the padding value for the top edge
        /// </summary>
        public Unit Top { get; set; }
    }
}
