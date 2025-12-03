using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person
    {
        // Property yang pakai validate never tidak pernah dicek
        //[ValidateNever]

        [Required(ErrorMessage = "{0} tidak boleh kosong")]
        [Display(Name = "Nama Orang")]
        [RegularExpression("^[A-za-z .]*$", ErrorMessage = "{0} must be only alphabet")]
        public string? PersonName { get; set;  }
        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set;  }
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string? Phone { get; set;  }
        [StringLength(maximumLength: 12, MinimumLength = 6, ErrorMessage = "{0} harus di antara minimum:{2}-maximum:{1} digit")]
        [Required]
        public string? Password { get; set;  }
        [Compare("Password", ErrorMessage = "Password dan Confirm Password harus sama")]
        public string? ConfirmPassword { get; set;  }
        [Range(1, 99.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double? Price { get; set;  }

        // custom validator
        [MinimumYearValidator(2005, ErrorMessage = "Date of birth should more than Jan 01, {0}")]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? FromDate { get; set; }
        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public override string ToString()
        {
            return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
}
