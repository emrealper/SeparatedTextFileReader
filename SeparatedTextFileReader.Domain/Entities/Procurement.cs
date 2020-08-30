using SeparatedTextFileReader.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Domain.Entities
{
   public class Procurement :IEntity
    {

        public string Project { get; set; }

        public string StartDate { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Responsible { get; set; }

        public string SavingsAmount { get; set; }

        public string Currency { get; set; }

        public string Complexity { get; set; }

    }
}
