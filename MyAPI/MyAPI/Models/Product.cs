using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class Product
    {
        [Key]
        public string StudentId { get; set; }

        [Required]
        [Display(Name = "Opus Card Fee")]
        public string OpusCardFee { get; set; }

        [Required]
        [Display(Name = "Average Rent")]
        public int AvgRent { get; set; }

        [Required]
        [Display(Name = "Public Library Addresses")]
        public string PublicLibAdd { get; set; }

        [Required]
        public string Category { get; set;}

        public Product() { }
    }
}
