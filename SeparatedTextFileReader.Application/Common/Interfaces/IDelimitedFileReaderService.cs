using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IDelimitedFileReaderService

    {

        bool TryReadAndParselines<T>(
            Dictionary<string, string> attributeMappings,
            out List<AdProcurement> valueList,
            out Dictionary<int, string> headerInOrder,
            out Dictionary<string, int> dataLinePropsInOrder,
            out string errors)
            where T : IEntity;

    }

}
