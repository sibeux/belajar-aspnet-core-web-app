using System.ComponentModel.DataAnnotations;

namespace FirstApp.Course_Udemy.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public int MinimumYear { get; set; } = 2002;

        // parameterless constructor
        public MinimumYearValidatorAttribute() { }

        public String DefaultErrorMessage = "Year should be greater than {0}";

        // parameterized cosntructor
        public MinimumYearValidatorAttribute(int minimumYear) { 
        MinimumYear = minimumYear;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year < MinimumYear)
                {
                    return new ValidationResult(String.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
