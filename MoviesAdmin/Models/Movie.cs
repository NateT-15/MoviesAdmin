namespace MoviesAdmin.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Rating {  get; set; } = string.Empty;
        [Display(Name = "Runtime (Minutes)")]
        public int RuntimeMinutes { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
    }
}
