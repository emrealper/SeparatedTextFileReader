
using SeparatedTextFileReader.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeparatedTextFileReader.Infrastructure.FileHelpers
{
   public class FileService :IFileService
    {

     

        private string FilePath { get; set; }



        public FileService(string filePath
          )
        {
            this.FilePath = filePath;
          

        }


      


        public IEnumerable<string> ReadLines()
        {

            if(File.Exists(FilePath))
            {

                return File.ReadLines(FilePath, Encoding.UTF8);
            }
            else
            {

                throw new FileNotFoundException(FilePath);


            }
           

        }














    }
}
