using SeparatedTextFileReader.Domain.ValueObject;
using System.Collections.Generic;


namespace SeparatedTextFileReader.Application.Common.Interfaces
{
    public interface IFilterByArgumentQueryHandler
    {
        IList<AdProcurement> FilterDataByArguments(IList<AdProcurement> data, IArguments arguments);
    }
}