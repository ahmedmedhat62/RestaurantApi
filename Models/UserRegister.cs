using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class UserRegister
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
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
