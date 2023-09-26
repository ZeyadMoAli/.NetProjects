using EntityFrameWorkPractice.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Modelss.ViewModels;

public class BookVm
{
    public Book Book { get; set; }
    public IEnumerable<SelectListItem> PublisherList { get; set; }
}