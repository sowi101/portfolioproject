using System.ComponentModel.DataAnnotations;

namespace projectportfolio.Models
{
    public class Experience
    {
        // Properties
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i utbildning eller tjänst.")]
        [Display (Name = "Utbildning / tjänst")]
        public string? What { get; set; }

        [Required(ErrorMessage = "Du måste fylla i var du studerade eller arbetade.")]
        [Display(Name = "Lärosäte / arbetsgivare")]
        public string? Where { get; set; }

        [Required(ErrorMessage = "Du måste fylla i vilket år du började.")]
        [Display(Name = "Start")]
        public int? Start { get; set; }

        [Display(Name = "Slut")]
        public string? End { get; set; }
        
        [Required(ErrorMessage = "Du måste fylla i en beskrivning av utbildningen/tjänsten.")]
        [Display(Name = "Beskrivning")]
        public string? Description { get; set; }

        // Relation Category
        [Required(ErrorMessage = "Du måste välja en kategori.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
