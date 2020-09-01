using System;

namespace SeparatedTextFileReader.Application.Common.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string path)
            : base($"File ({path}) was not found.")
        {
        }
    }
}