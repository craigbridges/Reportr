namespace Reportr.Drawing
{
    using System;
    using System.Drawing;

    /// <summary>
    /// A structure representing a unit of measure
    /// </summary>
    public struct Unit : IComparable, IComparable<Unit>
    {
        private bool _initialized;

        /// <summary>
        /// Gets or sets the default unit type for units of measure
        /// </summary>
        public static UnitType DefaultType { get; set; }

        /// <summary>
        /// Gets a double value representing the DPI of the current graphics context
        /// </summary>
        public static double DotsPerInch
        {
            get
            {
                using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    return graphics.DpiX;
                }
            }
        }

        /// <summary>
        /// Constructs the unit with the default unit type and a value
        /// </summary>
        /// <param name="value">The unit value</param>
        public Unit
            (
                double value
            )
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The unit value cannot be less than zero."
                );
            }

            this.UnitType = Unit.DefaultType;
            this.Value = Convert.ToSingle(value);

            _initialized = true;
        }

        /// <summary>
        /// Constructs the unit with the a unit type and value
        /// </summary>
        /// <param name="value">The unit value</param>
        /// <param name="unitType">The unit type</param>
        public Unit
            (
                double value,
                UnitType unitType
            )
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The unit value cannot be less than zero."
                );
            }

            this.Value = Convert.ToSingle(value);
            this.UnitType = unitType;

            _initialized = true;
        }

        /// <summary>
        /// Constructs the unit with the default unit type and a value
        /// </summary>
        /// <param name="value">The unit value</param>
        /// <remarks>
        /// Parameters passed to this constructor should be of the form 
        /// "2px", "3.4in", "4cm", "2mm", "12.5pt", "4pc", etc.
        /// </remarks>
        public Unit
            (
                string value
            )
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException
                (
                    "The unit value cannot be empty."
                );
            }

            if (value.Length < 3)
            {
                throw new ArgumentException
                (
                    "The unit value cannot contain less than three characters."
                );
            }

            var typeDescriptor = value.Substring
            (
                (value.Length - 3),
                2
            );

            var valueDescriptor = value.Substring
            (
                0,
                value.Length - 4
            );

            var convertedValue = Convert.ToSingle
            (
                valueDescriptor
            );

            if (convertedValue < 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The unit value cannot be less than zero."
                );
            }

            this.UnitType = GetTypeFromString(typeDescriptor);
            this.Value = convertedValue;

            _initialized = true;
        }

        /// <summary>
        /// Gets the unit type from a type descriptor
        /// </summary>
        /// <param name="typeDescriptor">The type descriptor</param>
        /// <returns>The unit type</returns>
        private static UnitType GetTypeFromString
            (
                string typeDescriptor
            )
        {
            switch (typeDescriptor)
            {
                case "px":
                    return UnitType.Pixel;
                    
                case "pt":
                    return UnitType.Point;
                    
                case "pc":
                    return UnitType.Pica;
                    
                case "in":
                    return UnitType.Inch;
                    
                case "mm":
                    return UnitType.Millimeter;
                    
                case "cm":
                    return UnitType.Centimeter;
                    
                case "pe":
                    return UnitType.Percentage;
                    
                default:
                    throw new ArgumentOutOfRangeException
                    (
                        String.Format
                        (
                            "The unit type '{0}' is not supported.",
                            typeDescriptor
                        )
                    );
            }
        }

        /// <summary>
        /// Gets the string from a unit type
        /// </summary>
        /// <param name="type">The unit type</param>
        /// <returns>The string representation</returns>
        private static string GetStringFromType
            (
                UnitType type
            )
        {
            switch (type)
            {
                case UnitType.Pixel:
                    return "px";

                case UnitType.Point:
                    return "pt";

                case UnitType.Pica:
                    return "pc";

                case UnitType.Inch:
                    return "in";

                case UnitType.Millimeter:
                    return "mm";

                case UnitType.Centimeter:
                    return "cm";

                case UnitType.Percentage:
                    return "pe";

                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Gets a flag indicating if the unit is uninitialized
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return false == _initialized;
            }
        }

        /// <summary>
        /// Gets the unit type
        /// </summary>
        public UnitType UnitType { get; private set; }

        /// <summary>
        /// Gets the magnitude of the unit
        /// </summary>
        public float Value { get; private set; }

        /// <summary>
        /// Compares the current instance with another object of the same type 
        /// and returns an integer that indicates whether the current instance 
        /// precedes, follows, or occurs in the same position in the sort order 
        /// as the other object.
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>The result</returns>
        public int CompareTo
            (
                object obj
            )
        {
            if (obj == null)
            {
                return -1;
            }
            else if (obj.GetType() != typeof(Unit))
            {
                return -1;
            }
            else
            {
                return CompareTo((Unit)obj);
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type 
        /// and returns an integer that indicates whether the current instance 
        /// precedes, follows, or occurs in the same position in the sort order 
        /// as the other object.
        /// </summary>
        /// <param name="other">The other unit</param>
        /// <returns>The result</returns>
        public int CompareTo
            (
                Unit other
            )
        {
            if (this.UnitType != other.UnitType)
            {
                other = other.ChangeType
                (
                    this.UnitType
                );
            }
            
            if (other.Value < this.Value)
            {
                return -1;
            }
            else if (other.Value == this.Value)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Generates a custom hash code for the unit
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            var tuple = Tuple.Create
            (
                this.Value,
                this.UnitType
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
            if (obj == null || false == (obj is Unit))
            {
                return false;
            }

            var u = (Unit)obj;

            // Compare internal values to avoid "defaulting" in the case of "Empty" 
            if (u.UnitType == this.UnitType && u.Value == this.Value)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares two unit instances to determine if they are equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>true, if both objects are equal; otherwise false</returns>
        public static bool operator ==(Unit left, Unit right)
        {
            return
            (
                left.UnitType == right.UnitType && left.Value == right.Value
            );
        }

        /// <summary>
        /// Compares two unit instances to determine if they are not equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>true, if both objects are not equal; otherwise false</returns>
        public static bool operator !=(Unit left, Unit right)
        {
            return
            (
                left.UnitType != right.UnitType || left.Value != right.Value
            );
        }

        /// <summary>
        /// Changes the unit type to the type specified
        /// </summary>
        /// <param name="type">The new unit type</param>
        /// <returns>The unit</returns>
        public Unit ChangeType
            (
                UnitType type
            )
        {
            if (type == this.UnitType)
            {
                return this;
            }
            else
            {
                if (this.UnitType == UnitType.Percentage)
                {
                    throw new InvalidOperationException
                    (
                        "Units cannot be converted from percentages."
                    );
                }

                if (type == UnitType.Percentage)
                {
                    throw new InvalidOperationException
                    (
                        "Units cannot be converted to percentages."
                    );
                }

                var oldValue = this.Value;
                var newValue = default(double);

                switch (this.UnitType)
                {
                    case UnitType.Pixel:
                        switch (type)
                        {
                            case UnitType.Point:
                                newValue = PixelToPoint(oldValue);
                                break;

                            case UnitType.Pica:
                                newValue = PixelToPica(oldValue);
                                break;

                            case UnitType.Inch:
                                newValue = PixelToInch(oldValue);
                                break;

                            case UnitType.Millimeter:
                                newValue = PixelToMm(oldValue);
                                break;

                            case UnitType.Centimeter:
                                newValue = PixelToCm(oldValue);
                                break;
                        }

                        break;

                    case UnitType.Point:
                        switch (type)
                        {
                            case UnitType.Pixel:
                                newValue = PointToPixel(oldValue);
                                break;

                            case UnitType.Pica:
                                newValue = PointToPica(oldValue);
                                break;

                            case UnitType.Inch:
                                newValue = PointToInch(oldValue);
                                break;

                            case UnitType.Millimeter:
                                newValue = PointToMm(oldValue);
                                break;

                            case UnitType.Centimeter:
                                newValue = PointToCm(oldValue);
                                break;
                        }

                        break;

                    case UnitType.Pica:
                        switch (type)
                        {
                            case UnitType.Pixel:
                                newValue = PicaToPixel(oldValue);
                                break;

                            case UnitType.Point:
                                newValue = PicaToPoint(oldValue);
                                break;

                            case UnitType.Inch:
                                newValue = PicaToInch(oldValue);
                                break;

                            case UnitType.Millimeter:
                                newValue = PicaToMm(oldValue);
                                break;

                            case UnitType.Centimeter:
                                newValue = PicaToCm(oldValue);
                                break;
                        }

                        break;

                    case UnitType.Inch:
                        switch (type)
                        {
                            case UnitType.Pixel:
                                newValue = InchToPixel(oldValue);
                                break;

                            case UnitType.Point:
                                newValue = InchToPoint(oldValue);
                                break;

                            case UnitType.Pica:
                                newValue = InchToPica(oldValue);
                                break;

                            case UnitType.Millimeter:
                                newValue = InchToMm(oldValue);
                                break;

                            case UnitType.Centimeter:
                                newValue = InchToCm(oldValue);
                                break;
                        }

                        break;

                    case UnitType.Millimeter:
                        switch (type)
                        {
                            case UnitType.Pixel:
                                newValue = MmToPixel(oldValue);
                                break;

                            case UnitType.Point:
                                newValue = MmToPoint(oldValue);
                                break;

                            case UnitType.Pica:
                                newValue = MmToPica(oldValue);
                                break;

                            case UnitType.Inch:
                                newValue = MmToInch(oldValue);
                                break;

                            case UnitType.Centimeter:
                                newValue = MmToCm(oldValue);
                                break;
                        }

                        break;

                    case UnitType.Centimeter:
                        switch (type)
                        {
                            case UnitType.Pixel:
                                newValue = CmToPixel(oldValue);
                                break;

                            case UnitType.Point:
                                newValue = CmToPoint(oldValue);
                                break;

                            case UnitType.Pica:
                                newValue = CmToPica(oldValue);
                                break;

                            case UnitType.Inch:
                                newValue = CmToInch(oldValue);
                                break;

                            case UnitType.Millimeter:
                                newValue = CmToMm(oldValue);
                                break;
                        }

                        break;
                }

                return new Unit(newValue, type);
            }
        }

        /// <summary>
        /// Converts pixels to millimeters
        /// </summary>
        /// <param name="value">The pixels value</param>
        /// <returns>The value in millimeters</returns>
        /// <remarks>
        /// There are 25.4mm to an inch.
        /// </remarks>
        private double PixelToMm
            (
                double value
            )
        {
            return (value * 25.4) / Unit.DotsPerInch;
        }

        /// <summary>
        /// Converts millimeters to pixels
        /// </summary>
        /// <param name="value">The millimeters value</param>
        /// <returns>The value in pixels</returns>
        /// <remarks>
        /// There are 25.4mm to an inch.
        /// </remarks>
        private double MmToPixel
            (
                double value
            )
        {
            return (value * Unit.DotsPerInch) / 25.4;
        }

        /// <summary>
        /// Converts pixels to centimeters
        /// </summary>
        /// <param name="value">The pixels value</param>
        /// <returns>The value in centimeters</returns>
        private double PixelToCm
            (
                double value
            )
        {
            var mm = PixelToMm(value);

            return mm / 10;
        }

        /// <summary>
        /// Converts centimeters to pixels
        /// </summary>
        /// <param name="value">The centimeters value</param>
        /// <returns>The value in pixels</returns>
        private double CmToPixel
            (
                double value
            )
        {
            return MmToPixel(value * 10);
        }

        /// <summary>
        /// Converts pixels to inches
        /// </summary>
        /// <param name="value">The pixels value</param>
        /// <returns>The value in inches</returns>
        private double PixelToInch
            (
                double value
            )
        {
            return value / Unit.DotsPerInch;
        }

        /// <summary>
        /// Converts inches to pixels
        /// </summary>
        /// <param name="value">The inches value</param>
        /// <returns>The value in pixels</returns>
        private double InchToPixel
            (
                double value
            )
        {
            return value * Unit.DotsPerInch;
        }

        /// <summary>
        /// Converts pixels to points
        /// </summary>
        /// <param name="value">The pixels value</param>
        /// <returns>The value in points</returns>
        /// <remarks>
        /// There are 72 points to an inch.
        /// </remarks>
        private double PixelToPoint
            (
                double value
            )
        {
            return (value * 72) / Unit.DotsPerInch;
        }

        /// <summary>
        /// Converts points to pixels
        /// </summary>
        /// <param name="value">The points value</param>
        /// <returns>The value in pixels</returns>
        /// <remarks>
        /// There are 72 points to an inch.
        /// </remarks>
        private double PointToPixel
            (
                double value
            )
        {
            return (value * Unit.DotsPerInch) / 72;
        }

        /// <summary>
        /// Converts pixels to picas
        /// </summary>
        /// <param name="value">The pixels value</param>
        /// <returns>The value in picas</returns>
        /// <remarks>
        /// There are 12 points to a pica and 6 picas in an inch.
        /// </remarks>
        private double PixelToPica
            (
                double value
            )
        {
            var points = PixelToPoint(value);
            
            return points / 12;
        }

        /// <summary>
        /// Converts picas to pixels
        /// </summary>
        /// <param name="value">The picas value</param>
        /// <returns>The value in pixels</returns>
        /// <remarks>
        /// There are 12 points to a pica and 6 picas in an inch.
        /// </remarks>
        private double PicaToPixel
            (
                double value
            )
        {
            var points = value * 12;

            return PointToPixel(points);
        }

        /// <summary>
        /// Converts points to picas
        /// </summary>
        /// <param name="value">The points value</param>
        /// <returns>The value in picas</returns>
        /// <remarks>
        /// There are 12 points to a pica.
        /// </remarks>
        private double PointToPica
            (
                double value
            )
        {
            return value / 12;
        }
        
        /// <summary>
        /// Converts points to picas
        /// </summary>
        /// <param name="value">The points value</param>
        /// <returns>The value in picas</returns>
        /// <remarks>
        /// There are 12 points to a pica.
        /// </remarks>
        private double PicaToPoint
            (
                double value
            )
        {
            return value * 12;
        }

        /// <summary>
        /// Converts points to inches
        /// </summary>
        /// <param name="value">The points value</param>
        /// <returns>The value in inches</returns>
        /// <remarks>
        /// There are 72 points to an inch.
        /// </remarks>
        private double PointToInch
            (
                double value
            )
        {
            return value * 72;
        }

        /// <summary>
        /// Converts inches to points
        /// </summary>
        /// <param name="value">The inches value</param>
        /// <returns>The value in points</returns>
        /// <remarks>
        /// There are 72 points to an inch.
        /// </remarks>
        private double InchToPoint
            (
                double value
            )
        {
            return value / 72;
        }

        /// <summary>
        /// Converts points to millimeters
        /// </summary>
        /// <param name="value">The points value</param>
        /// <returns>The value in millimeters</returns>
        /// <remarks>
        /// There are 25.4mm to an inch.
        /// </remarks>
        private double PointToMm
            (
                double value
            )
        {
            var inches = PointToInch(value);

            return inches / 25.4;
        }

        /// <summary>
        /// Converts millimeters to points
        /// </summary>
        /// <param name="value">The millimeters value</param>
        /// <returns>The value in points</returns>
        /// <remarks>
        /// There are 25.4mm to an inch.
        /// </remarks>
        private double MmToPoint
            (
                double value
            )
        {
            return InchToPoint(value * 25.4);
        }

        /// <summary>
        /// Converts points to centimeters
        /// </summary>
        /// <param name="value">The points value</param>
        /// <returns>The value in centimeters</returns>
        private double PointToCm
            (
                double value
            )
        {
            var mm = PointToMm(value);

            return mm / 10;
        }

        /// <summary>
        /// Converts centimeters to points
        /// </summary>
        /// <param name="value">The centimeters value</param>
        /// <returns>The value in points</returns>
        private double CmToPoint
            (
                double value
            )
        {
            return MmToPoint(value * 10);
        }

        /// <summary>
        /// Converts picas to inches
        /// </summary>
        /// <param name="value">The picas value</param>
        /// <returns>The value in inches</returns>
        /// <remarks>
        /// There are 12 points to a pica and 72 points to an inch.
        /// </remarks>
        private double PicaToInch
            (
                double value
            )
        {
            return (value * 12) / 72;
        }

        /// <summary>
        /// Converts inches to picas
        /// </summary>
        /// <param name="value">The inches value</param>
        /// <returns>The value in picas</returns>
        /// <remarks>
        /// There are 12 points to a pica and 72 points to an inch.
        /// </remarks>
        private double InchToPica
            (
                double value
            )
        {
            return (value * 72) / 12;
        }

        /// <summary>
        /// Converts picas to millimeters
        /// </summary>
        /// <param name="value">The picas value</param>
        /// <returns>The value in millimeters</returns>
        /// <remarks>
        /// There are 12 points to a pica.
        /// </remarks>
        private double PicaToMm
            (
                double value
            )
        {
            return PointToMm(value / 12);
        }

        /// <summary>
        /// Converts millimeters to picas
        /// </summary>
        /// <param name="value">The millimeters value</param>
        /// <returns>The value in picas</returns>
        /// <remarks>
        /// There are 12 points to a pica.
        /// </remarks>
        private double MmToPica
            (
                double value
            )
        {
            var points = MmToPoint(value);

            return points / 12;
        }

        /// <summary>
        /// Converts picas to centimeters
        /// </summary>
        /// <param name="value">The picas value</param>
        /// <returns>The value in centimeters</returns>
        private double PicaToCm
            (
                double value
            )
        {
            var mm = PicaToMm(value);

            return mm / 10;
        }

        /// <summary>
        /// Converts centimeters to picas
        /// </summary>
        /// <param name="value">The centimeters value</param>
        /// <returns>The value in picas</returns>
        private double CmToPica
            (
                double value
            )
        {
            return MmToPica(value * 10);
        }

        /// <summary>
        /// Converts inches to millimeters
        /// </summary>
        /// <param name="value">The inches value</param>
        /// <returns>The value in millimeters</returns>
        /// <remarks>
        /// There are 25.4mm to an inch.
        /// </remarks>
        private double InchToMm
            (
                double value
            )
        {
            return value * 25.4;
        }

        /// <summary>
        /// Converts millimeters to inches
        /// </summary>
        /// <param name="value">The millimeters value</param>
        /// <returns>The value in inches</returns>
        /// <remarks>
        /// There are 25.4mm to an inch.
        /// </remarks>
        private double MmToInch
            (
                double value
            )
        {
            return value / 25.4;
        }

        /// <summary>
        /// Converts inches to centimeters
        /// </summary>
        /// <param name="value">The inches value</param>
        /// <returns>The value in centimeters</returns>
        private double InchToCm
            (
                double value
            )
        {
            var mm = InchToMm(value);

            return value / 10;
        }

        /// <summary>
        /// Converts centimeters to inches
        /// </summary>
        /// <param name="value">The centimeters value</param>
        /// <returns>The value in inches</returns>
        private double CmToInch
            (
                double value
            )
        {
            return MmToInch(value * 10);
        }

        /// <summary>
        /// Converts millimeters to centimeters
        /// </summary>
        /// <param name="value">The millimeters value</param>
        /// <returns>The value in centimeters</returns>
        private double MmToCm
            (
                double value
            )
        {
            return value / 10;
        }

        /// <summary>
        /// Converts centimeters to millimeters
        /// </summary>
        /// <param name="value">The centimeters value</param>
        /// <returns>The value in millimeters</returns>
        private double CmToMm
            (
                double value
            )
        {
            return value * 10;
        }

        /// <summary>
        /// Returns a human-readable representation of the unit
        /// </summary>
        /// <returns>A string representing the unit</returns>
        public override string ToString()
        {
            if (this.UnitType == UnitType.Percentage)
            {
                return String.Format
                (
                    "{0}%",
                    this.Value
                );
            }
            else
            {
                var typeDescriptor = GetStringFromType
                (
                    this.UnitType
                );

                return String.Format
                (
                    "{0}{1}",
                    this.Value,
                    typeDescriptor
                );
            }
        }
    }
}
