using SeparatedTextFileReader.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Domain.Entities
{
   public class Procurement :IEntity
    {

        public int Project { get; set; }

        public DateTime StartDate { get; set; }

        public string Category { get; set; }

        public string Responsible { get; set; }

        public decimal? SavingsAmount { get; set; }

        public string Currency { get; set; }

        public string Complexity { get; set; }

    }
}
