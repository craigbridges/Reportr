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
            
            this.UnitType = GetTypeFromString(typeDescriptor);
            this.Value = Convert.ToSingle(valueDescriptor);

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
                switch (type)
                {
                    case UnitType.Pixel:
                        break;

                    case UnitType.Point:
                        break;

                    case UnitType.Pica:
                        break;

                    case UnitType.Inch:
                        break;

                    case UnitType.Millimeter:
                        break;

                    case UnitType.Centimeter:
                        break;

                    case UnitType.Percentage:
                        break;
                }
            }

            throw new NotImplementedException();
        }
    }
}
