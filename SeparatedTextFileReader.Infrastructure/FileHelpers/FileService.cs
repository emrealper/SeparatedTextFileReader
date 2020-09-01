using System.Collections.Generic;
using System.IO;
using System.Text;
using SeparatedTextFileReader.Application.Common.Interfaces;
using FileNotFoundException = SeparatedTextFileReader.Application.Common.Exceptions.FileNotFoundException;

namespace SeparatedTextFileReader.Infrastructure.FileHelpers
{
    public class FileService : IFileService
    {
        public IEnumerable<string> ReadLines(string filePath)
        {
            if (File.Exists(filePath))
                return File.ReadLines(filePath, Encoding.UTF8);
            throw new FileNotFoundException(filePath);
        }
    }
}