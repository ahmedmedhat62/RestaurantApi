using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class UserDTO
    {
        [Required]
       public string FullName { get; set; }
      
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
       
        public Guid id { get; set; }

       [Required]
       public Gender Gender { get; set; }
        
    }
}
