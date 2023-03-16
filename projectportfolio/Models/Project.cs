using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectportfolio.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        
        [Required(ErrorMessage = "Du måste fylla i en titel på projektet!")]
        [Display(Name = "Titel")]
        public string? Title { get; set; }

        [Display(Name = "Publicerad")]
        public string? Published { get; set; }

        [Display(Name = "Länk")]
        public string? Link { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en inledande beskrivning!")]
        [Display(Name = "Inledande beskrivning")]
        public string? InitialDescription { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en teknisk beskrivning.")]
        [Display(Name = "Teknisk beskrivning")]
        public string? TechnicalDescription { get; set; }

        [Display(Name = "Valfri beskrivning")]
        public string? OptionalDescription { get; set; }

        // Relation Images
        [Display(Name = "Mockupbild")]
        [Required(ErrorMessage = "Du måste välja en mockupbild.")]
        public int? MockupId { get; set; }
        [ForeignKey("MockupId")]
        public virtual Image? MockupImg { get; set; }

        [Display(Name = "Logotyp")]
        [Required(ErrorMessage = "Du måste välja en logotyp.")]
        public int? LogotypeId { get; set; }
        [ForeignKey("LogotypeId")]
        public virtual Image? LogoImg { get; set; }

        [Display(Name = "Detaljbild")]
        [Required(ErrorMessage = "Du måste välja en detaljbild.")]
        public int? DetailId { get; set; }
        [ForeignKey("DetailId")]
        public virtual Image? DetailImg { get; set; }

        // Relation to Competence
        [Display(Name = "Kompetenser")]
        //[Required(ErrorMessage = "Du måste välja kompetenser som använts.")]
        public virtual ICollection<Competence>? Competences { get; set; }

        // Relation to Category
        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Du måste välja en kategori.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
