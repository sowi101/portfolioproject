using System.ComponentModel.DataAnnotations;

namespace projectportfolio.Models
{
    public class Experience
    {
        // Properties
        public int ExperienceId { get; set; }

        [Required]
        [Display (Name = "Utbildning / tjänst")]
        public string? What { get; set; }

        [Required]
        [Display(Name = "Lärosäte / arbetsgivare")]
        public string? Where { get; set; }

        [Required]
        [Display(Name = "Start")]
        public int? Start { get; set; }

        [Display(Name = "Slut")]
        public string? End { get; set; } = "Pågående";
        
        [Required]
        [Display(Name = "Beskrivning")]
        public string? Description { get; set; }

        // Relation Category
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
