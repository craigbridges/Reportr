namespace Reportr.Components.Graphics
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a single graphic definition
    /// </summary>
    public class GraphicDefinition : ReportComponentBase
    {
        /// <summary>
        /// Constructs the graphic definition with overlays
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="image">The image</param>
        /// <param name="overlays">The overlays</param>
        public GraphicDefinition
            (
                string name,
                string title,
                Image image,
                params GraphicOverlay[] overlays
            )
            : base(name, title)
        {
            Validate.IsNotNull(image);

            this.Image = image;
            this.Areas = new GraphicArea[] { };

            if (overlays == null)
            {
                this.Overlays = new GraphicOverlay[] { };
            }
            else
            {
                this.Overlays = overlays;
            }
        }

        /// <summary>
        /// Constructs the graphic definition with areas
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="title">The title</param>
        /// <param name="image">The image</param>
        /// <param name="areas">The areas</param>
        public GraphicDefinition
            (
                string name,
                string title,
                Image image,
                params GraphicArea[] areas
            )
            : base(name, title)
        {
            Validate.IsNotNull(image);

            this.Image = image;
            this.Overlays = new GraphicOverlay[] { };

            if (areas == null)
            {
                this.Areas = new GraphicArea[] { };
            }
            else
            {
                this.Areas = areas;
            }
        }

        /// <summary>
        /// Gets the image to display
        /// </summary>
        public Image Image { get; private set; }



        // TODO: define way of having a query that generates the overlays or areas with coordinates etc



        /// <summary>
        /// Gets an array of overlays to display over the graphic
        /// </summary>
        public GraphicOverlay[] Overlays { get; private set; }
        
        /// <summary>
        /// Gets an array of areas to render over the graphic
        /// </summary>
        public GraphicArea[] Areas { get; private set; }

        /// <summary>
        /// Adds the image dimension values to the result
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>The updated graphic result</returns>
        public GraphicDefinition WithDimensions
            (
                double width,
                double height
            )
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The width must be greater than zero."
                );
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException
                (
                    "The height must be greater than zero."
                );
            }

            this.Width = width;
            this.Height = height;

            return this;
        }

        /// <summary>
        /// Gets the image width
        /// </summary>
        public double? Width { get; private set; }

        /// <summary>
        /// Gets the image height
        /// </summary>
        public double? Height { get; private set; }

        /// <summary>
        /// Gets the report component type
        /// </summary>
        public override ReportComponentType ComponentType
        {
            get
            {
                return ReportComponentType.Graphic;
            }
        }
    }
}
