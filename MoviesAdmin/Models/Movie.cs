namespace MoviesAdmin.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;
    public class Movie
    {
        public int Id { get; set; } 

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [ValidateNever]

        public Genre Genre { get; set; } = null!;

        [Display(Name = "Audience Rating")]
        public int AudienceRatingId { get; set; }
        [ValidateNever]
        [ScaffoldColumn(false)]
        public AudienceRating AudienceRating { get; set; } = null!;

        [Display(Name = "Runtime (Minutes)")]
        public int RuntimeMinutes { get; set; }

        //URL to image file stored in azure storage
        public string ImageUrl { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
    }
}
