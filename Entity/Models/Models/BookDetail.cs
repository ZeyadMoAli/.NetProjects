using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models;

public class BookDetail
{
    [Key] 
    public int BookDetailID { get; set; }
    [Required] 
    public int NumberOfChapters { get; set; }

    public int NumberOfPages { get; set; }
    public double Weight { get; set; }
    [ForeignKey("Book")]
    public int BookId { get; set; }
    public Book Book { get; set; }
}