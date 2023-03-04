using System.ComponentModel.DataAnnotations;

namespace projectportfolio.Models
{
    public class Category
    {
        // Properties
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Namn")]
        public string? Name { get; set; }
        
        [Required]
        [Display(Name = "Användningsområde")]
        public string? AreaOfUse { get; set; }
    }
}
