namespace Reportr.Data
{
    /// <summary>
    /// Defines importance flags for data (e.g. columns or rows)
    /// </summary>
    public enum DataImportance
    {
        Default = 0,
        Low = 1,
        Medium = 2,
        High = 4,
        Critical = 8
    }
}
