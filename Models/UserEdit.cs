using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class UserEdit
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
