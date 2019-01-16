namespace Reportr.Conversion.Csv
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using System;
    using System.IO;

    /// <summary>
    /// Various extension methods for CSV documents
    /// </summary>
    public static class CsvDocumentExtensions
    {
        /// <summary>
        /// Converts a CSV document to a byte array
        /// </summary>
        /// <param name="document">The CSV document</param>
        /// <returns>A byte array containing the CSV content</returns>
        public static byte[] ToByteArray
            (
                this CsvDocument document
            )
        {
            return document.ToByteArray
            (
                new Configuration()
            );
        }

        /// <summary>
        /// Converts a CSV document to a byte array
        /// </summary>
        /// <param name="document">The CSV document</param>
        /// <param name="configuration">The configuration</param>
        /// <returns>A memory stream containing the CSV content</returns>
        public static byte[] ToByteArray
            (
                this CsvDocument document,
                Configuration configuration
            )
        {
            Validate.IsNotNull(document);
            Validate.IsNotNull(configuration);

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, configuration))
            {
                foreach (var heading in document.Headings)
                {
                    csv.WriteField(heading);
                }

                csv.NextRecord();

                foreach (var row in document.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        csv.WriteField(cell.Value);
                    }

                    csv.NextRecord();
                }

                writer.Flush();

                return stream.ToArray();
            }
        }
    }
}
