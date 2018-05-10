namespace Reportr.Components.Graphics
{
    /// <summary>
    /// Defines a contract for a single report graphic
    /// </summary>
    public interface IGraphic : IReportComponent
    {
        /// <summary>
        /// Gets a flag indicating if the graphic has any overlays
        /// </summary>
        bool HasOverlays { get; }

        /// <summary>
        /// Gets the graphic output type
        /// </summary>
        GraphicOutputTypes OutputType { get; }
    }
}
