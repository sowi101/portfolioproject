using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectportfolio.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string? Title { get; set; }

        public string? Published { get; set; } = "Ej publicerad";
        public string? Link { get; set; } = "Länk saknas";

        public string? InitialDescription { get; set; }

        public string? TechnicalDescription { get; set; }

        public string? OptionalDescription { get; set; }

        // Relation Images
        public int? MockupId { get; set; }
        [ForeignKey("MockupId")]
        public virtual Image? MockupImg { get; set; }

        public int? LogotypeId { get; set; }
        [ForeignKey("LogotypeId")]
        public virtual Image? LogoImg { get; set; }

        public int? DetailId { get; set; }
        [ForeignKey("DetailId")]
        public virtual Image? DetailImg { get; set; }

        // Relation to Competence
        public List<Competence>? Competences { get; set; }

        // Relation to Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
