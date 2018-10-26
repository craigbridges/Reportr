namespace Reportr.Tests
{
    using Xunit;

    public class ReportDefinitionTests
    {
        [Fact]
        public void ReportDefinition_DefineSection_GetSection()
        {
            // Arrange
            var report = new ReportDefinition
            (
                "Test",
                "Test Report"
            );

            report.DefineBody("Body");

            // Act
            var body = report.GetSection
            (
                ReportSectionType.ReportBody
            );

            // Assert
            Assert.NotNull(body);
        }
    }
}
