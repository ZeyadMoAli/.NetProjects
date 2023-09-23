using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyMoveisModels.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
public class ProductVM
{
    public Product Product { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}