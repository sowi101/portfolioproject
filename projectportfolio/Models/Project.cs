namespace projectportfolio.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public int? Published { get; set; }

        // Relation to Competence
        public int CompetenceId { get; set; }
        public List<Competence>? Competence { get; set; }

        // Relation to Image
        public int ImageID { get; set; }
        public List<Image>? Image { get;set; }
    }
}
