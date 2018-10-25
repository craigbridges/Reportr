namespace Reportr.Templating
{
    using System.ComponentModel;

    /// <summary>
    /// Defines all template output types
    /// </summary>
    public enum TemplateOutputType
    {
        [Description("Hyper Text Markup Language (HTML)")]
        Html = 0,

        [Description("Plain Text")]
        Text = 1,

        [Description("Portable Document Format (PDF)")]
        Pdf = 2
    }
}
