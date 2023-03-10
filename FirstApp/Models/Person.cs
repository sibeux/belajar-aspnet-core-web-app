using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} can't be empty or null")]
        [Display(Name = "First Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} should be beetween {2} and {1} character long")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabet, space, and dots(.)")]
        public string firstName { get; set; }
        public Guid id { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        [Required(ErrorMessage = "{0} can't be blank")]
        public string email { get; set; }
        [Phone(ErrorMessage = "{0} should contain 10 digits")]
        //[ValidateNever] untuk mengabaikan validasi
        public string phone { get; set; }
        [Required(ErrorMessage = "{0} can't be blank")]
        public string password { get; set; }
        [Required(ErrorMessage = "{0} can't be blank")]
        [Compare("password", ErrorMessage = "{0} & {1} do not match")]
        [Display(Name = "Re-enter password")]
        public string confirmPassword { get; set; }
        [Range(0, 100, ErrorMessage = "{0} should be beetween ${1} and ${2}")]
        public double price { get; set; }

        public override string ToString()
        {
            return $"Person object - person name : {firstName + lastName}\n" +
                $"age : {age}\n" +
                $"email : {email}\n" +
                $"phone : {phone}\n" +
                $"password : {password}\n" +
                $"confirmPassword : {confirmPassword}\n" +
                $"price : {price}";
        }
    }
}
