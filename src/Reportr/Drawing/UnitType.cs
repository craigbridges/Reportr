namespace Reportr.Drawing
{
    using System.ComponentModel;
    
    /// <summary>
    /// Defines a collection of unit types
    /// </summary>
    /// <remarks>
    /// 1 inch = 6 picas = 72 points.
    ///
    /// 1 centimeter = 10 millimeters.
    ///
    /// Pixels convert to and from other units based on 
    /// the DPI of the current graphics context.
    /// </remarks>
    public enum UnitType
    {
        /// <summary>
        /// String equivalent used in Unit's constructor should be "px"
        /// </summary>
        [Description("Pixel")]
        Pixel = 0,

        /// <summary>
        /// String equivalent used in Unit's constructor should be "pt"
        /// </summary>
        [Description("Point")]
        Point = 1,

        /// <summary>
        /// String equivalent used in Unit's constructor should be "pc"
        /// </summary>
        [Description("Pica")]
        Pica = 2,

        /// <summary>
        /// String equivalent used in Unit's constructor should be "in"
        /// </summary>
        [Description("Inch")]
        Inch = 4,

        /// <summary>
        /// String equivalent used in Unit's constructor should be "mm"
        /// </summary>
        [Description("Millimeter")]
        Millimeter = 8,

        /// <summary>
        /// String equivalent used in Unit's constructor should be "cm"
        /// </summary>
        [Description("Centimeter")]
        Centimeter = 16,

        /// <summary>
        /// String equivalent used in Unit's constructor should be "pe"
        /// </summary>
        [Description("Percentage")]
        Percentage = 32
    }
}
