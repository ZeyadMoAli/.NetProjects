using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMoveisModels.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    public string Title { get; set; }

    public string Description { get; set; }
    
    [Required] 
    public string ISBN { get; set; }
    
    [Required] 
    public string Autor { get; set; }
    
    [Required]
    [Display(Name = "List Price")]
    [Range(1,1000)]
    public double ListPrice { get; set; }
    
    [Required]
    [Display(Name = "Price For 1-50")]
    [Range(1,1000)]
    public double Price { get; set; }
    
    [Required]
    [Display(Name = "Price For 50+")]
    [Range(1,1000)]
    public double Price50 { get; set; }
    
    [Required]
    [Display(Name = "Price For 100+")]
    [Range(1,1000)]
    public double Price100 { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category category { get; set; }
    public string ImageUrl { get; set; }
}