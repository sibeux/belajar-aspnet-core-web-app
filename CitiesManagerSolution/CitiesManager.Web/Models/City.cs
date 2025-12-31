using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Web.Models
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }
        [Required]
        public string? CityName { get; set; }
    }
}
