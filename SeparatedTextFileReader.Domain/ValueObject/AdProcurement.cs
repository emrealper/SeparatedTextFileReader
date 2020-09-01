using FluentValidation.Results;
using SeparatedTextFileReader.Domain.Entities;
using SeparatedTextFileReader.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SeparatedTextFileReader.Domain.ValueObject
{
    public class AdProcurement
    {
        private AdProcurement()
        {
        }


        public static bool For(Procurement entity, out AdProcurement value, out string validationError)
        {
            var errorMessage = string.Empty;
            var isValidationPassed = false;

            var adProcurement = new AdProcurement();

            try
            {
                var validator = new ProcurementEntityValidator();
                var results = validator.Validate(entity);

                if (!results.IsValid)
                {
                    errorMessage = string.Join("\n", results.Errors);
                }
                else
                {
                    adProcurement.Project = int.Parse(entity.Project);
                    adProcurement.Complexity = entity.Complexity;
                    adProcurement.Category = entity.Category;
                    adProcurement.SavingsAmount = entity.SavingsAmount == "NULL" ? string.Empty : entity.SavingsAmount;
                    adProcurement.Currency = entity.Currency == "NULL" ? string.Empty : entity.Currency;
                    adProcurement.StartDate = DateTime.ParseExact(entity.StartDate, "yyyy-MM-dd HH:mm:ss.fff",
                        CultureInfo.InvariantCulture);
                    adProcurement.Description = entity.Description;
                    adProcurement.Responsible = entity.Responsible;
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


        public string ToTabDelimatedString(Dictionary<string, int> propertiesInOrder)
        {
            var propsValueList = new List<string>();

            foreach (var mapping in propertiesInOrder)
            {
                var prop = GetType().GetProperty(mapping.Key).GetValue(this);

                if (GetType().GetProperty(mapping.Key).GetValue(this) == null)
                {
                    propsValueList.Add(string.Empty);
                }
                else
                {
                    if (mapping.Key == "StartDate")
                        propsValueList.Add(StartDateTime);
                    else
                        propsValueList.Add(prop.ToString());
                }
            }

            return string.Join("\t", propsValueList);
        }


        public int Project { get; private set; }

        public DateTime StartDate { get; private set; }

        public string StartDateTime => StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

        public string Description { get; private set; }

        public string Category { get; private set; }

        public string Responsible { get; private set; }

        public string SavingsAmount { get; private set; }

        public string Currency { get; private set; }

        public string Complexity { get; private set; }
    }
}