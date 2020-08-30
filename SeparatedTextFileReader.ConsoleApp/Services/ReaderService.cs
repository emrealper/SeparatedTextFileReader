using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Infrastructure.DataHelpers;

namespace SeparatedTextFileReader.ConsoleApp.Services
{
    public class ReaderService : IReaderService
    {
 



        private readonly IDelimitedFileReaderService _delimetedFileReaderService;

        private readonly ILogger<ReaderService> _log;

        public ReaderService(ILogger<ReaderService> log,
     
           
            IDelimitedFileReaderService delimetedFileReaderService
        )
        {
            _log = log;


            _delimetedFileReaderService = delimetedFileReaderService;
        }


        public async void Run()
        {

             

          var procurements = _delimetedFileReaderService.Readlines<Procurement>();
         
        }
    }
}