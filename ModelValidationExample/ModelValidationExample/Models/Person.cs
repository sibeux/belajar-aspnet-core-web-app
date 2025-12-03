using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} tidak boleh kosong")]
        [Display(Name = "Nama Orang")]
        public string? PersonName { get; set;  }
        [Required]
        public string? Email { get; set;  }
        public string? Phone { get; set;  }
        [StringLength(maximumLength: 12, MinimumLength = 6, ErrorMessage = "{0} harus di antara minimum:{2}-maximum:{1} digit")]
        public string? Password { get; set;  }
        public string? ConfirmPassword { get; set;  }
        [Range(1, 99.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double? Price { get; set;  }

        public override string ToString()
        {
            return $"Person object - Person name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
}
