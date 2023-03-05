using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace projectportfolio.Models
{
    public class Image
    {
        // Properties
        public int ImageId { get; set; }

        [Display(Name = "Filnamn")]
        public string? Name { get; set; }
        
        [Required]
        [Display(Name = "Beskrivning")]
        public string? AltText { get; set;}

        // Relation Category
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [NotMapped]
        [Display(Name = "Fil")]
        public IFormFile? File { get; set; }
    }
}
