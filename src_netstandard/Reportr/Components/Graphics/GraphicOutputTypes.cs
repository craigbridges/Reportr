namespace Reportr.Components.Graphics
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the most common graphic types
    /// </summary>
    public enum GraphicOutputTypes
    {
        [Description("JPEG (Joint Photographic Experts Group)")]
        Jpeg = 0,

        [Description("GIF (Graphics Interchange Format)")]
        Gif = 1,

        [Description("BMP (Windows Bitmap)")]
        Bmp = 2,

        [Description("TIFF (Tagged Image File Format)")]
        Tiff = 4,

        [Description("PNG (Portable Network Graphics)")]
        Png = 8,

        [Description("Icon")]
        Icon = 16,

        [Description("Vector")]
        Vector = 32,

        [Description("All Types")]
        All = Jpeg | Gif | Bmp | Tiff | Png | Icon | Vector
    }
}
