using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string ISBN { get; set; }
    public double Price { get; set; }

    public BookDetail BookDetail { get; set; }
    [ForeignKey("Publisher")]
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public List<Author> Authors{ get; set; }

    
}