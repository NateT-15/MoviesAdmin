namespace MoviesAdmin.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CriticReview
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        [Range(1, 5)]
        public int Rating { get; set; }
        public bool isPublished { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
    }
}
