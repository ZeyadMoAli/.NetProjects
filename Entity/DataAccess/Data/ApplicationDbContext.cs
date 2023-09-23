using Entity.Models;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Data;

public class ApplicationDbContext:DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<BookDetail> BookDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=ZEYAD\\SQLEXPRESS; Database=EntityDemo; TrustServerCertificate=True; Trusted_Connection=True; ");
    }
}