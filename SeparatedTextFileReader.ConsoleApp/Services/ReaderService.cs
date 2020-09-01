using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.ConsoleApp.Options;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.ValueObject;

namespace SeparatedTextFileReader.ConsoleApp.Services
{
    public class ReaderService : IReaderService
    {
        private static AttributeMappingsOptions _procurementFileAttributeEntityMappingsOptions;

        private static Dictionary<string, string> _attributesMappings;


        private readonly IDelimitedFileReaderService _delimetedFileReaderService;

        private readonly IFilterByArgumentQueryHandler _filterByArgumentQueryHandler;

        private readonly ILogger<ReaderService> _log;


        public ReaderService(ILogger<ReaderService> log,
            IDelimitedFileReaderService delimetedFileReaderService,
            IFilterByArgumentQueryHandler filterByArgumentQueryHandler,
            IConfiguration config
        )
        {
            _log = log;
            _procurementFileAttributeEntityMappingsOptions =
                config.GetSection("AttributeMappingsConfiguration").Get<AttributeMappingsOptions>();
            _attributesMappings = _procurementFileAttributeEntityMappingsOptions.AttributeMappings;

            _filterByArgumentQueryHandler = filterByArgumentQueryHandler;
            _delimetedFileReaderService = delimetedFileReaderService;
        }


        public void Run(IArguments arguments)
        {
            var errors = string.Empty;
            var headerInOrder = new Dictionary<int, string>();
            var dataLinePropsOrder = new Dictionary<string, int>();
            var valueList = new List<AdProcurement>();
            var procurements = _delimetedFileReaderService.TryReadAndParselines<Procurement>
            (arguments.File, _attributesMappings,
                out valueList, out headerInOrder,
                out dataLinePropsOrder,
                out errors);

            if (!string.IsNullOrEmpty(errors))
            {
                Console.WriteLine(errors);
            }
            else
            {
                //print header in correct order
                Console.WriteLine(string.Join("\t", headerInOrder.Values));

                //print lines in correct order
                foreach (var value in _filterByArgumentQueryHandler.FilterDataByArguments(valueList, arguments))
                {
                    Console.Write(value.ToTabDelimatedString(dataLinePropsOrder));
                    Console.Write("\n");
                }
            }
        }
    }
}