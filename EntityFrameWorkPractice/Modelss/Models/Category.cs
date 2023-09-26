using System.ComponentModel.DataAnnotations;

namespace EntityFrameWorkPractice.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [MaxLength(50)]
    public string GenreName { get; set; }
    [Required]
    public int DisplayOrder { get; set; }
}