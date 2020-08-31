using System.Collections.Generic;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IFileService
    {
        IEnumerable<string> ReadLines(string filePath);
    }
}