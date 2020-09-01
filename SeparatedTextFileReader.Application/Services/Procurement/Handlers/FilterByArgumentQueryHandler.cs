using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Domain.ValueObject;
using System.Collections.Generic;
using System.Linq;

namespace SeparatedTextFileReader.Application.Services.Procurement.Handlers
{
    public class FilterByArgumentQueryHandler : IFilterByArgumentQueryHandler
    {
        public IList<AdProcurement> FilterDataByArguments(IList<AdProcurement> data, IArguments arguments)
        {
            var filteredResult = data;


            if (arguments.Project.HasValue)
                filteredResult = filteredResult.Where(i => i.Project == arguments.Project.Value).ToList();

            if (arguments.SortByStartDate) filteredResult = filteredResult.OrderBy(i => i.StartDate).ToList();
            return filteredResult;
        }
    }
}