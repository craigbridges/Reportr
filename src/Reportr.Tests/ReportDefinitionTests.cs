namespace Reportr.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReportDefinitionTests
    {
        [TestMethod]
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
            Assert.IsNotNull(body);
        }
    }
}
