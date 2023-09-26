using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public class SubCategory
{
    [Key]
    public int SubCategoryId  { get; set; }
    [Required] 
    public string Name { get; set; }
}