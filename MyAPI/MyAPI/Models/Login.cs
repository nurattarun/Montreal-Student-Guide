using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class Login
    {
        [Key]
        public int ID { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public string UserName { get; set; }

        
        [Required]
        public string Password { get; set; }
    }
}
