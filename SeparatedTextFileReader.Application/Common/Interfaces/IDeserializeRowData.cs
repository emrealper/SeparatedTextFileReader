using System.Collections.Generic;
using SeparatedTextFileReader.Domain.Common;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IDeserializeRowData
        <E> where E : class, IEntity
    {
        E Deserialize(string line);

        E Deserialize(Dictionary<int, string> headerAttribute, Dictionary<int, string> rowData);
    }
}