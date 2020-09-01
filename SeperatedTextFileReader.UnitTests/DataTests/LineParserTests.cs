using SeparatedTextFileReader.Infrastructure.DataHelpers;
using Xunit;

namespace SeperatedTextFileReader.UnitTests.DataTests
{
    public class LineParserTests
    {
        [Theory]
        [InlineData("Project	Description	Start date	Category	Responsible	Savings amount	Currency	Complexity",
            new[]
            {
                "Project", "Description", "Start date", "Category", "Responsible", "Savings amount", "Currency",
                "Complexity"
            },
            true)]
        public void Should_ReturnsCorrectDictionary_WithTabSeparatedLine(string line, string[] expectedStrings,
            bool expectedResult)
        {
            var actualResult = false;

            var lineParser = new LineParser();

            var actual = lineParser.TabSeparatedParser(line);

            foreach (var entry in actual)
                if (entry.Value == expectedStrings[entry.Key])
                    actualResult = true;

            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData("Project,Description,Start date,Category,Responsible,Savings amount,Currency,Complexity",
            new[]
            {
                "Project", "Description", "Start date", "Category", "Responsible", "Savings amount", "Currency",
                "Complexity"
            },
            true)]
        public void Should_ReturnsCorrectDictionary_WithCommaSeparatedLine(string line, string[] expectedStrings,
            bool expectedResult)
        {
            var actualResult = false;

            var lineParser = new LineParser();

            var actual = lineParser.CommaSeparatedParser(line);

            foreach (var entry in actual)
                if (entry.Value == expectedStrings[entry.Key])
                    actualResult = true;


            Assert.Equal(expectedResult, actualResult);
        }
    }
}