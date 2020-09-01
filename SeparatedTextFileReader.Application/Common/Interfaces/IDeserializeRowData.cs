using System.Collections.Generic;
using SeparatedTextFileReader.Domain.Common;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IDeserializeRowData
        <E> where E : class, IEntity
    {
        E Deserialize(Dictionary<int, string> properties,
            Dictionary<int, string> values,
            Dictionary<string, string> propertyMapping);
    }
}