using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectportfolio.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Published { get; set; } = "Ej publicerad";
        public string? Link { get; set; } = "Länk saknas";

        [Required]
        public string? InitialDescription { get; set; }

        [Required]
        public string? TechnicalDescription { get; set; }

        [Required]
        public string? OptionalDescription { get; set; }

        // Relation Images
        [Required]
        public int? MockupId { get; set; }
        [ForeignKey("MockupId")]
        public virtual Image? MockupImg { get; set; }

        [Required]
        public int? LogotypeId { get; set; }
        [ForeignKey("LogotypeId")]
        public virtual Image? LogoImg { get; set; }

        [Required]
        public int? DetailId { get; set; }
        [ForeignKey("DetailId")]
        public virtual Image? DetailImg { get; set; }

        [Required]
        // Relation to Competence
        public List<Competence>? Competences { get; set; }

        // Relation to Category
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
