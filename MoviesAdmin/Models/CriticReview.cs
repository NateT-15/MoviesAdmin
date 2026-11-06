namespace MoviesAdmin.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;

    public class CriticReview
    {
        public int Id { get; set; }

        // foreign key
        [Display(Name = "Movie")]
        public int MovieId { get; set; }

        // navigation property
        [ValidateNever]
        public Movie Movie { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }

        [Display(Name = "Publish")]
        public bool IsPublished { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        [Display(Name = "Date Created")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
