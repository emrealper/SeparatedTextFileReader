using System.Collections.Generic;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.Validators;
using SeparatedTextFileReader.Infrastructure.DataHelpers;
using Xunit;

namespace SeparatedTextFileReader.UnitTests.ValidationTest
{
    public class LineDataValidationsTests
    {
        private static string headerString =>
            "Project	Description	Start date	Category	Responsible	Savings amount	Currency	Complexity";

        private Dictionary<string, string> attributeMappings => new Dictionary<string, string>
        {
            {"Project", "Project"}, {"Description", "Description"}, {"StartDate", "Start date"},
            {"Category", "Category"},
            {"Responsible", "Responsible"},
            {"SavingsAmount", "Savings amount"}, {"Currency", "Currency"}, {"Complexity", "Complexity"}
        };

        private List<string> validComplexityTypes => new List<string> {"Simple", "Moderate", "Hazardous"};


        [Theory]
        [InlineData("2	Harmonize Lactobacillus acidophilus sourcing	2014-01-01	Dairy	Daisy Milks	NULL	NULL	Simple")]
        public void Should_ReturnError_With_WrongStartDateFormat(string line)
        {
            var expectedResult = "Start date should conform to the following format: yyyy-mm-dd hh:mm:ss.sss";
            var lineParser = new LineParser();
            var deserializeRowData = new DeserializeRowData<Procurement>();
            var headerColumnDictionay = lineParser.TabSeparatedParser(headerString);
            var rowColumnDictionary = lineParser.TabSeparatedParser(line);

            var entity = deserializeRowData.Deserialize(headerColumnDictionay, rowColumnDictionary,
                attributeMappings);


            var actualResult = string.Empty;
            var results = new ProcurementEntityValidator().Validate(entity);

            if (!results.IsValid)
                actualResult = string.Join("", results.Errors);


            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("6	Black and white logo paper	2012-06-01 00:00:00.000	Office supplies	Clark Kent	4880	EUR	Simple")]
        public void Should_ReturnError_With_WrongSavingAmountFormat(string line)
        {
            var expectedResult =
                "Money (Savings amount) values should conform be numbers with a point as the decimal separator";
            var lineParser = new LineParser();
            var deserializeRowData = new DeserializeRowData<Procurement>();
            var headerColumnDictionay = lineParser.TabSeparatedParser(headerString);
            var rowColumnDictionary = lineParser.TabSeparatedParser(line);

            var entity = deserializeRowData.Deserialize(headerColumnDictionay, rowColumnDictionary,
                attributeMappings);


            var actualResult = string.Empty;
            var results = new ProcurementEntityValidator().Validate(entity);

            if (!results.IsValid)
                actualResult = string.Join("", results.Errors);


            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData(
            "6	Black and white logo paper	2012-06-01 00:00:00.000	Office supplies	Clark Kent	4880.199567	EUR	VeryHigh")]
        public void Should_ReturnError_With_ComplexityDiffersFromGivenValues(string line)
        {
            var expectedResult =
                $"Column Complexity should only have following values: {string.Join(",", validComplexityTypes)}";
            var lineParser = new LineParser();
            var deserializeRowData = new DeserializeRowData<Procurement>();
            var headerColumnDictionay = lineParser.TabSeparatedParser(headerString);
            var rowColumnDictionary = lineParser.TabSeparatedParser(line);

            var entity = deserializeRowData.Deserialize(headerColumnDictionay, rowColumnDictionary,
                attributeMappings);


            var actualResult = string.Empty;
            var results = new ProcurementEntityValidator().Validate(entity);

            if (!results.IsValid)
                actualResult = string.Join("", results.Errors);


            Assert.Equal(expectedResult, actualResult);
        }
    }
}