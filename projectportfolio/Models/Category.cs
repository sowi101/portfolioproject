using System.ComponentModel.DataAnnotations;

namespace projectportfolio.Models
{
    public class Category
    {
        // Properties
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett namn på kategorin.")]
        [Display(Name = "Namn")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Du måste välja ett användningsområde.")]
        [Display(Name = "Användningsområde")]
        public string? AreaOfUse { get; set; }
    }
}
