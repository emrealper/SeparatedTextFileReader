using Newtonsoft.Json;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Infrastructure.DataHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SeparatedTextFileReader.UnitTests.DataTests
{
    public class DeserializeRowDataTests
    {
        [Fact]
        public void Should_Correctly_Deserialize_Parsed_Row_Data()
        {
            var deserializeRowData = new DeserializeRowData<Procurement>();

            var properties = new Dictionary<int, string>
            {
                {0, "Project"}, {1, "Description"}, {3, "Start date"}, {4, "Category"}, {5, "Responsible"},
                {6, "Savings amount"}, {7, "Currency"}, {8, "Complexity"}
            };
            var values = new Dictionary<int, string>
            {
                {0, "2"}, {1, "Black and white logo paper"}, {3, "2012-06-01 00:00:00.000"}, {4, "Office supplies"},
                {5, "Clark Kent"},
                {6, "4880.199567"}, {7, "EUR"}, {8, "Simple"}
            };

            var propertyMapping = new Dictionary<string, string>
            {
                {"Project", "Project"}, {"Description", "Description"}, {"StartDate", "Start date"},
                {"Category", "Category"},
                {"Responsible", "Responsible"},
                {"SavingsAmount", "Savings amount"}, {"Currency", "Currency"}, {"Complexity", "Complexity"}
            };

            var expected = new Procurement
            {
                Project = "2",
                Description = "Black and white logo paper",
                StartDate = "2012-06-01 00:00:00.000",
                Category = "Office supplies",
                Responsible = "Clark Kent",
                SavingsAmount = "4880.199567",
                Currency = "EUR",
                Complexity = "Simple"
            };

            var actual = deserializeRowData.Deserialize(properties, values, propertyMapping);

            var expectedJsonString = JsonConvert.SerializeObject(expected);
            var actualJsonString = JsonConvert.SerializeObject(actual);

            Assert.Equal(expectedJsonString, actualJsonString);
        }
    }
}