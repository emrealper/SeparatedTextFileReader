using System.Collections.Generic;

namespace SeparatedTextFileReader.ConsoleApp.Options
{
    public class AttributeMappingsOptions
    {
        public Dictionary<string, string> AttributeMappings { get; set; }


        public void Deconstruct(out Dictionary<string, string> attributeMappings)
        {
            attributeMappings = AttributeMappings;
        }


        public IEnumerable<string> ToTabDelimatedString()
        {
            foreach (var headerAttribute in AttributeMappings) yield return headerAttribute.Value + "\t";
        }
    }
}