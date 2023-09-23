using MyMoveisModels.Models;

namespace MyMoviesData.Data;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
    {
        
    } 
    public DbSet<Category>Category { get; set; } 
    public DbSet<Product>Products{ get; set; } 
}