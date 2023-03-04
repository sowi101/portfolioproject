using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace projectportfolio.Models
{
    public class Image
    {
        // Properties
        public int ImageId { get; set; }

        [Required]
        public string? FileName { get; set; }
        
        [Required]
        public string? AltText { get; set;}

        // Relation Category
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Relation Project?
        //public int ProjectId { get; set; }
        //public Project? Project { get; set; }
    }
}
