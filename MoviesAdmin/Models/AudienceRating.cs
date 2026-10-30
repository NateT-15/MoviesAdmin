using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class AudienceRating
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [ValidateNever]
        [ScaffoldColumn(false)]
        public List<Movie> Movies { get; set; } = new();
    }
}
