namespace MoviesAdmin.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
