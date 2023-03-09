using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class Person
    {
        [Required]
        public Guid id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public int age { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? confirmPassword { get; set; }
        public double? price { get; set; }

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
