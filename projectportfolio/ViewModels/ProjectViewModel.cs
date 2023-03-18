using projectportfolio.Models;

namespace projectportfolio.ViewModels
{
    public class ProjectViewModel
    {
        // Properties
        public IEnumerable<Project>? Projects { get; set; }
        public IEnumerable<Competence>? Competences { get; set; }
    }
}
