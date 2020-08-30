using SeparatedTextFileReader.Domain.Common;
using SeparatedTextFileReader.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Application.Common.Interfaces
{
   public interface IDelimitedFileReaderService
  
    {

        List<IEntity> Readlines<T>() where T : IEntity;
    }
}
