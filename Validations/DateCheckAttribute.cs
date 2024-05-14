using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Validations
{
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Now)
            {
                return new ValidationResult("The date must be not less than today");
            }
            return ValidationResult.Success; //comment
        }

    }
}
