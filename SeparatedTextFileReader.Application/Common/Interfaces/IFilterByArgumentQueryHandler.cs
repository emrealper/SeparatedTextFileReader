using SeparatedTextFileReader.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
   public interface IFilterByArgumentQueryHandler
    {
        IList<AdProcurement> FilterDataByArguments(IList<AdProcurement> data, IArguments arguments);
    }
}
