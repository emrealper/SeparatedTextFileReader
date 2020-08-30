using System;
using System.Collections.Generic;
using System.Text;

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
