
using FluentValidation.Results;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.Validators;
using System;
using System.Collections.Generic;

using System.Text;

namespace SeparatedTextFileReader.Domain.ValueObject
{
  public class AdProcurement
    {

        private AdProcurement()
        {



        }



        public static bool For(Procurement entity,out AdProcurement value,out string validationError)
        {
            var errorMessage = string.Empty;
            var isValidationPassed = false;

            var adProcurement = new AdProcurement();

            try
            {

                ProcurementEntityValidator validator = new ProcurementEntityValidator();
                ValidationResult results = validator.Validate(entity);

                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {

                    }
                }
                else
                {
                    adProcurement.Project = Int32.Parse(entity.Project);

                }

                

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }


            value = adProcurement;
            validationError = errorMessage;
            return isValidationPassed;
        }



        public int Project { get; private set; }

        public DateTime StartDate { get; private set; }

        public string Category { get; private  set; }

        public string Responsible { get; private set; }

        public decimal SavingsAmount { get; private set; }

        public string Currency { get; private set; }

        public string Complexity { get; private  set; }

    }
}
