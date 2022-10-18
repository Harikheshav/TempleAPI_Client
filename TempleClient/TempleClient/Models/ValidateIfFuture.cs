using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TempleClient.Models
{
    public class ValidateIfFuture : ValidationAttribute
    {
        public string OtherProperty { get; private set; }
        public ValidateIfFuture(string otherProperty)
        {
            OtherProperty = otherProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime end_date;
            DateTime.TryParse(value.ToString(), out end_date);
            PropertyInfo startdateinfo = validationContext.ObjectType.GetProperty(OtherProperty);
            object startdateval = startdateinfo.GetValue(validationContext.ObjectInstance, null);
            DateTime start_date;
            DateTime.TryParse(startdateval.ToString(), out start_date);

            if (end_date >= start_date)
            {
                return ValidationResult.Success;
            }
            else
                return new ValidationResult(ErrorMessage);

        }
    }
}
