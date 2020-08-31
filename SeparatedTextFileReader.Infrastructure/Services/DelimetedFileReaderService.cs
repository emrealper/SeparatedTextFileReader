using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.ValueObject;

namespace SeparatedTextFileReader.Infrastructure.Services
{
    public class DelimetedFileReaderService : IDelimitedFileReaderService
    {
        private readonly IDeserializeRowData<Procurement> _deserializeRowData;


        private readonly IFileService _fileService;
        private readonly ILineParser _lineParser;

        public DelimetedFileReaderService(
            IFileService fileService,
            IDeserializeRowData<Procurement> deserializeRowData,
            ILineParser lineParser
        )
        {
            _deserializeRowData = deserializeRowData;
            _fileService = fileService;
            _lineParser = lineParser;
        }


        public bool TryReadAndParselines<T>(
            string filePath,
            Dictionary<string, string> attributeMappings,
            out List<AdProcurement> valueList,
            out Dictionary<int, string> headerInOrder,
            out Dictionary<string, int> dataLinePropsInOrder,
            out string errors)
            where T : IEntity
        {
            var errorMessages = new StringBuilder();
            var orderedHeaderAttributes = new Dictionary<int, string>();
            var orderedLineProperties = new Dictionary<string, int>();
            var isValidationsPassed = true;
            var adProcurementList = new List<AdProcurement>();


            var depth = 0;
            var result = new List<IEntity>();
            var headerColumnDictionary = new Dictionary<int, string>();


            try
            {
                foreach (var line in _fileService.ReadLines(filePath))
                {
                    if (line.StartsWith("****") || line.StartsWith("#") || string.IsNullOrEmpty(line)) continue;

                    var rowColumnDictionary = new Dictionary<int, string>();

                    if (depth == 0)
                    {
                        headerColumnDictionary = _lineParser.TabSeparatedParser(line);
                        //check the header is the correct format and extract missing header attribute
                        var missingHeaderAttributes = attributeMappings.Values.Except(headerColumnDictionary.Values);
                        if (missingHeaderAttributes.ToList().Count != 0)
                        {
                            errorMessages.Append(
                                $"Following header attributes are missing:  {string.Join(", ", missingHeaderAttributes)}");
                            break;
                        }

                        orderedHeaderAttributes = headerColumnDictionary;

                        //extract line data property order
                        foreach (var mapping in headerColumnDictionary)
                            orderedLineProperties[attributeMappings.FirstOrDefault(x => x.Value == mapping.Value).Key]
                                = mapping.Key;
                    }
                    else
                    {
                        rowColumnDictionary = _lineParser.TabSeparatedParser(line);


                        var entity = _deserializeRowData.Deserialize(headerColumnDictionary, rowColumnDictionary,
                            attributeMappings);

                        AdProcurement value;
                        var validationErrors = string.Empty;
                        AdProcurement.For(entity, out value, out validationErrors);

                        if (value != null && string.IsNullOrEmpty(validationErrors))
                        {
                            adProcurementList.Add(value);
                        }
                        else
                        {
                            errorMessages.Append($"Row Number {depth} : \n {validationErrors} \n");
                            isValidationsPassed = false;
                        }
                    }


                    depth++;
                }
            }

            catch(Exception exx)
            {
                errorMessages.Append(exx.Message);

            }

            valueList = adProcurementList;
            headerInOrder = orderedHeaderAttributes;
            dataLinePropsInOrder = orderedLineProperties;
            errors = errorMessages.ToString();
            return isValidationsPassed;
        }
    }
}