using System.ComponentModel.DataAnnotations;

namespace MVC_Movie.Models;

public class Anime
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Display(Name = "Image URL")]
    public string? ImageURL { get; set; }

    [Display(Name = "Additional Details (Release Year -- Number of episodes -- Censor Certificate)")] 
    public string? Metadata { get; set; }

    [Range(0, 10, ErrorMessage = "*Only the value between 0 - 10 is accepted")]
    public double Rating { get; set; }
    public string? Description { get; set; }
}
