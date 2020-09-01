using System;
using System.Collections.Generic;


namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface ILineParser
    {
        Dictionary<int, string> TabSeparatedParser(string line);
        Dictionary<int, string> CommaSeparatedParser(string line);
    }
}