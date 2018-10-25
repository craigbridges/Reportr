namespace Reportr.Drawing
{
    using System;
    
    /// <summary>
    /// Provides various extension methods for units
    /// </summary>
    public static class UnitExtensions
    {
        /// <summary>
        /// Gets the unit as the unit type specified
        /// </summary>
        /// <param name="unit">The unit</param>
        /// <param name="type">The type</param>
        /// <returns>The unit as the specified type</returns>
        public static Unit GetAs
            (
                this Unit unit,
                UnitType type
            )
        {
            if (unit.IsEmpty || unit.UnitType == type)
            {
                return unit;
            }
            else
            {
                return unit.ChangeType(type);
            }
        }

        /// <summary>
        /// Compares two unit instances to determine if they are equal
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if both objects are equal; otherwise false</returns>
        public static bool Equals
            (
                this Unit left,
                Unit right
            )
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
        /// <returns>True, if both objects are not equal; otherwise false</returns>
        public static bool DoesNotEqual
            (
                this Unit left,
                Unit right
            )
        {
            return
            (
                left.UnitType != right.UnitType || left.Value != right.Value
            );
        }

        /// <summary>
        /// Compares two unit instances to determine if left is greater than right
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if left is greater; otherwise false</returns>
        public static bool GreaterThan
            (
                this Unit left,
                Unit right
            )
        {
            right = right.GetAs
            (
                left.UnitType
            );

            return
            (
                left.Value > right.Value
            );
        }

        /// <summary>
        /// Compares two unit instances to determine if left is greater or equal to right
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if left is greater or equal; otherwise false</returns>
        public static bool GreaterThanOrEqual
            (
                this Unit left,
                Unit right
            )
        {
            right = right.GetAs
            (
                left.UnitType
            );

            return
            (
                left.Value >= right.Value
            );
        }

        /// <summary>
        /// Compares two unit instances to determine if left is less than right
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if left is less; otherwise false</returns>
        public static bool LessThan
            (
                this Unit left,
                Unit right
            )
        {
            right = right.GetAs
            (
                left.UnitType
            );

            return
            (
                left.Value < right.Value
            );
        }

        /// <summary>
        /// Compares two unit instances to determine if left is less or equal to right
        /// </summary>
        /// <param name="left">The left value</param>
        /// <param name="right">The right value</param>
        /// <returns>True, if left is less or equal; otherwise false</returns>
        public static bool LessThanOrEqual
            (
                this Unit left,
                Unit right
            )
        {
            right = right.GetAs
            (
                left.UnitType
            );

            return
            (
                left.Value <= right.Value
            );
        }

        /// <summary>
        /// Adds two units together using the unit type of the first unit
        /// </summary>
        /// <param name="unit1">The first unit</param>
        /// <param name="unit2">The second unit</param>
        /// <returns>
        /// The sum of the two units in the unit type of the first argument unit
        /// </returns>
        public static Unit Add
            (
                this Unit unit1,
                Unit unit2
            )
        {
            unit2 = unit2.GetAs
            (
                unit1.UnitType
            );

            var newValue = (unit1.Value + unit2.Value);

            return new Unit
            (
                newValue,
                unit1.UnitType
            );
        }

        /// <summary>
        /// Subtracts two units using the unit type of the first unit
        /// </summary>
        /// <param name="unit1">The unit to subtract from</param>
        /// <param name="unit2">The unit to subtract</param>
        /// <returns>
        /// The difference of the two units in the unit type of the first argument unit
        /// </returns>
        public static Unit Subtract
            (
                this Unit unit1,
                Unit unit2
            )
        {
            unit2 = unit2.GetAs
            (
                unit1.UnitType
            );

            var newValue = (unit1.Value - unit2.Value);
            
            return new Unit
            (
                newValue,
                unit1.UnitType
            );
        }

        /// <summary>
        /// Divides two units using the unit type of the first unit
        /// </summary>
        /// <param name="unit1">The unit to subtract from</param>
        /// <param name="unit2">The unit to subtract</param>
        /// <returns>
        /// The product of dividing this unit by the specified value
        /// </returns>
        public static Unit Divide
            (
                this Unit unit1,
                Unit unit2
            )
        {
            unit2 = unit2.GetAs
            (
                unit1.UnitType
            );

            var newValue = (unit1.Value / unit2.Value);
            
            return new Unit
            (
                newValue,
                unit1.UnitType
            );
        }

        /// <summary>
        /// Divides a unit by a specified value
        /// </summary>
        /// <param name="unit">The unit</param>
        /// <param name="value">The value to multiply by</param>
        /// <returns>
        /// The product of dividing this unit by the specified value
        /// </returns>
        public static Unit Divide
            (
                this Unit unit,
                double value
            )
        {
            var newValue = (unit.Value / value);

            return new Unit
            (
                newValue,
                unit.UnitType
            );
        }

        /// <summary>
        /// Multiplies two units using the unit type of the first unit
        /// </summary>
        /// <param name="unit1">The first unit</param>
        /// <param name="unit2">The second unit</param>
        /// <returns>
        /// The product of multiplying this unit by the specified value
        /// </returns>
        public static Unit Multiply
            (
                this Unit unit1,
                Unit unit2
            )
        {
            unit2 = unit2.GetAs
            (
                unit1.UnitType
            );

            var newValue = (unit1.Value * unit2.Value);

            return new Unit
            (
                newValue,
                unit1.UnitType
            );
        }

        /// <summary>
        /// Multiplies a unit by a specified value
        /// </summary>
        /// <param name="unit">The unit</param>
        /// <param name="value">The value to multiply by</param>
        /// <returns>
        /// The product of multiplying this unit by the specified value
        /// </returns>
        public static Unit Multiply
            (
                this Unit unit,
                double value
            )
        {
            var newValue = (unit.Value * value);

            return new Unit
            (
                newValue,
                unit.UnitType
            );
        }
    }
}
