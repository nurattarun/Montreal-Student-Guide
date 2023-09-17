using System.ComponentModel.DataAnnotations;

namespace Montreal_Student_Guide.Models
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
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Public Library Addresses")]
        public string PublicLibAdd { get; set; }

        [Required]
        [Display(Name = "Student Category")]
        public string Category { get; set; }

        public Product() { }

        public Product(string studentId, string opusCardFee, int avgRent, string publicLibAdd, string category)
        {
            StudentId = studentId;
            OpusCardFee = opusCardFee;
            AvgRent = avgRent;
            PublicLibAdd = publicLibAdd;
            Category = category;
        }
    }
}
