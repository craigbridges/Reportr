namespace Reportr.Components.Graphics
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    
    /// <summary>
    /// Represents a single graphic area
    /// </summary>
    public class GraphicArea
    {
        /// <summary>
        /// Constructs the graphic area with the details
        /// </summary>
        /// <param name="path">The graphics path</param>
        /// <param name="fillColor">The fill color (optional)</param>
        /// <param name="toolTipText">The tool-tip text (optional)</param>
        public GraphicArea
            (
                GraphicsPath path,
                Color? fillColor = null,
                string toolTipText = null
            )
        {
            Validate.IsNotNull(path);

            this.AreaId = Guid.NewGuid();
            this.Path = path;
            this.FillColor = fillColor;
            this.ToolTipText = toolTipText;
        }

        /// <summary>
        /// Gets the graphic area ID
        /// </summary>
        public Guid AreaId { get; protected set; }

        /// <summary>
        /// Gets the path describing the graphic area
        /// </summary>
        public GraphicsPath Path { get; protected set; }

        /// <summary>
        /// Gets the areas fill color
        /// </summary>
        public Color? FillColor { get; protected set; }

        /// <summary>
        /// Adds the border details to the area
        /// </summary>
        /// <param name="color">The border color</param>
        /// <param name="width">The border width</param>
        /// <returns>The updated graphic area</returns>
        public GraphicArea WithBorder
            (
                Color color,
                double width
            )
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The width cannot be less than zero."
                );
            }

            this.BorderColor = color;
            this.BorderWidth = width;

            return this;
        }

        /// <summary>
        /// Gets the border color
        /// </summary>
        public Color? BorderColor { get; protected set; }

        /// <summary>
        /// Gets the border width
        /// </summary>
        public double BorderWidth { get; protected set; }

        /// <summary>
        /// Gets the tool-tip text
        /// </summary>
        public string ToolTipText { get; protected set; }

        /// <summary>
        /// Adds an action to the area
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The updated graphic area</returns>
        public GraphicArea WithAction
            (
                ReportActionDefinition action
            )
        {
            Validate.IsNotNull(action);

            this.Action = action;

            return this;
        }

        /// <summary>
        /// Gets the action for the area
        /// </summary>
        public ReportActionDefinition Action { get; protected set; }
    }
}
