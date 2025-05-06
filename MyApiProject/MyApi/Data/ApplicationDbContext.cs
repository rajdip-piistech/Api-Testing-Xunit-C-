using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<Product> Products { get; set; }
}