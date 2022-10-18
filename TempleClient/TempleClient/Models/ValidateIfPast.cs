using System.ComponentModel.DataAnnotations;

namespace TempleClient.Models
{
    public class ValidateIfPast : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime given_date;
            DateTime.TryParse(value.ToString(), out given_date);
            if (given_date >= DateTime.Now)
            {
                return ValidationResult.Success;
            }

            else
                return new ValidationResult(ErrorMessage);

        }
    }
}
