using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class Category
    {

        [Key]
        public string StudentId { get; set; }


        [Required]
        public string Name { get; set; }

        public Category() { }
    }
}
