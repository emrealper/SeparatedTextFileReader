using SeparatedTextFileReader.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Infrastructure.DataHelpers
{
    public class LineParser : ILineParser
    {
        private static string TabDelimeter => "\t";

        private static string CommaDelimeter => ",";


        public Dictionary<int, string> TabSeparatedParser(string line)
        {
            var lineDictionary = new Dictionary<int, string>();

            for (var i = 0; i < line.Split(TabDelimeter).Length; i++)
                lineDictionary.Add(i, line.Split(TabDelimeter)[i].Replace("\"", ""));

            return lineDictionary;

        }


        public Dictionary<int, string> CommaSeparatedParser(string line)
        {
            var lineDictionary = new Dictionary<int, string>();

            for (var i = 0; i < line.Split(CommaDelimeter).Length; i++)
                lineDictionary.Add(i, line.Split(",")[i].Replace("\"", ""));

            return lineDictionary;

        }

    }
}
