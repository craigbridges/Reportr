namespace Reportr.Drawing
{
    using System;

    /// <summary>
    /// Represents an ordered pair of unit-based point x and 
    /// y-coordinates that defines a point in a two-dimensional plane
    /// </summary>
    public struct PointU
    {
        /// <summary>
        /// Constructs the point with the width and height
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y coordinate</param>
        public PointU
            (
                Unit x,
                Unit y
            )
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs the point with width, height and unit type
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="unitType">The unit type</param>
        public PointU
            (
                double x,
                double y,
                UnitType unitType
            )
        {
            this.X = new Unit
            (
                x,
                unitType
            );

            this.Y = new Unit
            (
                y,
                unitType
            );
        }

        /// <summary>
        /// Gets or sets the x-coordinate
        /// </summary>
        public Unit X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate
        /// </summary>
        public Unit Y { get; set; }

        /// <summary>
        /// Gets a value indicating whether the point has zero X and Y coordinates
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return
                (
                    this.X.IsEmpty && this.Y.IsEmpty
                );
            }
        }
        
        /// <summary>
        /// Generates a custom hash code for the point
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            var tuple = Tuple.Create
            (
                this.X,
                this.Y
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
            if (obj is null || false == (obj is PointU))
            {
                return false;
            }
            
            return (PointU)obj == this;
        }
        
        /// <summary>
        /// Tests whether two points are equal
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>True, if both points are equal; otherwise false</returns>
        public static bool operator ==
            (
                PointU point1,
                PointU point2
            )
        {
            return
            (
                point1.X == point2.X && point1.Y == point2.Y
            );
        }

        /// <summary>
        /// Tests whether two points are not equal
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>True, if both points are not equal; otherwise false</returns>
        public static bool operator !=
            (
                PointU point1,
                PointU point2
            )
        {
            return
            (
                point1.X != point2.X || point1.Y != point2.Y
            );
        }

        /// <summary>
        /// Adds the X and Y of one point to the X and Y of another
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>A point representing the result of the addition</returns>
        public static PointU operator +
            (
                PointU point1,
                PointU point2
            )
        {
            var newX = (point1.X + point2.X);
            var newY = (point2.Y + point2.Y);

            return new PointU
            (
                newX,
                newY
            );
        }

        /// <summary>
        /// Subtracts the X and Y of one point from the X and Y of another
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <returns>A point representing the result of the subtraction</returns>
        public static PointU operator -
            (
                PointU point1,
                PointU point2
            )
        {
            var newX = (point1.X - point2.X);
            var newY = (point2.Y - point2.Y);

            return new PointU
            (
                newX,
                newY
            );
        }

        /// <summary>
        /// Divides a point by a specified value
        /// </summary>
        /// <param name="point">The point</param>
        /// <param name="value">The value to divide by</param>
        /// <returns>A point that is the quotient of point and value</returns>
        public static PointU operator /
            (
                PointU point,
                double value
            )
        {
            var newX = (point.X / value);
            var newY = (point.Y / value);

            return new PointU
            (
                newX,
                newY
            );
        }

        /// <summary>
        /// Multiplies  a point by a specified value
        /// </summary>
        /// <param name="point">The point</param>
        /// <param name="value">The value to multiply by</param>
        /// <returns>A point that is the product of point and value</returns>
        public static PointU operator *
            (
                PointU point,
                double value
            )
        {
            var newX = (point.X * value);
            var newY = (point.Y * value);

            return new PointU
            (
                newX,
                newY
            );
        }
    }
}
