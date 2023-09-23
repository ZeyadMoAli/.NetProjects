using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models;

public class Author
{
    [Key]
    public int AuthorId { get; set; }

    [Required]
    [MaxLength(50) ]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string Location { get; set; }
    [NotMapped]
    public string FullName
    {
        get
        {
            return $"{FirstName }{{LastName}}";
        }
    }

    public List<Book> Books{ get; set; }
}