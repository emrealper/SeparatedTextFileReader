using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using FluentValidation;
using SeparatedTextFileReader.Domain.Entities;

namespace SeparatedTextFileReader.Domain.Validators
{
  public  class ProcurementEntityValidator: AbstractValidator<Procurement>
    {
        public ProcurementEntityValidator()
        {

            List<string> validComplexityTypes = new List<string> { "Simple", "Moderate", "Hazardous" };

            RuleFor(x => x.Project).NotEmpty();

            RuleFor(x => x.Complexity)
        .Must(x => validComplexityTypes.Contains(x))
        .WithMessage("Column Complexity should only have following values: " + String.Join(",", validComplexityTypes));

            RuleFor(x => x.StartDate).Must(IsValidDateTimeFormat)
           .WithMessage("Start date should conform to the following format: yyyy-mm-dd hh:mm:ss.sss");



            When(x => x.SavingsAmount != "NULL",
   () => {
       RuleFor(x => x.SavingsAmount)
           .Matches("^[0-9]+[.][0-9]{6}?$")
     .WithMessage("Money (Savings amount) values should conform be numbers with a point as the decimal separator");
   });




           
        }

        private bool IsValidDateTimeFormat(string value)
        {
            DateTime date;
            return DateTime.TryParseExact(value,
                       "yyyy-MM-dd HH:mm:ss.fff",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out date);
        }

    }
}
