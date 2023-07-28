using Microsoft.EntityFrameworkCore;
using MVC_Intro.Models;

namespace MVC_Intro.Data;

public sealed class MvcAppContext : DbContext
{
    public MvcAppContext(DbContextOptions options) : base(options)
    {
       Database.EnsureCreatedAsync();
       // Database.EnsureDeletedAsync();
    }

    public DbSet<Product> Products => Set<Product>();
}