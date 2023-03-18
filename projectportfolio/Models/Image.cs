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
        
        [Required(ErrorMessage = "Du måste fylla i en beskrivning av bilden.")]
        [Display(Name = "Beskrivning")]
        public string? AltText { get; set;}

            // Relation to Category model
        [Required(ErrorMessage = "Du måste välja en kategori.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Du måste välja en fil.")]
        [Display(Name = "Fil")]
        public IFormFile? File { get; set; }
    }
}
