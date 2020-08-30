using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.ConsoleApp.Options
{
   public  class AttributeMappingsOptions
    {

        public Dictionary<string, string> AttributeMappings { get; set; }


        public void Deconstruct(out Dictionary<string, string> attributeMappings)
        {
            attributeMappings = AttributeMappings;
        }
    }
}
