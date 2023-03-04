using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace projectportfolio.Models
{
    public class Competence
    {
        public int CompetenceId { get; set; }

        [Required]
        [Display (Name = "Kompetens")]
        public string? Name { get; set; }

        // Relation Category
        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Relation Project
        public List<Project>? Projects { get; set; }
    }
}
