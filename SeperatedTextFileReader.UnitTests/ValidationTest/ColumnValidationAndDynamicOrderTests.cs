using Newtonsoft.Json;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Infrastructure.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SeparatedTextFileReader.UnitTests.ValidationTest
{
    public class ColumnValidationAndDynamicOrderTests
    {




        private Dictionary<string, string> attributeMappings => new Dictionary<string, string>
            {
                 {"Project","Project"},{"Description","Description"},{"StartDate","Start date"},{"Category","Category"},
                {"Responsible","Responsible"},
                {"SavingsAmount","Savings amount" },{"Currency","Currency"},{"Complexity","Complexity"}
            };

        [Theory, InlineData("Project	Description		Category		Savings amount	Currency	")]
        public void Should_ReturnError_With_MissingColumnAttribute(string header)
        {

            var expectedResult = "Start date, Responsible, Complexity";
            var lineParser = new LineParser();
            
            var headerColumnDictionary = lineParser.TabSeparatedParser(header);

            var missingHeaderAttributes = attributeMappings.Values.Except(headerColumnDictionary.Values);

            var result = string.Join(", ", missingHeaderAttributes);

    
            Assert.Equal(expectedResult, result);




        }


        [Theory, InlineData("Description	Project	Savings amount	Complexity	Responsible	Start date	Currency	Category",
            "Harmonize Lactobacillus acidophilus sourcing	2	NULL	Simple	Daisy Milks	2014-01-01 00:00:00.000	EUR	Dairy")]
        public void Should_ReturnExpectedResult_WithChangedColumnOrder(string header,string line)
        {

            var deserializeRowData = new DeserializeRowData<Procurement>();
            var lineParser = new LineParser();
            Procurement expected = new Procurement
            {
                Project = "2",
                Description = "Harmonize Lactobacillus acidophilus sourcing",
                StartDate = "2014-01-01 00:00:00.000",
                Category = "Dairy",
                Responsible = "Daisy Milks",
                SavingsAmount = "NULL",
                Currency = "EUR",
                Complexity = "Simple"
            };

            

            var headerColumnDictionary = lineParser.TabSeparatedParser(header);
            var rowColumnDictionary = lineParser.TabSeparatedParser(line);
            Procurement actual = deserializeRowData.Deserialize(headerColumnDictionary, rowColumnDictionary, attributeMappings);
            var expectedJsonString = JsonConvert.SerializeObject(expected);
            var actualJsonString = JsonConvert.SerializeObject(actual);

            Assert.Equal(expectedJsonString, actualJsonString);

        }


    }
}
