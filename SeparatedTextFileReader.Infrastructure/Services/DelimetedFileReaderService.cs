using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.ValueObject;
using SeparatedTextFileReader.Infrastructure.FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Infrastructure.Services
{
   public class DelimetedFileReaderService :IDelimitedFileReaderService
    {


        private readonly IFileService _fileService;
        private readonly IDeserializeRowData<Procurement> _deserializeRowData;
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




        public List<IEntity> Readlines<T>() where T:IEntity
        {

            var depth = 0;
            var result = new List<IEntity>();
            var headerColumnDictionary = new Dictionary<int, string>();

            foreach ( var line in _fileService.ReadLines())
            {
                var rowColumnDictionary = new Dictionary<int, string>();

                if(depth==0)
                {
                    headerColumnDictionary = _lineParser.TabSeparatedParser(line);
                }
                else
                {
                    rowColumnDictionary = _lineParser.TabSeparatedParser(line);



                    var entity = _deserializeRowData.Deserialize(headerColumnDictionary,rowColumnDictionary);

                    AdProcurement value;
                    var validationErrors = string.Empty;
                    var adProcurement = AdProcurement.For(entity,out value,out validationErrors);
                    
                    
                    
                    result.Add(entity);

                    var a = 0;
                }


                depth++;
            }
      

            return result; 
            
        

           
            
        }
    }
}
