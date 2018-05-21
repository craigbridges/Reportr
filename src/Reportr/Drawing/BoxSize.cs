namespace Reportr.Drawing
{
    /// <summary>
    /// Represents the width and height of an element
    /// </summary>
    public class BoxSize
    {
        /// <summary>
        /// Constructs the box size with default sizes
        /// </summary>
        public BoxSize()
        {
            this.Width = new Unit(100, UnitType.Percentage);
            this.Height = new Unit(100, UnitType.Percentage);
        }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public Unit Width { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        public Unit Height { get; set; }
    }
}
