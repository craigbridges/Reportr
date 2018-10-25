namespace Reportr.Drawing
{
    using System;

    /// <summary>
    /// Stores an ordered pair of units, typically the width and height of a rectangle
    /// </summary>
    public struct SizeU
    {
        /// <summary>
        /// Constructs the size with the width and height
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public SizeU
            (
                Unit width,
                Unit height
            )
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Constructs the size with width, height and unit type
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="unitType">The unit type</param>
        public SizeU
            (
                double width,
                double height,
                UnitType unitType
            )
        {
            this.Width = new Unit
            (
                width,
                unitType
            );

            this.Height = new Unit
            (
                height,
                unitType
            );
        }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public Unit Width { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        public Unit Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether the size has zero width and height
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return
                (
                    this.Width.IsEmpty && this.Height.IsEmpty
                );
            }
        }
        
        /// <summary>
        /// Generates a custom hash code for the size
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            var tuple = Tuple.Create
            (
                this.Width,
                this.Height
            );

            return tuple.GetHashCode();
        }

        /// <summary>
        /// Determines if an object is equal to the current unit instance
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public override bool Equals
            (
                object obj
            )
        {
            if (obj is null || false == (obj is SizeU))
            {
                return false;
            }
            
            return (SizeU)obj == this;
        }
        
        /// <summary>
        /// Tests whether two sizes are equal
        /// </summary>
        /// <param name="size1">The first size</param>
        /// <param name="size2">The second size</param>
        /// <returns>True, if both sizes are equal; otherwise false</returns>
        public static bool operator ==
            (
                SizeU size1,
                SizeU size2
            )
        {
            return
            (
                size1.Width == size2.Width && size1.Height == size2.Height
            );
        }

        /// <summary>
        /// Tests whether two sizes are not equal
        /// </summary>
        /// <param name="size1">The first size</param>
        /// <param name="size2">The second size</param>
        /// <returns>True, if both sizes are not equal; otherwise false</returns>
        public static bool operator !=
            (
                SizeU size1,
                SizeU size2
            )
        {
            return
            (
                size1.Width != size2.Width || size1.Height != size2.Height
            );
        }

        /// <summary>
        /// Adds the width and height of one size to the width and height of another
        /// </summary>
        /// <param name="size1">The first size</param>
        /// <param name="size2">The second size</param>
        /// <returns>A size representing the result of the addition</returns>
        public static SizeU operator +
            (
                SizeU size1,
                SizeU size2
            )
        {
            var totalWidth = (size1.Width + size2.Width);
            var totalHeight = (size2.Height + size2.Height);

            return new SizeU
            (
                totalWidth,
                totalHeight
            );
        }

        /// <summary>
        /// Subtracts the width and height of one size from the width and height of another
        /// </summary>
        /// <param name="size1">The first size</param>
        /// <param name="size2">The second size</param>
        /// <returns>A size representing the result of the subtraction</returns>
        public static SizeU operator -
            (
                SizeU size1,
                SizeU size2
            )
        {
            var totalWidth = (size1.Width - size2.Width);
            var totalHeight = (size2.Height - size2.Height);

            return new SizeU
            (
                totalWidth,
                totalHeight
            );
        }

        /// <summary>
        /// Divides a size by a specified value
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="value">The value to divide by</param>
        /// <returns>A size that is the quotient of size and value</returns>
        public static SizeU operator /
            (
                SizeU size,
                double value
            )
        {
            var newWidth = (size.Width / value);
            var newHeight = (size.Height / value);

            return new SizeU
            (
                newWidth,
                newHeight
            );
        }

        /// <summary>
        /// Multiplies  a size by a specified value
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="value">The value to multiply by</param>
        /// <returns>A size that is the product of size and value</returns>
        public static SizeU operator *
            (
                SizeU size,
                double value
            )
        {
            var newWidth = (size.Width * value);
            var newHeight = (size.Height * value);

            return new SizeU
            (
                newWidth,
                newHeight
            );
        }
    }
}
