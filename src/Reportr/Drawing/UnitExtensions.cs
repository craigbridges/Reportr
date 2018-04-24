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

            if (newValue < 0)
            {
                newValue = 0;
            }

            return new Unit
            (
                newValue,
                unit1.UnitType
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
        /// <param name="unit1">The first unit</param>
        /// <param name="value">The value to multiply by</param>
        /// <returns>
        /// The product of multiplying this unit by the specified value
        /// </returns>
        public static Unit Multiply
            (
                this Unit unit1,
                double value
            )
        {
            var newValue = (unit1.Value * value);

            return new Unit
            (
                newValue,
                unit1.UnitType
            );
        }
    }
}
