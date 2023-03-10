using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace projectportfolio.Models
{
    public class Competence
    {
        public int CompetenceId { get; set; }

        [Required(ErrorMessage = "Du måste välja ett namn på kompetense.")]
        [Display (Name = "Kompetens")]
        public string? Name { get; set; }

        // Relation Category
        [Required(ErrorMessage = "Du måste välja en kategori.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Relation Project
        public virtual ICollection<Project>? Projects { get; set; }
    }
}
