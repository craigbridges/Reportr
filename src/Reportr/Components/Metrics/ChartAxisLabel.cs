namespace Reportr.Components.Metrics
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents the definition of a single chart axis label
    /// </summary>
    public class ChartAxisLabel
    {
        /// <summary>
        /// Constructs the label with the default values
        /// </summary>
        public ChartAxisLabel()
        {
            this.ValueType = ChartValueType.Double;
        }

        /// <summary>
        /// Gets or sets the label placement
        /// </summary>
        public ChartPlacement Placement { get; set; }

        /// <summary>
        /// Gets or sets the labels color
        /// </summary>
        public Color? Color { get; set; }

        /// <summary>
        /// Gets or sets the custom text to be used as the label
        /// </summary>
        public string CustomText { get; set; }

        /// <summary>
        /// Gets or sets the date and time value
        /// </summary>
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date and time format
        /// </summary>
        /// <remarks>
        /// See "Date and Time format strings" section in MSDN.
        /// </remarks>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// Gets or sets the value that the label represents
        /// </summary>
        public double DoubleValue { get; set; }

        /// <summary>
        /// Gets or sets the double format
        /// </summary>
        /// <remarks>
        /// See "Numeric Format Strings" section in MSDN.
        /// </remarks>
        public string DoubleFormat { get; set; }

        /// <summary>
        /// Gets or sets the rounding decimal places to be used in the label
        /// </summary>
        public int RoundingPlaces { get; set; }

        /// <summary>
        /// Gets or sets the text to be used for the tool tip
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the labels value type
        /// </summary>
        public ChartValueType ValueType { get; set; }

        /// <summary>
        /// Gets the formatted text that is to be displayed as the label
        /// </summary>
        public string Text
        {
            get
            {
                switch (this.ValueType)
                {
                    case ChartValueType.Custom:
                        return this.CustomText;

                    case ChartValueType.DateTime:
                        return GetDateString();

                    case ChartValueType.Double:
                        return GetDoubleString();

                    default:
                        return GetDoubleString();
                }
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance
        /// </summary>
        /// <returns>An exact clone of the label</returns>
        public ChartAxisLabel Clone()
        {
            return new ChartAxisLabel()
            {
                Placement = this.Placement,
                Color = this.Color,
                CustomText = this.CustomText,
                DateValue = this.DateValue,
                DateTimeFormat = this.DateTimeFormat,
                DoubleValue = this.DoubleValue,
                DoubleFormat = this.DoubleFormat,
                RoundingPlaces = this.RoundingPlaces,
                ToolTip = this.ToolTip,
                ValueType = this.ValueType
            };
        }

        /// <summary>
        /// Gets the date and time value as a string
        /// </summary>
        /// <returns>The date and time string</returns>
        private string GetDateString()
        {
            var date = this.DateValue;
            var dateFormat = this.DateTimeFormat;

            if (date == null)
            {
                throw new InvalidProgramException
                (
                    "The date and time value has not been set."
                );
            }

            if (String.IsNullOrEmpty(dateFormat))
            {
                return date.Value.ToString();
            }
            else
            {
                return date.Value.ToString
                (
                    dateFormat
                );
            }
        }

        /// <summary>
        /// Gets the double value as a string
        /// </summary>
        /// <returns>The double string</returns>
        private string GetDoubleString()
        {
            var format = this.DoubleFormat;
            var rounding = this.RoundingPlaces;
            var value = this.DoubleValue;

            value = Math.Round
            (
                value,
                rounding
            );

            if (String.IsNullOrEmpty(format))
            {
                return value.ToString();
            }
            else
            {
                return value.ToString
                (
                    format
                );
            }
        }
    }
}
