using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }

        [Display(Name = "Price")]
        [Range(1,1000)]
        public double Price { get; set; }

        [Display(Name = "Price for 1-50")]
        [Range(1,1000)]
        public double Price50 { get; set; }

        [Display(Name = "Price for 1-100")]
        [Range(1,1000)]
        public double Price100 { get; set; }

    }
}