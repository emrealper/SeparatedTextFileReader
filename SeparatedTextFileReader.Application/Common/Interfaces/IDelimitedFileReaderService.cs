using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Domain.ValueObject;
using System.Collections.Generic;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IDelimitedFileReaderService

    {
        bool TryReadAndParselines<T>(
            string filePath,
            Dictionary<string, string> attributeMappings,
            out List<AdProcurement> valueList,
            out Dictionary<int, string> headerInOrder,
            out Dictionary<string, int> dataLinePropsInOrder,
            out string errors)
            where T : IEntity;
    }
}